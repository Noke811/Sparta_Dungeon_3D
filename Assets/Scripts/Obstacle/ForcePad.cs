using UnityEngine;

public class ForcePad : MonoBehaviour
{
    [SerializeField] float power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            rigid.AddForce(transform.up * power, ForceMode.Impulse);
        }
    }
}
