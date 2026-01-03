using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private RangedEnemy rangedEnemy;

    private void Start()
    {
        rangedEnemy = GetComponentInParent<RangedEnemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rangedEnemy.PlayerInRange();
    }
}
