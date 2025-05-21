using UnityEngine;

public class MovingPad : MonoBehaviour
{
    [SerializeField] Transform movingPad;
    PlayerDetector playerDetector;
    [SerializeField] GameObject pointsObject;
    Transform[] points;
    [SerializeField] float speed;
    Vector3 moveDir;
    int index = 1;

    private void Awake()
    {
        points = pointsObject.GetComponentsInChildren<Transform>();
        playerDetector = movingPad.GetComponentInChildren<PlayerDetector>();
    }

    private void Update()
    {
        if (IsArrived())
        {
            ChangeTargetPoint();
        }

        movingPad.position += speed * Time.deltaTime * moveDir;
        if(playerDetector.Player != null) playerDetector.Player.position += speed * Time.deltaTime * moveDir;
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
