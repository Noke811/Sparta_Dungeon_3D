using UnityEngine;

public class MovingPad : MonoBehaviour
{
    [SerializeField] Transform movingPad;
    PlayerDetector playerDetector;
    [SerializeField] GameObject pointsObject;
    Transform[] points;
    int index = 1;
    [SerializeField] float speed;
    Vector3 moveDir;

    GameObject player;

    private void Awake()
    {
        points = pointsObject.GetComponentsInChildren<Transform>();
        playerDetector = movingPad.GetComponentInChildren<PlayerDetector>();
    }

    private void Start()
    {
        player = GameManager.Instance.PlayerInfo.gameObject;
    }

    private void Update()
    {
        if (IsArrived())
        {
            ChangeTargetPoint();
        }

        movingPad.position += speed * Time.deltaTime * moveDir;

        if(playerDetector.IsIn)
            player.transform.SetParent(transform);
        else
            player.transform.SetParent(null);
    }

    private bool IsArrived()
    {
        if ((points[index].position - movingPad.position).magnitude > 0.01f)
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
