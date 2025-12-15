using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform nextRoom;
    [SerializeField] private Transform lastRoom;
    [SerializeField] private CameraMovement cam;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Player" && collision.transform.position.x < transform.position.x)
        {
            cam.MoveToNewRoom(nextRoom);
        }
        else if (collision.tag == "Player" && collision.transform.position.x > transform.position.x)
        {
            cam.MoveToNewRoom(lastRoom);
        }
    }

}
// ollie is cool