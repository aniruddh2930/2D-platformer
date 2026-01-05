using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private bool hit = false;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private Rigidbody2D body;
    [NonSerialized] public float direction=0.0f;
    private float cooldown;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider.enabled = true;
    }

    private void OnEnable()
    {
        cooldown = 0.0f;
        boxCollider.enabled = true; 
        hit = false;
        transform.localScale = new Vector3(0.7f*direction, 0.7f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown>lifetime)
        {
            Deactivate();
        }

        if (hit)
        {
            body.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            body.linearVelocity = new Vector2(direction * speed, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;
        hit = true;
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    private void Deactivate() 
    {
        gameObject.SetActive(false);
    }


}
