using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] float distanceRange;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceRange, layerMask))
        {
            Debug.Log($"{hit.transform.gameObject.name}");
        }
    }
}
