using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{
    public bool isGameOver=false;
    public GameObject player;
    public float wave=0;    
    float timeLeft=60;

    CharacterControlScript playerScript;
    SpawnManagerScript spawnManagerScript;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI rocketText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button mainMenuButton;
    

    
    void Start()
    {
        Application.targetFrameRate = 60;
        
        playerScript=player.GetComponent<CharacterControlScript>();
        spawnManagerScript=GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerScript>();
        
        
    }

    
    void Update()
    {
        textController();
        if(player==null || timeLeft<=0)
        {
            isGameOver=true;
            
        }
        if(isGameOver)
        {
            spawnManagerScript.enabled=false;
            gameOverText.gameObject.SetActive(true);            
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            
        }
        
        
    }

    void textController(){
        waveText.text="Wave: "+ wave;
        rocketText.text="Rockets: "+ playerScript.rocketsRemain;
        energyText.text="Energy: "+ playerScript.energyJumpRemain;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void mainMenu(){
        SceneManager.LoadScene("MainMenuSc");
    }
}
