using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : EntityBehaviour
{
    [Header("AI")]
    [SerializeField] private float _atkDistance = 2.0f;
    [SerializeField] private float _changeNodeDistance = 0.75f;
    [SerializeField] private float _chaseDistance = 5.0f;

    [Header("Animation")]
    [SerializeField] private string _atkTriggerName = "onAttack";
    [SerializeField] private string _deathTriggerName = "onDeath";
    [SerializeField] private string _moveFloatName = "speed";

    [Header("Combat")]
    [SerializeField] private int _dmg = 10;

    [Header("Physics")]
    [SerializeField] private float _atkRayDistance = 2.0f;
    [SerializeField] private LayerMask _atkMask;
    [SerializeField] private Transform _rayOrigin;

    private float _distanceToPlayer = 0.0f, _distanceToNode = 0.0f;

    private Animator _animator;
    private NavMeshAgent _agent;
    private PlayerBehaviour _player;
    private Transform _actualNode;
    private Transform[] _aiNodes;

    private Ray _atkRay;
    private RaycastHit _atkHit;

    protected override void Start()
    {
        base.Start();

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _player = GameManager.Instance.Player;
        _aiNodes = GameManager.Instance.AINodes;

        _actualNode = GetNewNode();

        _agent.SetDestination(_actualNode.position);
    }

    protected override void Update()
    {
        base.Update();

        _distanceToPlayer = Vector3.SqrMagnitude(transform.position - _player.transform.position);

        _animator.SetFloat(_moveFloatName, _agent.velocity.magnitude);

        if(_distanceToPlayer <= _chaseDistance * _chaseDistance)
        {
            if(_distanceToPlayer <= _atkDistance * _atkDistance)
            {
                if (!_agent.isStopped)
                {
                    _agent.isStopped = true;
                }

                _animator.SetTrigger(_atkTriggerName);
            }
            else
            {
                if (_agent.isStopped)
                {
                    _agent.isStopped = false;
                }

                _agent.SetDestination(_player.transform.position);
            }
        }
        else
        {
            if(_agent.destination != _actualNode.position)
            {
                _agent.SetDestination(_actualNode.position);
            }

            _distanceToNode = Vector3.SqrMagnitude(transform.position - _actualNode.position);

            if (_distanceToNode <= _changeNodeDistance * _changeNodeDistance)
            {
                _actualNode = GetNewNode(_actualNode);

                _agent.SetDestination(_actualNode.position);
            }
        }
    }

    public void Attack()
    {
        _atkRay = new Ray(_rayOrigin.position, transform.forward);

        if(Physics.SphereCast(_atkRay, 0.25f, out _atkHit, _atkRayDistance, _atkMask))
        {
            if(_atkHit.collider.TryGetComponent(out PlayerBehaviour entity))
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

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
    }

    private Transform GetNewNode(Transform prevNode = null)
    {
        if (!prevNode)
        {
            return _aiNodes[Random.Range(0, _aiNodes.Length)];
        }
        else
        {
            Transform newNode;

            do
            {
                newNode = _aiNodes[Random.Range(0, _aiNodes.Length)];
            }
            while (newNode == prevNode);

            return newNode;
        }
    }
}
