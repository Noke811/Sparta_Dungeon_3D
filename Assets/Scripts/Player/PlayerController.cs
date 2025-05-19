using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] LayerMask groundLayerMask;
    Vector2 moveDir;

    [Header("Look")]
    [SerializeField] Transform camContainer;
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

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        UpdateLookDirection();
    }

    void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir *= playerInfo.Speed;
        dir.y = rigid.velocity.y;

        rigid.velocity = dir;
    }

    void UpdateLookDirection()
    {
        camCurXRot += mouseDelta.y * sensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, -lookRange, lookRange);
        camContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * sensitivity, 0);
    }

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
                rigid.AddForce(Vector2.up * playerInfo.JumpPower, ForceMode.Impulse);
        }
    }

    // 아이템 사용(F)
    void OnUse()
    {
        playerInfo.Damage(10f);
    }

    // 카메라 이동(마우스 Delta)
    void OnLook(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }

    // 달리기 상태(Shift)
    void OnRun()
    {
        if (!playerInfo.isRun)
        {
            playerInfo.SetSpeedRun();
        }
        else
        {
            playerInfo.SetSpeedOrigin();
        }
        playerInfo.isRun = !playerInfo.isRun;
    }
    #endregion
}
