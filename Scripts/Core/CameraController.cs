using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX; //to tell which postion the camera has to go 
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player; //Refrence to the object the camera is following 
    [SerializeField] private float aheadDistance; //How far the camera allows you to look fowrad 
    [SerializeField] private float cameraSpeed; //Speed of the camera 
    private float lookAhead;

    private void Update()
    {
        //Room camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z), ref velocity, speed);

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        //Follow player
        //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom) //Moves camera to the room the player is in 
    {
        currentPosX = _newRoom.position.x;
    }
}
