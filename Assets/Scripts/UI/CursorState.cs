using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Locked;
    [SerializeField] private bool _isVisible = false;

    private void Start()
    {
        Cursor.lockState = _lockMode;
        Cursor.visible = _isVisible;
    }
}
