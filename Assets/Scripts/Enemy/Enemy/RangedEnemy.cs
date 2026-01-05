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
    [SerializeField] private AudioClip fireballHit;
    [SerializeField] private Health playerHealth;
    private float cooldownTimer = 0;
    private BoxCollider2D box;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;



    private Animator anim;
    RangedPatrol patrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        patrol = GetComponent<RangedPatrol>();
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
            AudioManager.instance.PlaySound(fireballHit);
            anim.SetTrigger("cast");
        }
    }
    
    //called by animation event in cast
    private void CastFireball()
    {
        int index=findFireball();
        fireballs[index].transform.position = firePoint.position;
        fireballs[index].GetComponent<EnemyProjectile>().Activate();
        patrol.enabled = false;
        StartCoroutine(EnablePatrol());
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


    private IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(attackCooldown);
        patrol.enabled = true;
    }






}
