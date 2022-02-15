using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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
    
    private int _cuttedFruitCount;
    private int _cuttedFruitForPacksCount;
    private int _scoreSum;
    
    private void Start()
    {
        saveScoreController.LoadBestScore(bestScoreUI);
        lifeCountController.GameOverEvent.AddListener(SaveBestScore);
        _scoreSum = 0;
    }

    private void FixedUpdate()
    {
        var scoreUIValue = int.Parse(scoreUI.text);
        
        if (scoreUIValue < _scoreSum)
        {
            scoreUIValue++;
            scoreAnimation.Play();
            scoreUI.text = scoreUIValue.ToString();
            
            var bestScoreUIValue = int.Parse(bestScoreUI.text);
            
            if (scoreUIValue >= bestScoreUIValue)
            {
                bestScoreAnimation.Play();
                bestScoreUI.text = scoreUIValue.ToString();
            }
        }
    }

    public void AddScore(int score, Vector3 position)
    {
        _cuttedFruitCount++;
        _cuttedFruitForPacksCount++;
        
        if (_cuttedFruitCount == gameConfigController.GameConfig.CuttedFruitsForDecreaseFruitDelay)
        {
            _difficultyDelayEvent.Invoke();
            _cuttedFruitCount = 0;
        }
        
        if (_cuttedFruitForPacksCount == gameConfigController.GameConfig.CuttedFruitsForEncreaseFruitInPack)
        {
            _difficultyFruitPackEvent.Invoke();
            _cuttedFruitForPacksCount = 0;
        }
        
        _scoreSum += score;
        
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
