using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;
    

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype 4");
    }
    public void Options()
    {   Debug.Log("Options");
        //SceneManager.LoadScene("Options");
    }
    public void Quit()
    {
        Application.Quit();
    }

    
}
