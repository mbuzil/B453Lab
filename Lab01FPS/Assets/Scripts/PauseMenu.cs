using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public bool isPaused;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI.activeSelf)
                ResumeGame();
            else;
                PauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void PauseGame()
    {
        pauseUI.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }
}
