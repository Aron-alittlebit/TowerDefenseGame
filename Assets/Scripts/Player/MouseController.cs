using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float MouseSensitivity = 250f;
    float xRotation = 0f;
    float yRotattion = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;

        xRotation -= mouseY;
        yRotattion += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation,yRotattion,0f);
    }
}
