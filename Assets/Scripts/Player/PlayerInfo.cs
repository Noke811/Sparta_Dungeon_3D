using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Run,
}

public class PlayerInfo : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float baseHealth;
    public float MaxHealth => baseHealth;
    private float health;
    public float Health => health;

    [Header("Stamina")]
    [SerializeField] float baseStamina;
    public float MaxStamina => baseStamina;
    [SerializeField] float staminaRecovery;
    private float stamina;
    public float Stamina => stamina;

    [Header("Speed")]
    [SerializeField] float baseSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float runStamina;
    private float speed;
    private float speedMultiplier = 1f;
    public float Speed => speed * speedMultiplier;

    [Header("Jump")]
    [SerializeField] float baseJumpPower;
    [SerializeField] float jumpStamina;
    private float jumpPower;
    private float jumpMultiplier = 1f;
    public float JumpPower => jumpPower * jumpMultiplier;
    private bool isDoubleJump;
    public bool IsDoubleJump => isDoubleJump;

    private PlayerState state;

    private void Awake()
    {
        health = baseHealth;
        stamina = baseStamina;
        speed = baseSpeed;
        jumpPower = baseJumpPower;

        state = PlayerState.Walk;
    }

    private void Update()
    {
        switch (state)
        {
            case PlayerState.Walk:
                Rest(staminaRecovery * Time.deltaTime);
                break;

            case PlayerState.Run:
                Run(runStamina * Time.deltaTime);
                break;
        }
    }

    // 플레이어 걷기/달리기 상태 변경 : 현재 상태에서 다음 상태로
    public void ChangePlayerState()
    {
        switch (state)
        {
            case PlayerState.Walk:
                state = PlayerState.Run;
                SetRunSpeed();
                break;

            case PlayerState.Run:
                state = PlayerState.Walk;
                SetWalkSpeed();
                break;
        }
    }

    // 플레이어 걷기/달리기 상태 변경 : 원하는 상태로
    public void ChangePlayerState(PlayerState _state)
    {
        switch (_state)
        {
            case PlayerState.Walk:
                state = PlayerState.Walk;
                SetWalkSpeed();
                break;

            case PlayerState.Run:
                state = PlayerState.Run;
                SetRunSpeed();
                break;
        }
    }

    #region HP
    // HP 회복
    public void Heal(float amount)
    {
        if(health + amount > baseHealth)
        {
            health = baseHealth;
            return;
        }

        health += amount;
    }

    // HP 데미지
    public void Damage(float amount)
    {
        if (health - amount < 0)
        {
            health = 0;
            Die();
            return;
        }

        health -= amount;
    }

    // 플레이어 사망
    private void Die()
    {
        GameManager.Instance.GameOver();
        //Destroy(gameObject);
    }
    #endregion

    #region Stamina
    // 걷기 상태일 때 스테미나 회복
    public void Rest(float amount)
    {
        if (stamina + amount > baseStamina)
        {
            stamina = baseStamina;
            return;
        }

        stamina += amount;
    }

    // 달리기 상태일 때 스테미나 소비
    public void Run(float amount)
    {
        if (stamina - amount < 0)
        {
            stamina = 0;
            ChangePlayerState();
            return;
        }

        stamina -= amount;
    }

    // 점프할 수 있는 지 검사
    public bool CanJump()
    {
        if(stamina < jumpStamina)
        {
            return false;
        }

        stamina -= jumpStamina;
        return true;
    }
    #endregion

    #region Speed
    // 스피드 아이템 적용
    public void SpeedBuff(float multiplier, float duration)
    {
        StartCoroutine(CoSpeedBuff(multiplier, duration));
    }
    private IEnumerator CoSpeedBuff(float multiplier, float duration)
    {
        speedMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        speedMultiplier = 1f;
    }

    // 걷기 속도로 변경
    private void SetWalkSpeed()
    {
        speed = baseSpeed;
    }

    // 달리기 속도로 변경
    private void SetRunSpeed()
    {
        speed = runSpeed;
    }
    #endregion

    #region Jump
    // 점프 아이템 적용
    public void JumpBuff(float multiplier, float duration)
    {
        StartCoroutine(CoJumpBuff(multiplier, duration));
    }
    private IEnumerator CoJumpBuff(float multiplier, float duration)
    {
        jumpMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        jumpMultiplier = 1f;
    }

    public void SetDoubleJump()
    {
        isDoubleJump = true;
    }

    public void DoubleJumpDone()
    {
        isDoubleJump = false;
    }
    #endregion
}
