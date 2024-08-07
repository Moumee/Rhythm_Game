using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    List<GameObject> notesToCheck;
    BoxCollider2D syncCollider;
    public GameObject missText;
    // Start is called before the first frame update
    void Start()
    {
        syncCollider = GetComponent<BoxCollider2D>();
        notesToCheck = FindObjectOfType<NoteManager>().notesToCheck;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (notesToCheck.Count != 0)
            {
                notesToCheck[0].GetComponent<Animator>().SetTrigger("Miss");
                missText.GetComponent<Animator>().SetTrigger("Miss");
                notesToCheck.RemoveAt(0);
                

            }
        }
    }

    
}
