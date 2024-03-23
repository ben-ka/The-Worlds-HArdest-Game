using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    [SerializeField] private int score;
    private int numCoins;

    private bool canPass;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        numCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        if(numCoins == 0){
            canPass = true;
        }
        else{
            canPass = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score++;
        if(score == numCoins)
        {
            canPass = true;
        }
    }
    public void ResetScore()
    {
        score = 0;
        canPass = false;
    }

    public bool CanPassLevel()
    {
        return canPass;
    }
}
