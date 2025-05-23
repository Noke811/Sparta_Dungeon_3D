using UnityEngine;

public class HeadDectector : MonoBehaviour
{
    Rigidbody head;
    float power;

    public void Init(Rigidbody _head, float _power)
    {
        head = _head;
        power = _power;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == head.gameObject)
        {
            head.constraints = RigidbodyConstraints.None;
            head.AddForce(head.transform.up * power, ForceMode.Impulse);
        }
    }
}
