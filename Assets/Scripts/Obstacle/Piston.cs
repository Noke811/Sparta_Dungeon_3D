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
            // 플레이어가 있을 때, 타겟 포인트에 도착
            if (IsArrived())
            {
                // 만약 피스톤이 실행 중이라면
                if (excuted) 
                {
                    excuted = false;

                    // 플레이어에게 힘을 가함
                    player.AddForce(movingPad.up * power, ForceMode.Impulse);
                }

                // 피스톤이 실행 중이 아니라면, 밀어야 하는 상태로 변경
                ChangeTargetPoint(PistonState.Push);
                excuted = true;
            }
        }
        else
        {
            excuted = false;
        }

        // 타겟 포인트에 도착하지 않았다면, 계속 움직임
        if(!IsArrived())
            MoveHead(detector.IsIn);
    }

    // 헤드 오브젝트를 움직이는 메서드
    private void MoveHead(bool hasPlayer)
    {
        float speed = excuted ? EXCUTE_SPEED : RECOVER_SPEED;

        movingPad.position += speed * Time.fixedDeltaTime * moveDir;

        // 플레이어 있을 때는 플레이어도 같이 움직임
        if(hasPlayer) player.MovePosition(player.position + speed * Time.fixedDeltaTime * moveDir);
    }

    // 밀어야 하는 상태인지, 당겨야 하는 상태인지 변환
    public void ChangeTargetPoint(PistonState state)
    {
        index = (int)state;
        moveDir = (points[index].position - movingPad.position).normalized;
    }
}
