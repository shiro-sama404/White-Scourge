using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // configurações do ataque padrão
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public int manaRestoreOnHit = 1;

    // configurações da habilidade das letras chinesas
    public GameObject chineseLetterPrefab;
    public float letterSpeed = 10f;
    public float explosionRadius = 2f;
    public int explosionDamage = 2;
    public float abilityCooldown = 5f;
    public int manaCost = 3; 

    private bool isAbilityReady = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformStandardAttack();
        }

        if (Input.GetKeyDown(KeyCode.E) && isAbilityReady && HasEnoughMana())
        {
            UseChineseLettersAbility();
        }
    }

    void PerformStandardAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            GameManager.Instance.RestoreMana(manaRestoreOnHit);
        }
    }

    void UseChineseLettersAbility()
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
    bool HasEnoughMana()
    {
        return GameManager.Instance.playerMana >= manaCost;
    }
    void ResetAbility()
    {
        isAbilityReady = true;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}