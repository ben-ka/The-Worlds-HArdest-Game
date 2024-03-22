using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float startPosX;
    [SerializeField] private float startPosY;
    
    public float endPosX;
    public float endPosY;

    private bool toEnd;

    public float velocity;

    private Rigidbody2D myRB;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        toEnd = true;
        transform.position = new Vector2(startPosX, startPosY);
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(toEnd)
        {
            myRB.velocity = new Vector2((endPosX - startPosX) / velocity, (endPosY - startPosY) / velocity);
            
            if(Math.Abs(endPosX - transform.position.x) < 0.01 && Math.Abs(endPosY - transform.position.y) < 0.01)
            {
            toEnd = false;
            }
        }
        

        if(!toEnd)
        {
            myRB.velocity = new Vector2((startPosX - endPosX) / velocity, (startPosY - endPosY) / velocity);

            if(Math.Abs(startPosX - transform.position.x) < 0.01 && Math.Abs(startPosY - transform.position.y) < 0.01)
            {
                toEnd = true;
            }
        }
        

    }
}
