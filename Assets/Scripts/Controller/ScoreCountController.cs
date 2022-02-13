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
    
    private UnityEvent _difficultyDelayEvent = new UnityEvent();
    private UnityEvent _difficultyFruitPackEvent = new UnityEvent();
    
    public UnityEvent DifficultyDelayEvent
    {
        get { return _difficultyDelayEvent; }
        set { _difficultyDelayEvent = value; }
    }
    
    public UnityEvent DifficultyFruitPackEvent
    {
        get { return _difficultyFruitPackEvent; }
        set { _difficultyFruitPackEvent = value; }
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
            _difficultyDelayEvent.Invoke();
            cuttedFruitCount = 0;
        }
        
        if (cuttedFruitForPacksCount == gameConfigController.GameConfig.CuttedFruitsForEncreaseFruitInPack)
        {
            _difficultyFruitPackEvent.Invoke();
            cuttedFruitForPacksCount = 0;
        }
        
        var currentScore = int.Parse(scoreUI.text) + score;
        var bestScore = int.Parse(bestScoreUI.text);
        
        if (currentScore >= bestScore)
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
