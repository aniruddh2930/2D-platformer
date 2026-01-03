using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead = false;
    private bool invunerable = false;

    [Header("Iframes")]
    [SerializeField] float iFrameDuration;
    //number of times player turns red when taking dameage
    [SerializeField] int numberOfFlashes;
    private SpriteRenderer sprite;
    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (dead || invunerable) return;

        if (currentHealth-damage > 0)
        {
            invunerable = true;
            currentHealth -= damage;
            anim.SetTrigger("hurt");
            StartCoroutine("Invunerability");
        }
        else 
        {
            currentHealth = 0;
            //player
            if (GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;
            if (GetComponent<PlayerAttack>() != null)
                GetComponent<PlayerAttack>().enabled = false;
            if (GetComponent<PlayerAttack>() != null)
                GetComponent<Animator>().SetBool("jump", false);
            anim.SetTrigger("die");
            dead = true;

            //enemy
            if (GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().StopAllCoroutines();
                GetComponent<MeleeEnemy>().enabled = false;
            if (GetComponent<PlayerAttack>() != null)
                GetComponentInChildren<MeleeHitbox>().enabled = false;
            if(GetComponent<Patrol>() != null)
                GetComponent<Patrol>().enabled = false;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    public void AddHealth(float regen)
    {
        currentHealth += regen;
    }

    private IEnumerator Invunerability()
    {
        //player 8 enemy 9
        Physics2D.IgnoreLayerCollision(8, 9,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            sprite.color = new Color(1, 0, 0, 0.7f);
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlashes*2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invunerable = false;
    }
}



