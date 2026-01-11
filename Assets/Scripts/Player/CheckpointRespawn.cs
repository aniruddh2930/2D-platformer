using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointHit;
    public Transform currentRespawn { get; private set; }
    private Health playerHealth;
    private UIManager uiManager;
    [SerializeField] Vessel vessel;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager=FindFirstObjectByType<UIManager>();
    }
    public void Respawn()
    {
        if (currentRespawn == null)
        {
            uiManager.GameOver();
        }

        else
        {
            transform.position = currentRespawn.position;
            playerHealth.Respawn();

            //make sure the checkpoint object is a child of the room it is in
            Camera.main.GetComponent<CameraMovement>().MoveToNewRoom(transform.parent);
        }
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
