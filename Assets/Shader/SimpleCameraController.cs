using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float fastSpeed = 25f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 0.2f;

    private Vector2 lookInput;
    private Vector2 moveInput;
    private float verticalInput;

    private float rotX;
    private float rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = transform.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    void Update()
    {
        MouseLook();
        Movement();
    }

    // Called by Input System
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnVertical(InputValue value)
    {
        verticalInput = value.Get<float>();
    }

    public void MouseLook()
    {
        rotY += lookInput.x * mouseSensitivity;
        rotX -= lookInput.y * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
    }

    public void Movement()
    {
        float speed = Keyboard.current.leftShiftKey.isPressed ? fastSpeed : moveSpeed;

        Vector3 dir = (transform.forward * moveInput.y)
                    + (transform.right * moveInput.x)
                    + (transform.up * verticalInput);

        transform.position += dir * speed * Time.deltaTime;
    }
}