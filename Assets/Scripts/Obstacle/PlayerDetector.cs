using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private Transform player = null;
    public Transform Player => player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.PlayerTag))
        {
            Debug.Log("In");
            player = other.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.PlayerTag))
        {
            Debug.Log("Out");
            player = null;
        }
    }
}
