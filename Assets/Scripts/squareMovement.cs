using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class squareMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Rigidbody2D myRB;
    private Vector2 stop = new Vector2(0, 0);

    private float horizontal;
    private float vertical;
    private bool isAlive;

    private LogicManager logicManager;

    private float startPosX;
    private float startPosY;

    private int indexCoin;

    private GameObject[] coins;


    private Vector2[] coinsLocations;
    public GameObject coin;
    

    public AudioClip coinSound;

    public AudioClip failedSound;

    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;



        isAlive = true;
        velocity = 5f;
        myRB = GetComponent<Rigidbody2D>();


        GameObject logicManagerObject = GameObject.Find("Logic Manager");

        if (logicManagerObject != null)
        {
           
            logicManager = logicManagerObject.GetComponent<LogicManager>();
        }
        else
        {
            UnityEngine.Debug.LogWarning("Logic Manager not found in the scene. Make sure it exists and is named correctly.");
        }
        
        coinsLocations = new Vector2[GameObject.FindGameObjectsWithTag("Coin").Length];
        for(int i = 0; i < coinsLocations.Length; i++) {
            coinsLocations[i] = new Vector2(-999f, -999f);   
        }
        indexCoin = 0;


        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(failedSound);
            isAlive = false;
            logicManager.ResetScore();
            ReturnToOriginalLocation();
            
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            
            audioSource.PlayOneShot(coinSound);
            coinsLocations[indexCoin] = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            
            Destroy(other.gameObject);
            logicManager.AddScore();
            indexCoin++;
        }       

    }

    private void ReturnToOriginalLocation()
    {
        transform.position = new Vector2(startPosX, startPosY);
        isAlive = true;
        ReturnAllCoins();
    }

    private void ReturnAllCoins()
    {
        int i = 0; 
        
        while (i < coinsLocations.Length && coinsLocations[i] != new Vector2(-999f, -999f))
        {
            
            Vector2 position = coinsLocations[i];

            Instantiate(coin, position, Quaternion.identity); 
            i++;
        }
        GetCoinsReadyForNextTry();

    }
    private void GetCoinsReadyForNextTry(){
        indexCoin = 0;
        for(int j = 0; j < coinsLocations.Length; j++) {
            coinsLocations[j] = new Vector2(-999f, -999f);   
        }

    }



    
    
}
