using UnityEngine;
using UnityEngine.InputSystem;

public enum CamState
{
    FirstPerson,
    ThirdPerson_Back,
    ThirdPerson_Front,
}

public class PlayerController : MonoBehaviour
{
    const int CAM_STATE_CNT = 3;

    [Header("Move")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] bool godMode;
    Vector2 moveDir;
    bool isHangOn = false;

    [Header("Look")]
    [SerializeField] Transform camContainer;
    [SerializeField] Transform cam_Back;
    [SerializeField] Transform cam_Front;
    [SerializeField] LayerMask faceLayer;
    Transform camTransform;
    CamState camState;
    [SerializeField] float lookRange;
    [SerializeField] float sensitivity;
    Vector2 mouseDelta;
    float camCurXRot;

    Rigidbody rigid;
    PlayerInfo playerInfo;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        playerInfo = GetComponent<PlayerInfo>();
    }

    private void Start()
    {
        camTransform = Camera.main.transform;
        camState = CamState.ThirdPerson_Front;
        ChangeCameraState();
    }

    private void Update()
    {
        if(transform.position.y < -20f)
        {
            playerInfo.Damage(40f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // 갓 모드 활성화 : 유체화 상태
        if (godMode)
        {
            HangOnPlayer(true);
        }

        if (!isHangOn)
        {
            Move();
        }
        else
        {
            ClimbUp();
        }
    }

    private void LateUpdate()
    {
        UpdateLookDirection();
    }

    // 플레이어 이동
    void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir *= playerInfo.Speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + dir);
    }

    // 플레이어 벽 오르기
    void ClimbUp()
    {
        Vector3 dir = camContainer.forward * moveDir.y + camContainer.right * moveDir.x;
        dir *= playerInfo.Speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + dir);
    }

    // 카메라 이동
    void UpdateLookDirection()
    {
        camCurXRot += mouseDelta.y * sensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, -lookRange, lookRange);
        camContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * sensitivity, 0);
    }

    // 땅에 닿아있는지 검사
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    // 카메라의 상태(1인칭 / 3인칭_뒤 / 3인칭_앞)를 변경
    void ChangeCameraState()
    {
        camState = (CamState)(((int)camState + 1) % CAM_STATE_CNT);

        switch (camState)
        {
            case CamState.FirstPerson:
                Camera.main.cullingMask &= ~faceLayer.value;
                camTransform.SetParent(camContainer);
                break;

            case CamState.ThirdPerson_Back:
                camTransform.SetParent(cam_Back);
                Camera.main.cullingMask |= faceLayer.value;
                break;

            case CamState.ThirdPerson_Front:
                camTransform.SetParent(cam_Front);
                break;
        }
        camTransform.localPosition = Vector3.zero;
        camTransform.localRotation = Quaternion.identity;
        camTransform.localScale = Vector3.one;
    }

    public void HangOnPlayer(bool _isHangOn)
    {
        isHangOn = _isHangOn;

        if (isHangOn)
        {
            rigid.velocity = Vector3.zero;
            rigid.useGravity = false;

            playerInfo.ChangePlayerState(PlayerState.Walk);
        }
        else
        {
            rigid.useGravity = true;
        }
    }

    #region InputSystem
    // 방향키(WASD, ↑←↓→)
    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    // 점프(Space)
    void OnJump()
    {
        if (IsGrounded())
        {
            if(playerInfo.CanJump())
                rigid.AddForce(Vector3.up * playerInfo.JumpPower, ForceMode.Impulse);
        }
        else
        {
            if (playerInfo.IsDoubleJump)
            {
                playerInfo.DoubleJumpDone();
                rigid.AddForce(Vector3.up * playerInfo.JumpPower, ForceMode.Impulse);
            }
        }
    }

    // 카메라 이동(마우스 Delta)
    void OnLook(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }

    // 달리기 상태(Shift)
    void OnRun()
    {
        if (IsGrounded())
        {
            playerInfo.ChangePlayerState();
        }
    }

    // 카메라 상태 변경(Z)
    void OnCamChange()
    {
        ChangeCameraState();
    }
    #endregion
}
