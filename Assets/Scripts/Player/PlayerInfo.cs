using UnityEngine;

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
    public float Speed => speed;

    [Header("Jump")]
    [SerializeField] float baseJumpPower;
    [SerializeField] float jumpStamina;
    private float jumpPower;
    public float JumpPower => jumpPower;

    private void Awake()
    {
        health = baseHealth;
        stamina = baseStamina;
        speed = baseSpeed;
        jumpPower = baseJumpPower;
    }

    private void Update()
    {
        Rest(staminaRecovery * Time.deltaTime);
    }

    #region HP
    public void Heal(float amount)
    {
        if(health + amount > baseHealth)
        {
            health = baseHealth;
            return;
        }

        health += amount;
    }

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

    private void Die()
    {
        GameManager.Instance.GameOver();
        //Destroy(gameObject);
    }
    #endregion

    #region Stamina
    public void Rest(float amount)
    {
        if (stamina + amount > baseStamina)
        {
            stamina = baseStamina;
            return;
        }

        stamina += amount;
    }

    public bool CanJump()
    {
        if(stamina < jumpStamina)
        {
            return false;
        }

        stamina -= jumpStamina;
        return true;
    }

    public bool CanRun()
    {
        if (stamina < runStamina)
        {
            return false;
        }

        stamina -= runStamina;
        return true;
    }
    #endregion

    #region Speed
    public void SetSpeed(float multiplier)
    {
        speed *= multiplier;
    }

    public void SetSpeedOrigin()
    {
        speed = baseSpeed;
    }

    public void SetSpeedRun()
    {
        speed = runSpeed;
    }
    #endregion

    #region Jump
    public void SetJumpPower(float multiplier)
    {
        jumpPower *= multiplier;
    }

    public void SetJumpPowerOrigin()
    {
        jumpPower = baseJumpPower;
    }
    #endregion
}
