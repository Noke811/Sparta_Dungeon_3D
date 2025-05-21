using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private Transform player = null;
    public Transform Player => player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.PlayerTag))
        {
            Debug.Log("player in");
            player = collision.gameObject.GetComponent<Transform>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameManager.Instance.PlayerTag))
        {
            Debug.Log("player out");
            player = null;
        }
    }
}
