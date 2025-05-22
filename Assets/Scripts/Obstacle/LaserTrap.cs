using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] Transform detectObject;
    [SerializeField] Rigidbody trapObject;
    LineRenderer line;
    [SerializeField] Gradient defaultColor;
    [SerializeField] Gradient detectedColor;
    [SerializeField] float length;

    bool onTrap = false;

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        line.positionCount = 2;
        line.SetPosition(0, detectObject.position);
        line.SetPosition(1, detectObject.position + detectObject.forward * length);

        trapObject.useGravity = false;
        trapObject.isKinematic = true;
    }

    private void Update()
    {
        Ray ray = new Ray(detectObject.position, detectObject.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, length, 1 << GameManager.PLAYER_LAYER))
        {
            line.colorGradient = detectedColor;
            if (!onTrap)
            {
                onTrap = true;
                ExcuteTrap();
            }
        }
        else
        {
            line.colorGradient = defaultColor;
        }
    }

    private void ExcuteTrap()
    {
        trapObject.isKinematic = false;
    }
}
