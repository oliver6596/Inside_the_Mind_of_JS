using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndRotation : MonoBehaviour {

    public int moveSpeed = 80;
    public Rigidbody playerRigidbody;   // Reference to the player's rigidbody.
    int floorMask;
    Vector3 newPos;
    Vector2 inputVector;
    public float movePower = 10f;
    public float slowPower = .4f;

    // Use this for initialization
    void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Vertical") != 0.0f)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed * Input.GetAxis("Vertical")));
        }

        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed * Input.GetAxis("Horizontal")));
        }

        if (Input.GetAxis("VerticalR") != 0.0f)
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * moveSpeed * Input.GetAxis("VerticalR")));
        }

        if (Input.GetAxis("HorizontalR") != 0.0f)
        {
            transform.Rotate(Vector3.right * (Time.deltaTime * moveSpeed * Input.GetAxis("HorizontalR")));
        }

        //Runs racast function. 
        Turning();
    }

    void FixedUpdate()
    {
        // Runs runs every frame.
        newPos = transform.position;
        newPos += transform.right * inputVector.x;
        newPos += transform.forward * inputVector.y;

        playerRigidbody.AddForce((newPos - transform.position)*movePower, ForceMode.Impulse);
        if (playerRigidbody.velocity.sqrMagnitude > 100)
        {
            playerRigidbody.velocity *= slowPower;
        }
    }

    private void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("HitBox");
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

}
