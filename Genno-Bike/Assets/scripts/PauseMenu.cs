using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject optionScreen, pauseScreen;

    public string mainMenuScene;

    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if(!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused=true;
            Time.timeScale=0;
        } else
        {
            optionScreen.SetActive(false);
            pauseScreen.SetActive(false);
            isPaused=false;
            Time.timeScale=1;
        }
    }

    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }

     public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }

     public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale=1;
    }
}
