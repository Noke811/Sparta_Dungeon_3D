using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    [SerializeField] Transform climbWall;
    PlayerDetector playerDetector;

    PlayerController controller;

    private void Awake()
    {
        playerDetector = climbWall.GetComponentInChildren<PlayerDetector>();
    }

    private void Start()
    {
        controller = GameManager.Instance.PlayerInfo.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerDetector.IsIn)
            controller.HangOnPlayer(true);
        else
            controller.HangOnPlayer(false);
    }
}
