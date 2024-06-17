using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject pauseMenu;
    public bool isPlaying = true;
    public void OnHomeButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
        isPlaying = true;
    }

    public void OnSettingButtonClicked()
    {
        optionMenu.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        AudioManager.Instance.bgmSource.Stop();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("1-1");
        isPlaying = true;

    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.bgmSource.UnPause();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPlaying = true;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlaying)
            {
                isPlaying = false;
                AudioManager.Instance.bgmSource.Pause();
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (!isPlaying)
            {
                if (optionMenu.activeInHierarchy)
                {
                    optionMenu.SetActive(false);
                }
                else if (!optionMenu.activeInHierarchy)
                {
                    isPlaying = true;
                    AudioManager.Instance.bgmSource.UnPause();
                    pauseMenu.SetActive(false);
                    optionMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
                
            }
        }
        if (isPlaying)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!isPlaying)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
