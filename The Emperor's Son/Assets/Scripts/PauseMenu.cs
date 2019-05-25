using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool gameIsPaused = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")){
            if(gameIsPaused)
                Resume();
            else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0.0001f;
    }
}
