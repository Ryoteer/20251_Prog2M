using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] protected int _maxHP = 100;

    protected bool _isAlive = true;
    protected int _actualHP;

    protected virtual void Awake()
    {
        _actualHP = _maxHP;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (!_isAlive) return;
    }

    protected virtual void FixedUpdate()
    {
        if (!_isAlive) return;
    }

    protected virtual void LateUpdate()
    {
        if (!_isAlive) return;
    }

    public virtual void TakeDamage(int dmg)
    {
        _actualHP -= dmg;

        if(_actualHP <= 0)
        {
            _isAlive = false;
        }
    }
}
