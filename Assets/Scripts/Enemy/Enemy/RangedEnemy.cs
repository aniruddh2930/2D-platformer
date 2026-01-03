using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = 0;
    private BoxCollider2D box;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;



    private Animator anim;
    Patrol patrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        patrol = GetComponent<Patrol>();
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
            anim.SetTrigger("cast");
            fireballs[findFireball()].transform.position = firePoint.position;
            fireballs[findFireball()].GetComponent<EnemyProjectile>().Activate() ;
            patrol.enabled = false;
        }
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    //called from animationevent
    private void DamagePlayer()
    {
       
    }

    private IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(attackCooldown);
        patrol.enabled = true;
    }






}
