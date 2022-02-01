using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    
    [SerializeField]
    private TextMeshProUGUI life;

    [SerializeField]
    private GameObject gameOverPopup;
    
    [SerializeField]
    private Button gameOverPopupRestartButton;
    
    private int currentLifeCount;
    private bool gameOver;

    public bool GameOver
    {
        get { return gameOver; }
    }
    

    private void Start()
    {
        if (gameOverPopupRestartButton != null)
        {
            gameOverPopupRestartButton.onClick.AddListener(RestartGameOnClick);
        }
        
        SetLifeCount();
    }

    private void RestartGameOnClick()
    {
        SceneManager.LoadScene(0);
        gameOverPopup.SetActive(false);
        gameOver = false;
        SetLifeCount();
        SetZeroScore();
    }

    public void AddScore()
    {
        if (gameOver)
        {
            return;
        }
        
        var currentScore = int.Parse(score.text) + 1;
        score.text = currentScore.ToString();
    }
    
    public void DecreaseLife()
    {
        if (gameOver)
        {
            return;
        }
        
        currentLifeCount = int.Parse(life.text) - 1;
        life.text = currentLifeCount.ToString();
        
        if (currentLifeCount == 0)
        {
            gameOverPopup.SetActive(true);
            gameOver = true;
        }
    }

    private void SetZeroScore()
    {
        var value = 0;
        score.text = value.ToString();
    }
    
    private void SetLifeCount()
    {
        var gameConfig = GetComponent<GameConfig>();
        currentLifeCount = gameConfig.LifeCount;

        life.text = currentLifeCount.ToString();
    }
}
