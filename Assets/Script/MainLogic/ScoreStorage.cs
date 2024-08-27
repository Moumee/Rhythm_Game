using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStorage : MonoBehaviour
{
    public int FinalScore = 0;

    private static ScoreStorage _Instance;
    public static ScoreStorage Instance
    {
        get
        {
            if (!_Instance)
            {
                var prefab = Resources.Load<GameObject>("ScoreStoragePrefab");
                var inScene = Instantiate(prefab);
                _Instance = inScene.GetComponentInChildren<ScoreStorage>();
                if (!_Instance) _Instance = inScene.AddComponent<ScoreStorage>();
                DontDestroyOnLoad(_Instance.transform.root.gameObject);
            }
            return _Instance;
        }
    }

}
