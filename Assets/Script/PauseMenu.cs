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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.Instance.bgmSource.Stop();
        AudioManager.Instance.stageSource.Stop();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("1-1");
        isPlaying = true;

    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.bgmSource.UnPause();
        AudioManager.Instance.stageSource.UnPause();
        pauseMenu.SetActive(false);
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
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isPlaying = false;
                AudioManager.Instance.bgmSource.Pause();
                AudioManager.Instance.stageSource.Pause();
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (!isPlaying)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (optionMenu.activeInHierarchy)
                {
                    optionMenu.SetActive(false);
                }
                else if (!optionMenu.activeInHierarchy)
                {
                    isPlaying = true;
                    AudioManager.Instance.bgmSource.UnPause();
                    AudioManager.Instance.stageSource.UnPause();
                    pauseMenu.SetActive(false);
                    optionMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
                
            }
        }
        

    }
}
