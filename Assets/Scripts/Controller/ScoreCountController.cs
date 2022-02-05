using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCountController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private TextMeshProUGUI scoreUI;

    private UnityEvent difficultyDelayEvent;
    private UnityEvent difficultyFruitPackEvent;
    
    public UnityEvent DifficultyDelayEvent
    {
        get { return difficultyDelayEvent; }
        set { difficultyDelayEvent = value; }
    }
    
    public UnityEvent DifficultyFruitPackEvent
    {
        get { return difficultyFruitPackEvent; }
        set { difficultyFruitPackEvent = value; }
    }
    
    private int cuttedFruitCount;
    private int cuttedFruitForPacksCount;
    
    public void AddScore()
    {
        cuttedFruitCount++;
        cuttedFruitForPacksCount++;
        
        if (cuttedFruitCount == gameConfigController.GameConfig.CuttedFruitsForDecreaseFruitDelay)
        {
            difficultyDelayEvent.Invoke();
            cuttedFruitCount = 0;
        }
        
        if (cuttedFruitForPacksCount == gameConfigController.GameConfig.CuttedFruitsForEncreaseFruitInPack)
        {
            difficultyFruitPackEvent.Invoke();
            cuttedFruitForPacksCount = 0;
        }
        
        var currentScore = int.Parse(scoreUI.text) + 1;
        scoreUI.text = currentScore.ToString();
    }
    
    public void ResetScore()
    {
        var currentScore = 0;
        scoreUI.text = currentScore.ToString();
    }
}
