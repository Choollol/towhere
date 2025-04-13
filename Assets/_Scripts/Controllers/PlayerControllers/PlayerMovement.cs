using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isHyperSpeed;

    [SerializeField] private CinemachineCamera playerCamera;

    private static readonly Quaternion startRotation = Quaternion.Euler(new Vector3(60, 0, 0));

    private void Awake()
    {
        EventMessenger.TriggerEvent(EventKey.HideCursor);

        playerCamera.GetComponent<CinemachinePanTilt>().VirtualCamera.ForceCameraPosition(playerCamera.transform.position, 
            startRotation);

        StartCoroutine(WaitToAllowCameraInput());
    }
    private void FixedUpdate()
    {
        if (DataMessenger.GetBool(BoolKey.IsGameActive))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
            Vector3 move = playerCamera.transform.right * horizontalInput + playerCamera.transform.forward * verticalInput;

            transform.position += speed * (isHyperSpeed ? 5 : 1) * Time.deltaTime * move;
        }
    }
    private IEnumerator WaitToAllowCameraInput()
    {
        CinemachineInputAxisController cameraInputController = playerCamera.GetComponent<CinemachineInputAxisController>();
        
        cameraInputController.enabled = false;

        yield return DataMessenger.WaitForBool(BoolKey.IsGameActive, true);

        cameraInputController.enabled = true;
    }
}
