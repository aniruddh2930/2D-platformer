using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject fireHitbox;
    [Header("lifetime before object dies")]
    [SerializeField] private float lifeTime;
    [SerializeField] private Behaviour[] behaviours;
    [SerializeField] private Health playerHealth;
    [Header("Fire trap timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool active;
    private bool triggered;

    [Header ("SFX")]
    [SerializeField] private AudioClip fireTrapSound;
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
        //actiavted
        triggered = true;
        sprite.color = new Color(0.79f, 0.03f, 0.03f);
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activationDelay);
        //firing
        active = true;
        sprite.color = Color.white;
        anim.SetBool("activated", false);
        anim.SetBool("fire", true);
        fireHitbox.SetActive(true);
        AudioManager.instance.PlaySound(fireTrapSound);
        yield return new WaitForSeconds(activeTime);
        //stop firing
        active = false;
        anim.SetBool("fire", false);
        fireHitbox.SetActive(false);
        triggered = false;
    }

    private void Update()
    {
        if (!playerHealth.dead)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
                Deactivate();
        }
    }

    private void Deactivate()
    {
      this.enabled= false;
      foreach (Behaviour component in behaviours)
        {
            component.enabled = false;
        }
    }
}
