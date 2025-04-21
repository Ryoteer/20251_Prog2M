using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour, IInteraction
{
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponentInParent<Animation>();
    }

    public void OnInteraction()
    {
        if (_animation.isPlaying) return;

        _animation.Play();
    }
}
