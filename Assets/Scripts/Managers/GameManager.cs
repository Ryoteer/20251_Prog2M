using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private NavMeshSurface _navMeshSurface;
    public NavMeshSurface NavMeshSurface
    {
        get { return _navMeshSurface; }
        set { _navMeshSurface = value; }
    }

    private PlayerBehaviour _player;
    public PlayerBehaviour Player 
    { 
        get { return _player; }
        set { _player = value; }
    }

    private Transform[] _aiNodes;
    public Transform[] AINodes 
    { 
        get { return _aiNodes; }
        set { _aiNodes = value; }
    }

    private Vector3 _actualCheckpointPosition;
    public Vector3 ActualCheckpointPosition 
    {
        get { return _actualCheckpointPosition; }
        set { _actualCheckpointPosition = value; }
    }
}
