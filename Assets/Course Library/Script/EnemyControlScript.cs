using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour
{
    GameObject player;
    Rigidbody enemyRb;
    public int speed=4;
    Vector3 toPlayer;
    public Vector3 enemyPosition;
    
    void Start()
    {
        enemyRb=GetComponent<Rigidbody>();
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if(transform.position.y<-3){
            Destroy(gameObject);
        }
        enemyPosition=transform.position;
        
    }
    void FixedUpdate() {
        if(player!=null){
            toPlayer=player.transform.position-transform.position;
             enemyRb.AddForce(toPlayer*speed,ForceMode.Force);
}
        }
        
    
    
}
