using UnityEngine;

public class BackgroundScratch : MonoBehaviour
{
    GameObject firstScratch;
    GameObject secondScratch;
    // Start is called before the first frame update
    void Start()
    {
        firstScratch = transform.GetChild(0).gameObject;
        secondScratch = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOneThird()
    {
        firstScratch.SetActive(true);
    }

    public void OnTwoThirds()
    {
        secondScratch.SetActive(true);
    }
}
