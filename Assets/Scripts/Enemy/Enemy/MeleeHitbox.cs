using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private MeleeEnemy meleeEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider2D hitbox;
    private void Start()
    {
        meleeEnemy = GetComponentInParent<MeleeEnemy>();
        hitbox = GetComponent<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        meleeEnemy.PlayerInRange();
        
    }
    public float GetRange()
    {
        return hitbox.bounds.size.x;
    }
}
