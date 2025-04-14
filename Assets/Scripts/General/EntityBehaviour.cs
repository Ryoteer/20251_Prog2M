using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] protected int _maxHP = 100;

    protected bool _isAlive = true;
    protected int _actualHP;

    public virtual void Awake()
    {
        _actualHP = _maxHP;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (!_isAlive) return;
    }

    public virtual void FixedUpdate()
    {
        if (!_isAlive) return;
    }

    public virtual void LateUpdate()
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
