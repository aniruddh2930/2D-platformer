    using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject nextRoom;
    [SerializeField] private GameObject lastRoom;
    [SerializeField] private CameraMovement cam;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Player" && collision.transform.position.x < transform.position.x)
        {
            nextRoom.GetComponent<Reset>().ActivateRoom(true);
            lastRoom.GetComponent<Reset>().ActivateRoom(false);
            cam.MoveToNewRoom(nextRoom.transform);
        }
        else if (collision.tag == "Player" && collision.transform.position.x > transform.position.x)
        {
            nextRoom.GetComponent<Reset>().ActivateRoom(false);
            lastRoom.GetComponent<Reset>().ActivateRoom(true);
            cam.MoveToNewRoom(lastRoom.transform);
        }
    }

}
