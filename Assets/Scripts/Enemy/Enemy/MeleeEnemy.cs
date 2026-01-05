using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private Health playerHealth;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer=0;
    private BoxCollider2D box;

    [Header("Melee Hitbox")]
    [SerializeField] private BoxCollider2D meleeHitbox;


    private Animator anim;
    MeleePatrol patrol;
    [SerializeField] private AudioClip swordHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box= GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        patrol=GetComponent<MeleePatrol>();
    }

    // Update is called once per frame
    void Update()
    {
       cooldownTimer -= Time.deltaTime;
    }

    public void PlayerInRange()
    {
        if (cooldownTimer <= 0 && playerHealth.currentHealth>0)
        {
            cooldownTimer = attackCooldown;
            AudioManager.instance.PlaySound(swordHit);
            anim.SetTrigger("attack");
            patrol.enabled = false;
        }
    }


    //called from animationevent
    private void DamagePlayer()
    {
        if (Physics2D.OverlapBox(meleeHitbox.bounds.center,meleeHitbox.bounds.size,0,playerLayer)!=null)
        {
            Physics2D.OverlapBox(meleeHitbox.bounds.center, meleeHitbox.bounds.size, 0,playerLayer).GetComponent<Health>().TakeDamage(damage);
        }
        StartCoroutine(EnablePatrol());
    }

    private  IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(attackCooldown);
        patrol.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meleeHitbox.bounds.center, meleeHitbox.bounds.size);
    }

}
