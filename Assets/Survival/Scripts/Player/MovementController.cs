using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;

    public event Action<Vector3> onCursorPositionChange;

    // Actions
    private PlayerInputActions playerInput;
    private InputAction move;

    // Self components
    private Rigidbody rb;

    // Move related
    private Vector2 moveDirection;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        move = playerInput.Player.Move;
        move.Enable();

    }
    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    Plane plane = new Plane(Vector3.up, Vector3.zero);
    float rayLength;

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * movementSpeed, 0 , moveDirection.y * movementSpeed);

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(plane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            onCursorPositionChange?.Invoke(pointToLook);
            Debug.DrawLine(transform.position, pointToLook, Color.red);
            transform.forward = new Vector3 (pointToLook.x - transform.position.x, 0f, pointToLook.z - transform.position.z);
        }
    }

}
