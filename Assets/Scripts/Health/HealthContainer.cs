using UnityEngine;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] private float regen;
    [SerializeField] private AudioClip pickupSound;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (collision.CompareTag("Player") && !(health.currentHealth == 3))
        {
            health.AddHealth(regen);
            AudioManager.instance.PlaySound(pickupSound);
            gameObject.SetActive(false);
        }
    }
    
}
