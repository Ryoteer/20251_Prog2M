using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : EntityBehaviour
{
    public override void TakeDamage(int dmg)
    {
        _actualHP -= dmg;

        if (_actualHP <= 0)
        {
            Debug.Log($"Pasmó {name}.");

            _isAlive = false;
        }
        else
        {
            Debug.Log($"{name} recibió {dmg} puntos de daño. ({_actualHP}/{_maxHP}.)");
        }
    }
}
