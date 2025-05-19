using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : MonoBehaviour
{
    private EnemyBehaviour _parent;

    private void Start()
    {
        _parent = GetComponentInParent<EnemyBehaviour>();
    }

    public void Attack()
    {
        _parent.Attack();
    }
}
