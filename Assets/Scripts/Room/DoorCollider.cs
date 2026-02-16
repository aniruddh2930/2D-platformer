using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] GameObject nextRoom;
    [SerializeField] private Door door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            nextRoom.GetComponent<Reset>().ActivateRoom(true);
            door.EnableDoor();
            gameObject.SetActive(false);
        }
    }
}
