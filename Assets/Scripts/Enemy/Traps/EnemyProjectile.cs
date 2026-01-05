using UnityEngine;

public class EnemyProjectile : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit = false;
    [SerializeField] private Transform knight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        if(hit)
            return;
        transform.Translate(speed * Time.deltaTime*knight.localScale.x,0,0);
        lifetime -= Time.deltaTime;  
        if (lifetime <= 0)
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        hit= false;
        lifetime = resetTime;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
        transform.localScale = new Vector3(knight.localScale.x*0.7f, 0.7f, 0.7f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            return;
        hit = true;
        OnTriggerStay2D(collision);
        if (anim == null)
        {
            Deactivate();
            boxCollider.enabled = false;
        }

        else
        {
            //explosion calls decativate
            anim.SetTrigger("explode");
            boxCollider.enabled = false;
        }
    }
}
