using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private bool isIn = false;
    public bool IsIn => isIn;

    Piston piston = null;

    public void Init(Piston _piston)
    {
        piston = _piston;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameManager.PLAYER_LAYER)
        {
            isIn = true;
            piston?.ChangeTargetPoint(PistonState.Pull);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == GameManager.PLAYER_LAYER)
        {
            isIn = false;
            piston?.ChangeTargetPoint(PistonState.Push);
        }
    }
}
