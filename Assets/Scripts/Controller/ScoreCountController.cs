using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCountController : MonoBehaviour
{
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private SaveScoreController saveScoreController;
    [SerializeField]
    private TextMeshProUGUI scoreUI;
    [SerializeField]
    private TextMeshProUGUI bestScoreUI;
    
    private UnityEvent difficultyDelayEvent = new UnityEvent();
    private UnityEvent difficultyFruitPackEvent = new UnityEvent();
    
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
    
    public TextMeshProUGUI ScoreUI
    {
        get { return scoreUI; }
    }
    
    private int cuttedFruitCount;
    private int cuttedFruitForPacksCount;

    private void Start()
    {
        saveScoreController.LoadBestScore(bestScoreUI);
        lifeCountController.GameOverEvent.AddListener(SaveBestScore);
    }

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
        
        if (bestScoreUI.text == scoreUI.text)
        {
            bestScoreUI.text = currentScore.ToString();
        }
        
        scoreUI.text = currentScore.ToString();
    }
    
    public void ResetScore()
    {
        var currentScore = 0;
        scoreUI.text = currentScore.ToString();
    }

    private void SaveBestScore() => saveScoreController.SaveBestScore(scoreUI);
}
