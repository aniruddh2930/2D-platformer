using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private float respawnCooldown = 1f;
    private float respawnTimer = 0f;
    private void Update()
    {
        respawnTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && respawnTimer < 0)
        {
            Respawn();
            respawnTimer = respawnCooldown;
        }
    }

    public void Respawn()
    {
        GetComponent<CheckpointRespawn>().Respawn();
        this.enabled = false;
    }
}
