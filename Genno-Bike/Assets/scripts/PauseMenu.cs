using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionScreen, pauseScreen;

    public string mainMenuScene;

//proprierty, car j'en ai besoin pour la coroutine,si le jeu est sur pause on ne veut pas que
//les questions continuent d'etre posee.
    public bool IsPaused{get;set;}

//pour verifier si on peut faire pause au jeu, on ne veut pas mettre le jeu en pause si un question est en train d'etre posee.
//le joueur pourrais chercher la reponse sur internet, on veut tester ses connaissances generales.
    public bool CanBePaused{get; set;}

    // Start is called before the first frame update
    void Start()
    {
        //quand le jeu commence, on peut le mettre sur pause.ss
        CanBePaused=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && CanBePaused)
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if(!IsPaused)
        {
            pauseScreen.SetActive(true);
            IsPaused=true;
            Time.timeScale=0;
        } else
        {
            optionScreen.SetActive(false);
            pauseScreen.SetActive(false);
            IsPaused=false;
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
        Time.timeScale=1;
        SceneManager.LoadScene(mainMenuScene);
    }
}
