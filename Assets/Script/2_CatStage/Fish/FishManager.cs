using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FishManager : MonoBehaviour
{
    [SerializeField] private Knife knife;
    private bool thirdFishActive = false;
    public List<Fish> fishList = new List<Fish>();
    public Fish currentFish;
    public float vibrateFishOffset = 0.3f;
    private Coroutine vibrateCoroutine;
    public float vibrateDuration = 0.05f;

    

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void MoveAllFish()
    {
        foreach (Fish fish in fishList)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                fish.MoveFish();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var fish in fishList)
        {
            if (fish.positionId == 1)
            {
                currentFish = fish;
            }
        }

    }

    public void VibrateCurrentFish()
    {
        if (vibrateCoroutine != null)
        {
            StopCoroutine(vibrateCoroutine);
        }
        vibrateCoroutine = StartCoroutine(VibrateCoroutine());
    }

    private IEnumerator VibrateCoroutine()
    {
        Vector3 startPos = currentFish.transform.position;
        Vector3 vibratePos = startPos - new Vector3(0, vibrateFishOffset, 0);
        float elapsedTime = 0f;

        while (elapsedTime < vibrateDuration)
        {
            float t = elapsedTime / vibrateDuration;
            float yOffset = Mathf.Sin(t * Mathf.PI * 2) * vibrateFishOffset;
            currentFish.transform.position = startPos + new Vector3(0, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentFish.transform.position = startPos;
    }

    public void OnNoteHit()
    {
        if (currentFish.isMoving)
            return;
        knife.OnKeyPress();
        currentFish.cutObjects[knife.knifeIndex].SetActive(true);
        VibrateCurrentFish();
    }

    public void OnNoteMiss()
    {

        knife.OnKeyPress();
    }
}
