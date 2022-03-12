using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;
    private bool started = false; 

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;

        }
    } 
    void Start()
    {
        gameOver = false;
    } 
    void Update()
    {
        TabToStart();
    } 
    private void FixedUpdate()
    {
        if (started)
        {
            StartGame(); 
        }
    }

    public void StartGame()
    {
        if (started)
        {
            UiManager.instance.GameStart();
            ScoreManager.instance.startScore();
        }
       
    }
    public void GameOver()
    {
        UiManager.instance.GameOver();
        ScoreManager.instance.stopScore();
        gameOver = true;

    }
    void TabToStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            started = true;
        }
    }
}
