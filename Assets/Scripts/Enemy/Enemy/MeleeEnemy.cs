using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    private float cooldownTimer=0;
    private BoxCollider2D box;

    private BoxCollider2D meleeHitbox;

    private Animator anim;
    private Health health;
    Patrol patrol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box= GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        patrol=GetComponent<Patrol>();
        meleeHitbox = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       cooldownTimer -= Time.deltaTime;
    }

    public void PlayerInRange()
    {
        if (cooldownTimer <= 0)
        {
            cooldownTimer = attackCooldown;
            anim.SetTrigger("attack");
        }
    }


    //called from animationevent
    private void DamagePlayer()
    {
        if (Physics2D.OverlapBox(meleeHitbox.bounds.center,meleeHitbox.bounds.size,0)!=null)
        {
            health.TakeDamage(damage);
            patrol.enabled = false;
            StartCoroutine(EnablePatrol());
        }
    }

    private  IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(attackCooldown);
        patrol.enabled = true;
    }

}
