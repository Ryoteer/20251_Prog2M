using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
