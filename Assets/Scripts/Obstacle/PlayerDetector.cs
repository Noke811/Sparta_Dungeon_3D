using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private bool isIn = false;
    public bool IsIn => isIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameManager.PLAYER_LAYER)
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == GameManager.PLAYER_LAYER)
        {
            isIn = false;
        }
    }
}
