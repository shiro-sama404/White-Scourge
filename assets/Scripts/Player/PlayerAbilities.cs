using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public float abilityRange = 1f;
    public float abilityCooldown = 5f;
    public int manaCost = 3;
    public float abilityDistanceModifier = 1f;

    private bool isAbilityReady = true;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isAbilityReady && HasEnoughMana())
        {
            UseAbility();
        }
    }

    bool HasEnoughMana()
    {
        return gameManager.playerMana >= manaCost;
    }

    void UseAbility()
    {
        gameManager.ConsumeMana(manaCost);

        InvokeChineseLetters();

        isAbilityReady = false;
        Invoke(nameof(ResetAbility), abilityCooldown);
    }

    void InvokeChineseLetters()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            GameObject letter = Instantiate(Resources.Load<GameObject>("Prefabs/ChineseLetter"), transform.position, Quaternion.identity);
            Rigidbody2D letterRb = letter.GetComponent<Rigidbody2D>();

            letterRb.velocity = direction * abilityRange * abilityDistanceModifier;

            ChineseLetter letterScript = letter.GetComponent<ChineseLetter>();
            letterScript.SetExplosion(2f, 2);
        }
    }

    void ResetAbility()
    {
        isAbilityReady = true;
    }

    public void UpgradeAbilityDistance(float modifier)
    {
        abilityDistanceModifier += modifier;
    }
}