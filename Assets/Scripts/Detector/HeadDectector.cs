using UnityEngine;

public class HeadDectector : MonoBehaviour
{
    Piston piston;
    GameObject head;

    public void Init(Piston _piston, GameObject _head)
    {
        piston = _piston;
        head = _head;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject == head.gameObject)
        //{
        //    //piston.ExcutePush();
        //}
    }
}
