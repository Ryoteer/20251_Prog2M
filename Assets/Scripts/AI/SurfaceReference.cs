using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class SurfaceReference : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.NavMeshSurface = GetComponent<NavMeshSurface>();
    }
}
