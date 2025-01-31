using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public Vector2 sensitivity = Vector2.one * 360f;
    public float moveSpeed = 10f;

    bool freeCam = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            freeCam = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonUp(1))
        {
            freeCam = false;
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (freeCam) 
        {
            rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity.x;
            rotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * -1 * sensitivity.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift)) { moveSpeed = 20f; } else { moveSpeed = 10f; }

        // Get the input axis values
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Move the camera
        transform.Translate(movement * moveSpeed * Time.deltaTime);

    }
}