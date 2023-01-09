using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get 
        {
            if(instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private PlayerMovement _player = null;
    public PlayerMovement Player
    {
        get 
        {
            if(_player == null)
                _player = FindObjectOfType<PlayerMovement>();
            return _player;
        }
    }

    private void Awake()
    {
        if(instance != null) 
        {
            Debug.LogError($"Multiple GameManager Instance is running!");
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
