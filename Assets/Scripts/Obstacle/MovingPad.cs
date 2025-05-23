using UnityEngine;

public class MovingPad : MonoBehaviour
{
    protected const float REACH_THRESHOLD = 0.05f;

    [Header("MovingPad")]
    [SerializeField] protected Transform movingPad;
    protected PlayerDetector detector;
    [SerializeField] protected GameObject pointsObject;
    protected Transform[] points;
    protected int index = 1;
    [SerializeField] private float speed;
    protected Vector3 moveDir;

    protected Rigidbody player;

    protected virtual void Awake()
    {
        points = pointsObject.GetComponentsInChildren<Transform>();
        detector = movingPad.GetComponentInChildren<PlayerDetector>();
    }

    protected virtual void Start()
    {
        player = GameManager.Instance.Player.GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        // 타겟에 도착하면 다음 타겟으로 변경
        if (IsArrived())
        {
            ChangeTargetPoint();
        }

        movingPad.position += speed * Time.fixedDeltaTime * moveDir;

        // 플레이어가 있을 때, 플레이어 움직임 처리
        if(detector.IsIn)
            player.MovePosition(player.position + speed * Time.fixedDeltaTime * moveDir);
    }

    // 타겟 포인트에 도착했는지 확인
    protected bool IsArrived()
    {
        float distance = Vector3.Distance(points[index].position, movingPad.position);
        if (distance > REACH_THRESHOLD)
        {
            return false;
        }

        movingPad.position = points[index].position;
        return true;
    }

    // 다음 타겟 포인트로 변경
    private void ChangeTargetPoint()
    {
        index = (index + 1) < points.Length ? index + 1 : 1;
        moveDir = (points[index].position - movingPad.position).normalized;
    }
}
