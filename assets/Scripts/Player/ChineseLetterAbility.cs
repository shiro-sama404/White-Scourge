using UnityEngine;

public class ChineseLettersAbility : MonoBehaviour
{
    public GameObject chineseLetterPrefab; 
    public float letterSpeed = 10f;
    public float explosionRadius = 2f;
    public int explosionDamage = 2;
    public float abilityCooldown = 5f;
    public int manaCost = 3;

    private bool isAbilityReady = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isAbilityReady && HasEnoughMana())
        {
            UseAbility();
        }
    }

    bool HasEnoughMana()
    {
        return GameManager.Instance.playerMana >= manaCost;
    }

    void UseAbility()
    {
        GameManager.Instance.ConsumeMana(manaCost);

        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            GameObject letter = Instantiate(chineseLetterPrefab, transform.position, Quaternion.identity);
            Rigidbody2D letterRb = letter.GetComponent<Rigidbody2D>();

            letterRb.velocity = direction * letterSpeed;

            ChineseLetter letterScript = letter.GetComponent<ChineseLetter>();
            letterScript.SetExplosion(explosionRadius, explosionDamage);
        }

        isAbilityReady = false;
        Invoke(nameof(ResetAbility), abilityCooldown);
    }

    void ResetAbility()
    {
        isAbilityReady = true;
    }
}