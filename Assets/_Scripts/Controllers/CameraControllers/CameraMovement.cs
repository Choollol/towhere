using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private float sensitivity = 600f;
    private float xRotation = 60f;

    private Transform playerTransform;

    [SerializeField] private bool smoothTurnToggle;

    private void Start()
    {
        playerTransform = transform.parent;
    }

    private void LateUpdate()
    {
        if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            if (smoothTurnToggle)
            {
                SmoothLook();
            }
            else
            {
                BasicLook();
            }
        }
    }
    private void BasicLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
    private void SmoothLook()
    {
        
    }
}
