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
            Debug.Log($"Pasm� {name}.");

            _isAlive = false;
        }
        else
        {
            Debug.Log($"{name} recibi� {dmg} puntos de da�o. ({_actualHP}/{_maxHP}.)");
        }
    }
}
