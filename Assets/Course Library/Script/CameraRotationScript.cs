using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationScript : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float sensivity=100;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput=Input.GetAxis("Horizontal");               
        transform.Rotate(0,horizontalInput*Time.deltaTime*sensivity,0);
        
    }
}
