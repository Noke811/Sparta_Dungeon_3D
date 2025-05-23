using UnityEngine;

public class Piston : MovingPad
{
    const float RECOVER_SPEED = 3f;
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
            if (excuted)
            {
                if (IsArrived())
                {
                    player.AddForce(movingPad.up * power, ForceMode.Impulse);
                    return;
                }

                movingPad.position += EXCUTE_SPEED * Time.fixedDeltaTime * moveDir;
                player.MovePosition(player.position + EXCUTE_SPEED * Time.fixedDeltaTime * moveDir);
                return;
            }

            if (IsArrived())
            {
                ChangeTargetPoint();
                excuted = true;
                return;
            }

            movingPad.position += RECOVER_SPEED * Time.fixedDeltaTime * moveDir;
            player.MovePosition(player.position + RECOVER_SPEED * Time.fixedDeltaTime * moveDir);
        }
        else
        {
            excuted = false;

            if (IsArrived())
            {
                return;
            }
            
            movingPad.position += RECOVER_SPEED * Time.fixedDeltaTime * moveDir;
        }
    }
}
