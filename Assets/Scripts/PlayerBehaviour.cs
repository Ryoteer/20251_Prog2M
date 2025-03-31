using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Physics")]
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _moveSpeed = 3.5f;

    private Vector3 _dir = Vector3.zero;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(_jumpKey))
        {
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
}
