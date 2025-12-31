using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
