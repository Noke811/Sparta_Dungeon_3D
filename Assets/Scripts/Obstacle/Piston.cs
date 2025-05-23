using UnityEngine;

public enum PistonState
{
    Push = 1,
    Pull,
}

public class Piston : MovingPad
{
    const float RECOVER_SPEED = 1f;
    const float EXCUTE_SPEED = 6f;

    [Header("Piston")]
    [SerializeField] float power;

    bool excuted;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        detector.Init(this);
    }

    protected override void FixedUpdate()
    {
        if (detector.IsIn)
        {
            if (IsArrived())
            {
                if (excuted) 
                {
                    excuted = false;
                    player.AddForce(movingPad.up * power, ForceMode.Impulse);
                }
                ChangeTargetPoint(PistonState.Push);
                excuted = true;
            }
        }
        else
        {
            excuted = false;
        }

        if(!IsArrived())
            MoveHead(detector.IsIn);
    }

    private void MoveHead(bool hasPlayer)
    {
        float speed = excuted ? EXCUTE_SPEED : RECOVER_SPEED;

        movingPad.position += speed * Time.fixedDeltaTime * moveDir;
        if(hasPlayer) player.MovePosition(player.position + speed * Time.fixedDeltaTime * moveDir);
    }

    public void ChangeTargetPoint(PistonState state)
    {
        index = (int)state;
        moveDir = (points[index].position - movingPad.position).normalized;
    }
}
