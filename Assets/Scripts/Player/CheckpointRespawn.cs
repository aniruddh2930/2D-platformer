using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointHit;
    private Transform currentRespawn;
    private Health playerHealth;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    public void Respawn()
    {
        transform.position=currentRespawn.position;
        playerHealth.Respawn();

        //make sure the checkpoint object is a child of the room it is in
        Camera.main.GetComponent<CameraMovement>().MoveToNewRoom(transform.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint") )
        {
            currentRespawn=collision.transform;
            AudioManager.instance.PlaySound(checkpointHit);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
