using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    // Events
    public event Action<IInteractable> OnInteractableObjectFound;

    [SerializeField] private float range = 1f;

    private IInteractable detectedInteractableObject;
    private PlayerInputActions playerInput;
    private InputAction interact;

    void Awake()
    {
        playerInput = new PlayerInputActions();
        GetComponentInParent<MovementController>().onCursorPositionChange += ChangePosition;
    }

    private void OnEnable()
    {
        interact = playerInput.Player.Interact;
        playerInput.Player.Interact.canceled += Interact;
        interact.Enable();

    }
    private void OnDisable()
    {
        interact.Disable();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if(detectedInteractableObject != null)
        {
            detectedInteractableObject.Interact();
        }
    }

    private void ChangePosition(Vector3 position)
    {
        position = new Vector3(position.x - transform.root.position.x, 0f, position.z - transform.root.position.z);
        position = Vector3.ClampMagnitude(position, range);
        position = new Vector3((transform.root.position.x + position.x), 0.5f, transform.root.position.z + position.z);

        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            detectedInteractableObject = interactable;
            OnInteractableObjectFound.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            detectedInteractableObject = null;
            OnInteractableObjectFound.Invoke(null);
        }
    }
}
