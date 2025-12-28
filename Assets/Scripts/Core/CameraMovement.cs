using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    //Room Camera
    [SerializeField] private float roomtime;
    private float positionX;
    private Vector3 velocity = Vector3.zero;

    //Player Camera
    [SerializeField] private Transform player;
    [SerializeField] private float playertime;
    [SerializeField] private float distanceAhead;

    private void Start()
    {
        //Room Camera
        //positionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(positionX, transform.position.y, transform.position.z), ref velocity, time);

        //Player Camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x+(distanceAhead*player.localScale.x), player.position.y, transform.position.z), ref velocity, playertime);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        //+7 to offset the camera to the center of the room
        positionX = newRoom.position.x+7;
    }
}
