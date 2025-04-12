using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float sensitivity = 700f;
    private float xRotation = 0f;

    private Transform playerTransform;

    private Vector2 currentMouseDelta;
    [SerializeField] private bool smoothTurnToggle;
    private const float smoothTime = 0.1f;
    private Vector2 smoothInputVelocity = Vector2.zero;

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
                BasicLook();
            }
            else
            {
                SmoothLook();
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
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;

        smoothInputVelocity = Vector2.zero;
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref smoothInputVelocity, smoothTime);

        xRotation = Mathf.Clamp(xRotation - currentMouseDelta.y, -90f, 90f);

        playerTransform.Rotate(Vector3.up * currentMouseDelta.x);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
