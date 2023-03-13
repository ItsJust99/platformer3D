using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //cam movment
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _camLimitMin;
    [SerializeField] private float _camLimitMax;
    private float _camAngle = 0.0f;
    //movement
    [SerializeField] private float _speed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private KeyCode _sprintKey;
    private Rigidbody _rb;
    //jump
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKey;
    //courch
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _crouchYScale;
    private float _startYScale;
    [SerializeField] private KeyCode _crouchKey;
    // interct
    [SerializeField] private KeyCode _interactKey;
    [SerializeField] private float _interactRange;
    //ability
    [SerializeField] private KeyCode _abilityKey;
    [SerializeField] private Ability _ability;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _startYScale = transform.localScale.y;
    }
    private void Update()
    {
        RotateEyes();
        RotateBody();
        StateHandler();

        //Debug.Log(IsGrounded());

        if (Input.GetKeyDown(_jumpKey))
        {
            TryJump();
        }
        if (Input.GetKeyDown(_interactKey))
        {
            TryInteract();
        }
        if (Input.GetKeyDown(_abilityKey))
        {
            _ability.Use(this);
        }
        //start crouch
        if (Input.GetKeyDown(_crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, _crouchYScale, transform.localScale.z);
            _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //stop crouch
        if (Input.GetKeyUp(_crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, _startYScale, transform.localScale.z);
        }

    }
    private void FixedUpdate()
    {
        Move();
    }

    private void RotateEyes()
    {
        float yMouse = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _camAngle -= yMouse;
        _camAngle = Mathf.Clamp(_camAngle, _camLimitMin, _camLimitMax);
        _eyes.localRotation = Quaternion.Euler(_camAngle, 0, 0);
    }
    private void RotateBody()
    {
        float xMouse = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xMouse);
    }
    private void Move()
    {
        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * xDir + transform.forward * zDir;

        _rb.velocity = new Vector3(0, _rb.velocity.y, 0) + dir.normalized * _speed;
    }
    private void TryJump()
    {
        if (IsGrounded())
        {
            Jump(_jumpForce);
        }
    }
    private void Jump(float jumpForce)
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, 1.1f);
    }
    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(_eyes.position, _eyes.forward, out hit, _interactRange))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    private void StateHandler()
    {
        //mode - crouching
        if (Input.GetKey(_crouchKey))
        {
            state = MovementState.crouching;
            _speed = _crouchSpeed;
        }
        // mode - sprinting
        else if (IsGrounded() && Input.GetKey(_sprintKey))
        {
            state = MovementState.sprinting;
            _speed = _sprintSpeed;
        }
        //mode - walking
        else if (IsGrounded())
        {
            state = MovementState.walking;
            _speed = _walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
}   
