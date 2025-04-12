using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isHyperSpeed;

    private void Awake()
    {
        EventMessenger.TriggerEvent(EventKey.HideCursor);
    }
    private void FixedUpdate()
    {
        if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

            transform.position += speed * (isHyperSpeed ? 5 : 1) * Time.deltaTime * move;
        }
    }
}
