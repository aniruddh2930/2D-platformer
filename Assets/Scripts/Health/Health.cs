using System;
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
    private bool isEnemy = false;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip hurt;

    [Header("Iframes")]
    [SerializeField] float iFrameDuration;
    //number of times player turns red when taking dameage
    [SerializeField] int numberOfFlashes;
    private SpriteRenderer sprite;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
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
            currentHealth -= damage;
            anim.SetTrigger("hurt");
            AudioManager.instance.PlaySound(hurt);
            StartCoroutine("Invunerability");
        }
        else 
        {
            currentHealth = 0;
            GetComponent<Animator>().SetBool("grounded", true);
            anim.SetTrigger("die");
            AudioManager.instance.PlaySound(death);
            dead = true;

            foreach (Behaviour component in components)
            {
                if (component.name=="Knight")
                {
                    isEnemy = true;
                }
                component.enabled = false;
            }
            if(!isEnemy)
            {
                StartCoroutine(EnableRespawn());
            }
        }

    }

    private IEnumerator DespawnEnemy()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);

    }

    private IEnumerator EnableRespawn()
    {
        yield return new WaitForSeconds(2.0f);
        GetComponent<PlayerRespawn>().enabled = true;
    }

    public void AddHealth(float regen)
    {
        currentHealth += regen;
    }

    private IEnumerator Invunerability()
    {
        //player 8 enemy 9
        invunerable = true;
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

    public void Respawn()
    {
        dead = false;
        AddHealth(maxHealth);
        GetComponent<Animator>().SetBool("grounded", true);
        anim.Play("Player_respawn");
    }

    //animation event
    private void Deactivate()
    {
        transform.parent.gameObject.SetActive(false);
    }

    //called by Player_respawn animation event
    private void EnableComponents()
    {
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
        StartCoroutine(Invunerability());
    }
}



