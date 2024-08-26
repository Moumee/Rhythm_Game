using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using UnityEngine.Pool;

public class Mold : MonoBehaviour
{
    MoldManager manager;
    private ObjectPool<Mold> _pool;
    public int positionId = 0;
    private int beatJumpCount;
    [SerializeField]
    SpriteRenderer[] fillingRenderers;
    [SerializeField] Transform[] standPoints;

    public bool isLive = false;
    public bool isOnTime = false;

    private float moveDuration = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        manager = FindObjectOfType<MoldManager>();
        isLive = true;
        beatJumpCount = 0;
        standPoints = manager.standPoints;
    }


    public void SetPoint(Transform[] standPoint)
    {
        this.standPoints = standPoint;
        isLive = true;
    }

    public void SetPool(ObjectPool<Mold> pool)
    {
        _pool = pool;
    }

    private void OnDisable()
    {
        foreach (var renderer in fillingRenderers)
        {
            renderer.enabled = false;
        }
    }

    public void MoveMold()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        if (positionId < 2)
        {
            float timer = 0f;
            while (timer <= moveDuration)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(standPoints[positionId].position,
                    standPoints[positionId + 1].position, timer / moveDuration);
                yield return null;
            }
            transform.position = standPoints[positionId + 1].position;
            positionId++;
        }
        else
        {
            yield return new WaitForSeconds(moveDuration);
            positionId = 0;
            manager.activeMolds.Remove(this);
            _pool.Release(this);
        }
    }


}
