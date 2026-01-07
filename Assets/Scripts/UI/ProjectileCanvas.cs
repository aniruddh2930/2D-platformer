using System;
using UnityEngine;

public class ProjectileCanvas : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    private bool hit = false;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private Rigidbody2D body;
    [NonSerialized] public float direction = 0.0f;
    private float cooldown;
    private RectTransform rectTransform;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider.enabled = true;
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        cooldown = 0.0f;
        boxCollider.enabled = true;
        hit = false;
        
        rectTransform.localScale = new Vector3(0.7f * direction, 0.7f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown > lifetime)
        {
            Deactivate();
        }

        if (hit)
        {
            return;
        }
        else
        {
            rectTransform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}


