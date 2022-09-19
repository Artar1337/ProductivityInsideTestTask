using UnityEngine;

// user input check and moving player controller

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private CameraFollow followingCamera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        followingCamera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.enabled)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);
        controller.Move(move * PlayerStats.instance.Speed * Time.deltaTime);
        followingCamera.UpdatePosition(transform.position);
    }
}
