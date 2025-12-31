using UnityEngine;

public class EnemyProjectile : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime,0,0);
        lifetime -= Time.deltaTime;  
        if (lifetime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        lifetime = resetTime;
        gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            
        base.OnTriggerStay2D(collision);   
        gameObject.SetActive(false);
        
    }
}
