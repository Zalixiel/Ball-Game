using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    private float fireForce = 10;
    private Rigidbody bulletRb;
    private GameObject focalPoint;
    private float gameAreaBoundsForBullet=25;
    ParticleSystem bulletExplosion;
   

    private void Start() {
        bulletRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        bulletExplosion = GetComponentInChildren<ParticleSystem>();
    }
    private void FixedUpdate() {        
        bulletRb.AddForce(focalPoint.transform.forward*fireForce,ForceMode.Impulse);        
        areaboundDestroy();

        
        
        
    }
   
        
    
    void areaboundDestroy() {
        if(transform.position.x>gameAreaBoundsForBullet || transform.position.x<-gameAreaBoundsForBullet || transform.position.z>gameAreaBoundsForBullet || transform.position.z<-gameAreaBoundsForBullet) {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Enemy") {   
            transform.DetachChildren();         
            bulletExplosion.Play();
            Destroy(gameObject);
        }
    }
    
}
