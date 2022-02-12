using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCountController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI scoreTextPrefab;
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
    [SerializeField]
    private Animation scoreAnimation;
    [SerializeField]
    private Animation bestScoreAnimation;
    
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

    public void AddScore(int score, Vector3 position)
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
        
        var currentScore = int.Parse(scoreUI.text) + score;
        
        if (bestScoreUI.text == scoreUI.text)
        {
            bestScoreAnimation.Play();
            bestScoreUI.text = currentScore.ToString();
        }

        scoreAnimation.Play();
        scoreUI.text = currentScore.ToString();
        
        var rot = Quaternion.Euler(0, 0, Random.Range(gameConfigController.GameConfig.ScoreTextRotationMin, gameConfigController.GameConfig.ScoreTextRotationMax));
        var spawnedScoreText = Instantiate(scoreTextPrefab, position, rot, canvas.transform);
        spawnedScoreText.text = $"+{score}";
    }
    
    public void ResetScore()
    {
        var currentScore = 0;
        scoreUI.text = currentScore.ToString();
    }

    private void SaveBestScore()
    {
        var bestScore = int.Parse(bestScoreUI.text);
        var currentScore = int.Parse(scoreUI.text);
        
        if (bestScore <= currentScore)
        {
            saveScoreController.SaveBestScore(scoreUI);
        }
    }
}
