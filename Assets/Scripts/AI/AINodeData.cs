using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINodeData : MonoBehaviour
{
    private void Awake()
    {
        Transform[] nodes = GetComponentsInChildren<Transform>();

        GameManager.Instance.AINodes = nodes;
    }
}
