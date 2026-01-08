    using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject cam;
    [Header("Enemies in current room")]
    [SerializeField] private GameObject[] enemiesDead;
    private int deadEnemies = 0;

    private void Update()
    {
        foreach (GameObject enemy in enemiesDead)
        {
            if (enemy.activeInHierarchy == true)
                return;
            deadEnemies++;
        }

        if (deadEnemies == enemiesDead.Length)
        {
            StartCoroutine(DisableDoor());
            this.enabled = false;
        }
    }
    private System.Collections.IEnumerator DisableDoor()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            if(i%2==0)
                cam.transform.Translate(new Vector3(-1,0,0));
            else
                cam.transform.Translate(new Vector3(1,0,0));
            transform.Translate(new Vector3(0,-0.5f, 0));
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Player")
        {
            nextRoom.GetComponent<Reset>().ActivateRoom(true);
            gameObject.SetActive(false);
        }
    }

}
