using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField] Rigidbody head;
    [SerializeField] PlayerDetector detector;
    [SerializeField] HeadDectector power;
    [SerializeField] float pushPower;

    Rigidbody player;

    private void Start()
    {
        player = GameManager.Instance.Player.GetComponent<Rigidbody>();
        power.Init(head, pushPower);
    }

    private void FixedUpdate()
    {
        if (detector.IsIn)
            head.useGravity = true;
        else
            head.useGravity = false;
    }
}
