using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : EntityBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _airBoolName = "isOnAir";
    [SerializeField] private string _attackTriggerName = "onAttack";
    [SerializeField] private string _deathTriggerName = "onDeath";
    [SerializeField] private string _interactTriggerName = "onInteract";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _moveBoolName = "isMoving";
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";

    [Header("Camera")]
    [SerializeField] private Transform _playerHead;
    public Transform PlayerHead 
    { 
        get { return _playerHead; } 
    }

    [Header("General")]
    [SerializeField] private int _dmg = 10;

    [Header("Inputs")]
    [SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _interactKey = KeyCode.F;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _menuKey = KeyCode.Escape;
    [SerializeField] private KeyCode _resetKey = KeyCode.R;

    [Header("Physics")]
    [SerializeField] private float _attackDistance = 5.0f;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private float _attackRadius = 1.0f;
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _interactDistance = 1.0f;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private float _interactRadius = 0.33f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private Transform _rayOrigin;

    private bool _isGrounded = true;

    private Vector3 _dir = new(), _dirFix = new(), _camForwardFix = new(), _camRightFix = new(), _posOffset = new();

    private Animator _animator;
    private CameraController _cam;
    private Rigidbody _rb;
    private Transform _camTransform;

    private Ray _attackRay, _groundRay, _interactRay;
    private RaycastHit _attackHit, _interactHit;

    protected override void Awake()
    {
        GameManager.Instance.Player = this;

        base.Awake();

        _animator = GetComponentInChildren<Animator>();

        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.ActualCheckpointPosition = transform.position;

        _camTransform = Camera.main.transform;
        _cam = Camera.main.GetComponentInParent<CameraController>();
    }

    protected override void Update()
    {
        base.Update();

        _dir.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xAxisName, _dir.x);
        _dir.z = Input.GetAxis("Vertical");
        _animator.SetFloat(_zAxisName, _dir.z);

        _isGrounded = IsGrounded();

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

        if (Input.GetKeyDown(_menuKey))
        {
            AsyncLoadManager.Instance.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(_resetKey))
        {
            transform.position = GameManager.Instance.ActualCheckpointPosition;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(_dir.sqrMagnitude != 0.0f)
        {
            Movement(_dir);
        }
    }

    public void Attack()
    {
        _attackRay = new Ray(_rayOrigin.position, transform.forward);

        if(Physics.SphereCast(_attackRay, _attackRadius, out _attackHit, _attackDistance, _attackMask))
        {
            if(_attackHit.collider.TryGetComponent(out EnemyBehaviour entity))
            {
                entity.TakeDamage(_dmg);
            }
        }
    }

    protected override void Death()
    {
        base.Death();

        _animator.SetTrigger(_deathTriggerName);
    }

    public void Interact()
    {
        _interactRay = new Ray(_rayOrigin.position, transform.forward);

        if(Physics.SphereCast(_interactRay, _interactRadius, out _interactHit, _interactDistance, _interactMask))
        {
            if(_interactHit.collider.TryGetComponent(out IInteraction interaction))
            {
                interaction.OnInteraction();
            }
        }
    }

    private bool IsGrounded()
    {
        _posOffset = new Vector3(transform.position.x,
                                 transform.position.y + 0.1f,
                                 transform.position.z);

        _groundRay = new Ray(_posOffset, -transform.up);

        return Physics.Raycast(_groundRay, _groundDistance, _groundMask);
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(Vector3 dir)
    {
        _camForwardFix = _camTransform.forward;
        _camRightFix = _camTransform.right;

        _camForwardFix.y = 0.0f;
        _camRightFix.y = 0.0f;

        Rotate(_camForwardFix);

        _dirFix = (_camRightFix * dir.x + _camForwardFix * dir.z).normalized;

        _rb.MovePosition(transform.position + _dirFix * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate(Vector3 dir)
    {
        transform.forward = dir;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(_posOffset, _groundRay.direction * _groundDistance);
    //}
}
