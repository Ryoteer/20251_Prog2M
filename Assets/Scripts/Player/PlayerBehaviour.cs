using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : EntityBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _airBoolName = "isOnAir";
    [SerializeField] private string _attackTriggerName = "onAttack";
    [SerializeField] private string _interactTriggerName = "onInteract";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _moveBoolName = "isMoving";
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _interactKey = KeyCode.F;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _groundRayDistance = 0.25f;
    [SerializeField] private LayerMask _groundRayMask;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;

    private bool _isGrounded = true;

    private Vector3 _dir = new(), _posOffset = new();

    private Animator _animator;
    private Rigidbody _rb;

    private Ray _groundRay;

    public override void Awake()
    {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();

        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public override void Update()
    {
        base.Update();

        _dir.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xAxisName, _dir.x);
        _dir.z = Input.GetAxis("Vertical");
        _animator.SetFloat(_zAxisName, _dir.z);

        _isGrounded = IsGrounded();

        Debug.Log($"Is grounded? {_isGrounded}.");

        _animator.SetBool(_airBoolName, !_isGrounded);
        _animator.SetBool(_moveBoolName, _dir.sqrMagnitude != 0.0f);

        if (Input.GetKeyDown(_attackKey))
        {
            _animator.SetTrigger(_attackTriggerName);
        }
        else if (Input.GetKeyDown(_interactKey))
        {
            _animator.SetTrigger(_interactTriggerName);
        }
        else if (Input.GetKeyDown(_jumpKey) && _isGrounded)
        {
            _animator.SetTrigger(_jumpTriggerName);
            Jump();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(_dir.sqrMagnitude != 0.0f)
        {
            Movement(_dir);
        }
    }

    public void Attack()
    {
        Debug.Log($"Player attack.");
    }

    public void Interact()
    {
        Debug.Log($"Player interact.");
    }

    private bool IsGrounded()
    {
        _posOffset = new Vector3(transform.position.x,
                                 transform.position.y + (_groundRayDistance / 3.33f),
                                 transform.position.z);

        _groundRay = new Ray(_posOffset, -transform.up);

        return Physics.Raycast(_groundRay, _groundRayDistance, _groundRayMask);
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir.normalized * _moveSpeed * Time.fixedDeltaTime);
    }
}
