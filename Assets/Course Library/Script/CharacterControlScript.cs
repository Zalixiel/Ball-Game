using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum powerUpStates  {
    None,
    Speed,
    Bullet,
    HomingMissile,
    energyJump    
};



public class CharacterControlScript : MonoBehaviour
{
    float speed=100;
    float verticalInput;
    float powerBoostMultipler=2;
    public float rocketsRemain=0;
    public float energyJumpRemain=0;

    private Vector3 bulletSpawnOffset=new Vector3(0,0.5f,0);
    powerUpStates powerUpState = powerUpStates.None;

    public GameObject bullet;
    public GameObject rocket;   

    Rigidbody playerRb;
    Animator RingAnimator;
    GameObject FocalPoint;
    GameObject PowerBoostHolder;
    GameObject PowerBoostRing;


        
    
    

    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        FocalPoint=GameObject.Find("FocalPoint");
        PowerBoostHolder=GameObject.Find("AnimHolder"); 
        PowerBoostRing=GameObject.Find("SelectionRing_02");
        RingAnimator = PowerBoostRing.GetComponent<Animator>();
        
    }
 
    // Update is called once per frame
    void Update()
    {  
        
         if(transform.position.y<-3){
            Destroy(gameObject);
        }
        PowerBoostHolder.transform.position=transform.position;
        PowerBoostHolder.transform.eulerAngles=FocalPoint.transform.eulerAngles + new Vector3(0,90,0);

        if (powerUpState==powerUpStates.HomingMissile && Input.GetKeyDown(KeyCode.Space)&& rocketsRemain>0 ){ // Not a bug it's a secret feature :)        
            Instantiate(rocket,transform.position + new Vector3(0,1,0),Quaternion.identity);
            powerUpState=powerUpStates.None;
            rocketsRemain--;
                        
        }else if(powerUpState==powerUpStates.energyJump&&Input.GetKeyDown(KeyCode.Space)&&energyJumpRemain>0){
            powerUpState=powerUpStates.None;
            energyJumpRemain--;
            energyJump();
            playerRb.AddForce(FocalPoint.transform.up*speed,ForceMode.Impulse); 
        }
        
            
        
    }
    private void FixedUpdate() {
            verticalInput=Input.GetAxis("Vertical");        
            playerRb.AddForce(FocalPoint.transform.forward*verticalInput*speed,ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PowerBoost")){
            powerUpState=powerUpStates.Speed;            
            powerUpControl();    
            Destroy(other.gameObject);          

            }else if(other.gameObject.CompareTag("Bullet")){              
                powerUpState=powerUpStates.Bullet;
                powerUpControl();
                Destroy(other.gameObject);
                
              }else if (other.gameObject.CompareTag("Missile")){
                powerUpState=powerUpStates.HomingMissile;
                powerUpControl();
                Destroy(other.gameObject);

                }else if (other.gameObject.CompareTag("EnergyJump")){
                powerUpState=powerUpStates.energyJump;
                powerUpControl();
                Destroy(other.gameObject);
                }
    }
    void powerUpControl(){

        switch (powerUpState)
        {
            case powerUpStates.Speed:
                RingAnimator.SetBool("isSpeedUp",true);
                if(speed<400){
                    speed*=powerBoostMultipler;
                }                       
                StartCoroutine(powerUpCountdownRoutine());
                break;
            case powerUpStates.Bullet:
                RingAnimator.SetBool("isBulletFire",true);                
                    StartCoroutine(powerUpCountdownRoutine());
                    InvokeRepeating("bulletFire",0.3f,0.3f);                                              
                break;    
            case powerUpStates.HomingMissile:
                rocketsRemain++;
                Debug.Log($"{rocketsRemain} rockets remain");
                break;
            case powerUpStates.energyJump:
                energyJumpRemain++;
                Debug.Log($"{energyJumpRemain} energy jump remain");
                break;
                    
            default:
            Debug.Log("No PowerUp"); 
                break;
        }
    }

    IEnumerator powerUpCountdownRoutine(){
        yield return new WaitForSeconds(5); 
        if(speed>100){
        speed=speed/powerBoostMultipler;
        }              
                
        powerUpState=powerUpStates.None;
        RingAnimator.SetBool("isSpeedUp",false);
        RingAnimator.SetBool("isBulletFire",false); 
        Debug.Log("Power Up ended");
    }
 
    void bulletFire(){     // calling from powerUpControl() > Bullet enum Control
        if (powerUpState==powerUpStates.Bullet)
        {
            Instantiate(bullet,transform.position+bulletSpawnOffset,Quaternion.identity); 
        }       
                
    }
    void energyJump(){
        float radius=10;
        float explosionForce=100;               
        Collider [] enemys= Physics.OverlapSphere(transform.position,radius);
        foreach (Collider enemy in enemys)
        {
            if(enemy.gameObject.CompareTag("Enemy")){
                Rigidbody enemyRb=enemy.GetComponent<Rigidbody>();
                if(enemyRb!=null){
                    
                    enemyRb.AddExplosionForce(explosionForce,transform.position,radius,0,ForceMode.Impulse);
                }
                }
                
            }
        }
    }




