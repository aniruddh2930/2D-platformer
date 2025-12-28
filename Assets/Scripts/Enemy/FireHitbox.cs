using UnityEngine;

public class FireHitbox : MonoBehaviour
{
    [SerializeField] private FireTrap fireTrap;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             collision.gameObject.GetComponent<Health>().TakeDamage(fireTrap.Damage);
        }
    }
}
