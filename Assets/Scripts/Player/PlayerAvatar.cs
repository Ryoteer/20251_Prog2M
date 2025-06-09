using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private PlayerBehaviour _parent;

    private void Start()
    {
        _parent = GetComponentInParent<PlayerBehaviour>();
    }

    public void Attack()
    {
        _parent.Attack();
    }

    public void Interact()
    {
        _parent.Interact();
    }

    public void PlayAttackClip()
    {
        _parent.PlayAttackClip();
    }

    public void PlayJumpClip(int index)
    {
        _parent.PlayJumpClip(index);
    }

    public void PlayMovementClip()
    {
        _parent.PlayMovementClip();
    }
}
