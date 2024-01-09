using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Transform cam;
    public InputActionAsset playerControls;
    public CharacterController controller;
    
    private InputAction movement;
    private InputAction take;
    private InputAction inventory;
    private InputAction sprint;
    
    private IInteractable currentInteractable;
    
    [SerializeField] GameObject inventoryUI;

    private Animator _playerAnimator;
    
    private Vector3 direction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    
    [SerializeField] private float gravity = 9.81f;
    private Vector3 velocity;

    [SerializeField] private float sprintSpeed = 8f; // Adjust the sprinting speed as needed
    private bool isSprinting = false;
    
    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        movement = playerControls.FindAction("Move");
        take = playerControls.FindAction("Take");
        inventory=playerControls.FindAction("Inventory");
        sprint = playerControls.FindAction("Sprint");
    }

    private void Start()
    {
        inventoryUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        //_playerAnimator.SetFloat("Horizontal", moveDirection.x);
        //_playerAnimator.SetFloat("Vertical", moveDirection.z);
        _playerAnimator.SetFloat("Speed", direction.sqrMagnitude);


        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate horizontal movement
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.fixedDeltaTime);

            // Apply gravity to vertical movement
            velocity.y += -gravity * Time.fixedDeltaTime;
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }
    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        var horizontal = input.x;
        var vertical = input.y;

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (controller.isGrounded)
        {
            velocity.y = 0f;
        }
    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }
    private void OnEnable()
    {
        movement.Enable();
        movement.performed += OnMovement;
        movement.canceled += OnMovement;
        sprint.Enable();
        sprint.performed += OnSprint;
        sprint.canceled += OnSprint;
        take.Enable();
        take.performed += OnTake;
        inventory.Enable();
        inventory.performed += OnInventory;
    }
    
    private void OnDisable()
    {
        movement.Disable();
        movement.performed -= OnMovement;
        sprint.Disable();
        sprint.performed -= OnSprint;
        take.Disable();
        take.performed -= OnTake;
        inventory.Disable();
        inventory.performed -= OnInventory;

    }
    
    private void OnTake(InputAction.CallbackContext context)
    {
        currentInteractable?.Interact();
        
    }
    private void OnInventory(InputAction.CallbackContext context)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(typeof(IInteractable), out var outCurrentInteractable)){
            currentInteractable = (IInteractable) outCurrentInteractable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(typeof(IInteractable), out var _))
        {
            currentInteractable = null;
        }
    }
}
