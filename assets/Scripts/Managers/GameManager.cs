using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth = 4;
    public int playerMaxHealth = 4;
    public int playerMana = 5;
    public int playerMaxMana = 10; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void Heal(int amount)
    {
        playerHealth = Mathf.Min(playerHealth + amount, playerMaxHealth);
    }

    public void UpgradeHealth(int amount)
    {
        playerMaxHealth += amount;
        playerHealth = playerMaxHealth;
    }

    public void ConsumeMana(int amount)
    {
        playerMana = Mathf.Max(playerMana - amount, 0);
    }

    public void RestoreMana(int amount)
    {
        playerMana = Mathf.Min(playerMana + amount, playerMaxMana);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}