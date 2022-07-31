using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProp : MonoBehaviour
{
    public float speed=8;
    float explosionForce=50;
    float explosionRadius=3;
    float angleChangingSpeed=12;
    float rocketMaxAltitude=4;
    Rigidbody rocketRb;
    Vector3 explosionPosition;        
    EnemyControlScript enemyControlScript;
    ParticleSystem explosionParticle;

    private void Start() {          //Instantiates
        rocketRb=GetComponent<Rigidbody>();             
        rocketRb.centerOfMass=new Vector3(0,1,1f);  
        enemyControlScript=GameObject.FindWithTag("Enemy").GetComponent<EnemyControlScript>();
        explosionParticle=GetComponentInChildren<ParticleSystem>();

        rocketRb.AddForce(transform.up*speed,ForceMode.Impulse); //rockets first move to UP direction
        
        
    }
        private void OnTriggerEnter(Collider other) {  //when hit the enemy or ground it's explode
        explosionPosition=transform.position; 
        if(other!=null){
            explosionParticle.Play();
        }               
        if(transform.position.y<0.013f){
            Collider[] objectColliders=Physics.OverlapSphere(explosionPosition,explosionRadius); //get all the colliders in the explosion radius
            
            foreach (Collider Cols in objectColliders)
            {   
                Rigidbody rb=Cols.GetComponent<Rigidbody>();  
                if(rb!=null){
                    rb.AddExplosionForce(explosionForce,explosionPosition,explosionRadius,0,ForceMode.Impulse);
                    
                }
            }
                transform.DetachChildren();
                Destroy(gameObject);
                
                  
            
                     
            
        }
}
    private void FixedUpdate() { //controls the rocket in fixed microseconds
             RocketControl();
                
        }
    
 
    void RocketControl(){                      
        if(transform.position.y>rocketMaxAltitude){   
                Vector3 direction = enemyControlScript.enemyPosition - rocketRb.position;
                direction.Normalize ();
                float rotateAmount = Vector3.Cross (direction, transform.up).z;                     
                rocketRb.angularVelocity = new Vector3(0,0,-angleChangingSpeed * rotateAmount);                    
                rocketRb.velocity = transform.up * speed;                                                     
            
        }
        
        
    }
}
