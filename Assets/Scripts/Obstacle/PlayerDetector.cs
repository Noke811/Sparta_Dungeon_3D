using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private bool isIn = false;
    public bool IsIn => isIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.PlayerTag))
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.PlayerTag))
        {
            isIn = false;
        }
    }
}
