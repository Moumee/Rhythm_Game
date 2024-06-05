using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject pauseMenu;
    bool isPlaying = true;
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }

    public void OnSettingButtonClicked()
    {
        optionMenu.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlaying)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isPlaying = false;
            }
            else if (!isPlaying)
            {
                if (optionMenu.activeInHierarchy)
                {
                    optionMenu.SetActive(false);
                }
                else if (!optionMenu.activeInHierarchy)
                {
                    pauseMenu.SetActive(false);
                    optionMenu.SetActive(false);
                    Time.timeScale = 1f;
                    isPlaying = true;
                }
                
            }
            
            
        }

    }
}
