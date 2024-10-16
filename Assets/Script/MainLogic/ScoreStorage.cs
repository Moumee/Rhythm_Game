using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreStorage : MonoBehaviour
{
    public int FinalScore = 0;

    public bool[] isSuccess = new bool[]{false, false, false, false, false};

    private static ScoreStorage _Instance;
    public static ScoreStorage Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = FindObjectOfType<ScoreStorage>();
                if (_Instance == null)
                {
                    GameObject scoreStorageObject = new GameObject(nameof(ScoreStorage));
                    _Instance = scoreStorageObject.AddComponent<ScoreStorage>();
                }
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
