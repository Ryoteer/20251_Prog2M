using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePlatformBehaviour : MonoBehaviour
{
    [Header("Times")]
    [SerializeField] private float _fadeTime = 3.0f;
    [SerializeField] private float _interval = 5.0f;
    [SerializeField] private float _respawnTime = 5.0f;

    private bool _isActive = false;

    private Collider _collider;
    private Material _material;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _material = GetComponentInChildren<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerBehaviour>() && !_isActive)
        {
            StartCoroutine(FadeCycle());
        }
    }

    private IEnumerator FadeCycle()
    {
        _isActive = true;

        float t = 0.0f;

        while(t < 1.0f)
        {
            t += Time.deltaTime / _fadeTime;

            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, Mathf.Lerp(1.0f, 0.0f, t));

            yield return null;
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 0.0f);
        _collider.enabled = false;

        yield return new WaitForSeconds(_interval);

        t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / _respawnTime;

            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, Mathf.Lerp(0.0f, 1.0f, t));

            yield return null;
        }

        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1.0f);
        _collider.enabled = true;

        _isActive = false;
    }
}
