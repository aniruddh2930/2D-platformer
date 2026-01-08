using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] GameObject nextRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nextRoom.GetComponent<Reset>().ActivateRoom(true);
            gameObject.SetActive(false);
        }
    }
}
