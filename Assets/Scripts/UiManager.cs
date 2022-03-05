using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject stairRunPanel;
    public GameObject gameOverPanel;
    public GameObject tabText;
    public Text score;
    public Text highScore1;
    public Text highScore2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameStart()
    {
        tabText.SetActive(false);
        stairRunPanel.GetComponent<Animation>().Play("panelUp");
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
