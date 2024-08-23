using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageManager : MonoBehaviour
{
    public Transform[] cabbagePoints;
    public Cabbage currentCabbage;
    public Cabbage[] cabbages;
    public CabbageFist fist;
    public int keyPressCount = 0;
    private Vector3 randomPos;
    private Vector3 startPos = Vector3.zero;    
    private float shakeDuration = 0.12f;
    private float shakeDistance = 0.2f;
    private float delayBetweenShakes = 0.02f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var cabbage in cabbages)
        {
            if (cabbage.transform.position.x < 6.5 && cabbage.transform.position.x >= 0)
            {
                currentCabbage = cabbage;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnNoteHit();
        }
    }

    public void OnNoteHit()
    {
        StartCoroutine(ShakeCoroutine());
        currentCabbage.OnNoteHit();
        fist.OnNoteHit();
        keyPressCount++;
        if (keyPressCount == 3)
        {
            MoveAllCabbages();
            keyPressCount = 0;
        }
    }

    public void OnNoteMiss()
    {
        fist.OnNoteMiss();
        keyPressCount++;
        if (keyPressCount == 3)
        {
            MoveAllCabbages();
            keyPressCount = 0;
        }
    }

    public void MoveAllCabbages()
    {
        foreach (var cabbage in cabbages)
        {
            cabbage.MoveNext();
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        float timer = 0f;
        while (timer < shakeDuration)
        {
            randomPos = startPos + (Random.insideUnitSphere * shakeDistance);
            currentCabbage.transform.position = randomPos;
            yield return new WaitForSeconds(delayBetweenShakes);
            timer += delayBetweenShakes;
        }

        currentCabbage.transform.position = startPos;
    }
}
