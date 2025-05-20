using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}
