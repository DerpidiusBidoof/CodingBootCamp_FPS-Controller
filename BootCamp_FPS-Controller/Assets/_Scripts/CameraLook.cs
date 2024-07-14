using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public InputManager inputManager; // Reference to the input manager for handling player input


    public float mouseSensitivity = 75f;
    public Transform body;

    private float xRoataion = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        // Read mouse input values from the input manager
        float mouseX = inputManager.inputMaster.CameraLook.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.inputMaster.CameraLook.MouseY.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        // Adjust the xRotation based on mouse Y input, clamped to restrict vertical rotation
        xRoataion -= mouseY;
        xRoataion = Mathf.Clamp(xRoataion, -90f, 90f);

        // Rotate the camera locally based on xRotation
        transform.localRotation = Quaternion.Euler(xRoataion, 0f, 0f);

        // Rotate the player's body horizontally based on mouse X input
        body.Rotate(Vector3.up * mouseX);
    }
}
