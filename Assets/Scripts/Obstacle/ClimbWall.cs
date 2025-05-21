using UnityEngine;

public class ClimbWall : MonoBehaviour
{
    [SerializeField] Transform climbWall;
    PlayerDetector playerDetector;

    Rigidbody playerRigid = null;

    private void Awake()
    {
        playerDetector = climbWall.GetComponentInChildren<PlayerDetector>();
    }

    private void FixedUpdate()
    {
        if (playerDetector.Player != null)
        {
            playerRigid = playerDetector.Player.GetComponent<Rigidbody>();
            playerRigid.useGravity = false;
        }
        else
        {
            if(playerRigid != null)
            {
                playerRigid.useGravity = true;
            }
        }
    }
}
