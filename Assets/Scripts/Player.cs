using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed { get; set; }  // movement speed of the character

    [Header("SpaceShip Movement")]
    [SerializeField] private float fastSpeed = 100f;
    [SerializeField] private float cruiseSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;

    [Header("Mouse Movement")]
    [SerializeField] private float mouseSensitivity = 10f;  // sensitivity of the mouse look

    private Rigidbody rb;  // reference to the Rigidbody component
    private float xRotation = 0f;  // current rotation around the x-axis

    [Header("Particle Effect Systems")]
    [SerializeField] ParticleSystem thrusrterPS;
    [SerializeField] ParticleSystem thrusrterPSUR;
    [SerializeField] ParticleSystem thrusrterPSUL;
    [SerializeField] ParticleSystem thrusrterPSDR;
    [SerializeField] ParticleSystem thrusrterPSDL;

    ParticleSystem.EmissionModule _emission;
    ParticleSystem.EmissionModule _emissionDR;
    ParticleSystem.EmissionModule _emissionDL;
    ParticleSystem.EmissionModule _emissionUR;
    ParticleSystem.EmissionModule _emissionUL;

    private int idleThrusterPartCount = 2;
    private int cruiseThrusterPartCount = 6;
    private int fastThrusterPartCount = 13;

    private bool boostActive = false;
    private bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();  // get the Rigidbody component from the character
        Cursor.lockState = CursorLockMode.Locked;  // lock the cursor to the center of the screen
        moveSpeed = cruiseSpeed;

        _emission = thrusrterPS.emission;
        _emissionDR = thrusrterPSDR.emission;
        _emissionDL = thrusrterPSDL.emission;
        _emissionUR = thrusrterPSUR.emission;
        _emissionUL = thrusrterPSUL.emission;

        _emission.rateOverTime = idleThrusterPartCount;
        _emissionUR.rateOverTime = 0;
        _emissionUL.rateOverTime = 0;
        _emissionDR.rateOverTime = 0;
        _emissionDL.rateOverTime = 0;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            boostActive = true;
            moveSpeed = fastSpeed;
            canMove = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            boostActive = false;
            moveSpeed = cruiseSpeed;
            canMove = true;
        }


        switch (Input.GetKey(KeyCode.LeftControl))
        {
            case true:
                Vector3 movement = -transform.up * verticalSpeed;
                rb.AddForce(movement, ForceMode.VelocityChange);
                _emissionDR.rateOverTime = 5;
                _emissionDL.rateOverTime = 5;
                break;
            case false:
                _emissionDR.rateOverTime = 0;
                _emissionDL.rateOverTime = 0;
                break;
        }

        switch (Input.GetKey(KeyCode.Space))
        {
            case true:
                Vector3 movement = transform.up * verticalSpeed;
                rb.AddForce(movement, ForceMode.VelocityChange);
                _emissionUR.rateOverTime = 5;
                _emissionUL.rateOverTime = 5;
                break;
            case false:
                _emissionUR.rateOverTime = 0;
                _emissionUL.rateOverTime = 0;
                break;
        }

        // get horizontal and vertical input (AWSD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (canMove)
        {
            // create a movement vector and apply it to the character's Rigidbody component
            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
            rb.velocity = movement * moveSpeed;
        }
        else if (boostActive && vertical > 0)
        {
            Vector3 movement = transform.forward * vertical;
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (boostActive && vertical > 0)
        {
           _emission.rateOverTime = fastThrusterPartCount;
        }
        else if (vertical == 0)
        {
            _emission.rateOverTime = idleThrusterPartCount;
        }
        else
        {
            _emission.rateOverTime = cruiseThrusterPartCount;
        }
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y + mouseX, 0f);  // rotate the character left and right
    }
}


