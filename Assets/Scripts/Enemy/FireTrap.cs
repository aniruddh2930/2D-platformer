using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject fireHitbox;
    [Header("Fire trap timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool active;
    private bool triggered;
    //make it read only
    public float Damage => damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Player"))
        {
            if (active)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            if (!triggered)
            {
                StartCoroutine(ActiveTrap());
            }
        }

    }

    private IEnumerator ActiveTrap()
    {
        triggered = true;
        sprite.color = new Color(0.79f, 0.03f, 0.03f);
        yield return new WaitForSeconds(activationDelay);
        active = true;
        fireHitbox.SetActive(true);
        yield return new WaitForSeconds(activeTime);
        sprite.color= Color.white;
        active = false;
        fireHitbox.SetActive(false);
        triggered = false;
    }
}
