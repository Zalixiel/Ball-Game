using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    float spawnBound=10;
    public float enemyCount=1;
    
    
    int maxEnemyCount=1; // TODO  every 10 wave increase maxEnemyCount by 1
    
    float powerUpSpawnRate=30;
    

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] boosts;
    GameControlScript gameControlScript;


    
    
    void Start()
    {
        InvokeRepeating("enemyRemain",1f,0.5f);
        InvokeRepeating("randomPowerUpSpawn",20f,powerUpSpawnRate);
        gameControlScript=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();
        
    }

    
    void Update()
    {
        if(enemyCount<maxEnemyCount)
        {   
            float Index=enemySpawnChance();
            float xPos=Random.Range(-spawnBound,spawnBound);
            float zPos=Random.Range(-spawnBound,spawnBound);
            Vector3 spawnPointOffset=new Vector3(xPos,0,zPos);
            Instantiate(enemies[((int)Index)],transform.position+spawnPointOffset,Quaternion.identity);
            enemyCount++;
            randomPowerUpSpawn();
            
            
            if(gameControlScript.wave%10==0)
            {
                maxEnemyCount++;
            }
        }
        


       
    }

    void enemyRemain(){        
        enemyCount=GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount==0){
            gameControlScript.wave++;  
        }
                
    }

   
    void randomPowerUpSpawn(){
        
            float Index=Random.Range(0.0f,boosts.Length);
            float xPos=Random.Range(-spawnBound,spawnBound);
            float zPos=Random.Range(-spawnBound,spawnBound);
            Vector3 spawnPointOffset=new Vector3(xPos,0,zPos);
            Instantiate(boosts[((int)Index)],transform.position+spawnPointOffset,Quaternion.identity);
    }
    
    public int enemySpawnChance(){
        float spawnChance=Random.Range(0.0f,100.0f);
        Debug.Log(spawnChance);
        if(spawnChance<=50) //%50
        {
            return 0;
        }
        else if(spawnChance<=85) //%35
        {
            return 1;
        }
        else if(spawnChance<=95)  //%10
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
