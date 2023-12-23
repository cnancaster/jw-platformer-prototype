using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jumpForce = 8.0f;
    public bool isGrounded;

    private void Start()
    {
        // Lock the mouse cursor at the beginning
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get input from the player
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player based on mouse input
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Calculate movement direction
        Vector3 moveDirection = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);

        // Apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Rotate the camera based on mouse input (up and down)
        Camera.main.transform.Rotate(Vector3.left * mouseY * sensitivity);

        // Check for jumping input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Toggle mouse lock when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMouseLock();
        }
    }

    private void Jump()
    {
        // Apply jump force
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player is grounded
        isGrounded = true;
    }

    private void ToggleMouseLock()
    {
        // Toggle mouse lock and visibility
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }
}
