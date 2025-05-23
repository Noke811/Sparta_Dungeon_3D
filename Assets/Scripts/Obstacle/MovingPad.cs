using UnityEngine;

public class MovingPad : MonoBehaviour
{
    [SerializeField] Transform movingPad;
    PlayerDetector playerDetector;
    [SerializeField] GameObject pointsObject;
    Transform[] points;
    int index = 1;
    [SerializeField] float speed;
    float arrivalThreshold = 0.1f; 
    Vector3 moveDir;

    Rigidbody player;

    private void Awake()
    {
        points = pointsObject.GetComponentsInChildren<Transform>();
        playerDetector = movingPad.GetComponentInChildren<PlayerDetector>();
    }

    private void Start()
    {
        player = GameManager.Instance.PlayerInfo.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsArrived())
        {
            ChangeTargetPoint();
        }

        movingPad.position += speed * Time.fixedDeltaTime * moveDir;

        if(playerDetector.IsIn)
            player.MovePosition(player.position + speed * Time.fixedDeltaTime * moveDir);
    }

    private bool IsArrived()
    {
        float distance = Vector3.Distance(points[index].position, movingPad.position);
        if (distance > arrivalThreshold)
        {
            return false;
        }

        movingPad.position = points[index].position;
        return true;
    }

    private void ChangeTargetPoint()
    {
        index = (index + 1) < points.Length ? index + 1 : 1;
        moveDir = (points[index].position - movingPad.position).normalized;
    }
}
