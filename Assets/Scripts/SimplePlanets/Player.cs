using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;  // movement speed of the character
    public float mouseSensitivity = 100f;  // sensitivity of the mouse look

    private Rigidbody rb;  // reference to the Rigidbody component
    private Transform cameraTransform;  // reference to the camera transform
    private float xRotation = 0f;  // current rotation around the x-axis

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // get the Rigidbody component from the character
        cameraTransform = Camera.main.transform;  // get the transform of the main camera
        Cursor.lockState = CursorLockMode.Locked;  // lock the cursor to the center of the screen
    }

    void Update()
    {
        // get horizontal and vertical input (AWSD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // get up and down input (space and left shift)
        float upDown = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            upDown = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            upDown = -1f;
        }

        // create a movement vector and apply it to the character's Rigidbody component
        Vector3 movement = transform.right * horizontal + transform.forward * vertical + transform.up * upDown;
        rb.velocity = movement * moveSpeed;

        // get mouse input and rotate the character and camera pivot accordingly
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y + mouseX, 0f);  // rotate the character left and right
    }
}
