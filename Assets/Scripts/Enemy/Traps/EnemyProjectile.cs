using UnityEngine;

public class EnemyProjectile : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit = false;


    [Header("only for ranged patrol")]
    [SerializeField] private Transform knight;
    private float direction;

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
        if(knight!=null)
            transform.Translate(speed * Time.deltaTime*direction,0,0);
        else
            transform.Translate(speed * Time.deltaTime,0,0);
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
        if (knight != null)
        {
            direction = knight.localScale.x;
            transform.localScale = new Vector3(direction * 0.7f, 0.7f, 0.7f);
        }
        else
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Checkpoint"))
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
