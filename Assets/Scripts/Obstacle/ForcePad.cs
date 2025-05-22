using UnityEngine;

public class ForcePad : MonoBehaviour
{
    [SerializeField] float power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameManager.PLAYER_LAYER)
        {
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            rigid.velocity = Vector3.zero;
            rigid.AddForce(transform.up * power, ForceMode.Impulse);
        }
    }
}
