using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    private Collider _collider;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            _particleSystem.Play();

            GameManager.Instance.ActualCheckpointPosition = transform.position;

            _collider.enabled = false;
        }
    }
}
