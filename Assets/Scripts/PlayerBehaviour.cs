using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private string _airBoolName = "isOnAir";
    [SerializeField] private string _jumpTriggerName = "onJump";
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";

    [Header("Inputs")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;

    private bool _isOnAir = false;

    private Vector3 _dir = Vector3.zero, _velocity;

    private Animator _animator;
    private Rigidbody _rb;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(_xAxisName, _dir.x);
        _dir.z = Input.GetAxis("Vertical");
        _animator.SetFloat(_zAxisName, _dir.z);

        if (Input.GetKeyDown(_jumpKey) && !_isOnAir)
        {
            _animator.SetTrigger(_jumpTriggerName);
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if(_dir.sqrMagnitude != 0.0f)
        {
            Movement(_dir);
        }
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir.normalized * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.layer}");

        if (collision.gameObject.layer == 30)
        {
            _isOnAir = false;
            _animator.SetBool(_airBoolName, _isOnAir);
        }

        if(collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            _velocity = _rb.velocity;
            _rb.velocity = rb.velocity;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 30)
        {
            _isOnAir = true;
            _animator.SetBool(_airBoolName, _isOnAir);
        }

        if (collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            _rb.velocity = _velocity;
        }
    }
}
