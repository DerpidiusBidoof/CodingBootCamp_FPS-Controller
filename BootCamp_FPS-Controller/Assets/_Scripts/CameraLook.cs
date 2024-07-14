using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public InputManager inputManager;

    public float mouseSensitivity = 75f;
    public Transform body;

    private float xRoataion = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = inputManager.inputMaster.CameraLook.MouseX.ReadValue<float>() * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.inputMaster.CameraLook.MouseY.ReadValue<float>() * mouseSensitivity * Time.deltaTime;

        xRoataion -= mouseY;
        xRoataion = Mathf.Clamp(xRoataion, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRoataion, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
