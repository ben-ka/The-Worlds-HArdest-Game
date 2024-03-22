using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class squareMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Rigidbody2D myRB;
    private Vector2 stop = new Vector2(0, 0);

    private float horizontal;
    private float vertical;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        velocity = 5f;
        myRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        if(isAlive)
        {
            horizontal = 0;
            vertical = 0;

            if(UnityEngine.Input.GetKey(KeyCode.UpArrow) == true || UnityEngine.Input.GetKey(KeyCode.W) == true){vertical += velocity;}

            if(UnityEngine.Input.GetKey(KeyCode.DownArrow) == true || UnityEngine.Input.GetKey(KeyCode.S) == true){vertical -= velocity;}

            if(UnityEngine.Input.GetKey(KeyCode.RightArrow) == true || UnityEngine.Input.GetKey(KeyCode.D) == true){horizontal += velocity;}
            if(UnityEngine.Input.GetKey(KeyCode.LeftArrow) == true || UnityEngine.Input.GetKey(KeyCode.A) == true){horizontal -= velocity;}
            
            Vector2 vector = new Vector2(horizontal, vertical);
            myRB.velocity = vector;
        }
        else{
            myRB.velocity = stop;
        }
        
        
        
    }

    
  
     private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            isAlive = false;
            
        }    
    }

    
    
}
