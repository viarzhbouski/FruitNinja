using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.UI;

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
    private Text scoreUI;
    [SerializeField]
    private Text bestScoreUI;
    [SerializeField]
    private RectTransform scoreImage;
    
    private UnityEvent _difficultyDelayEvent = new UnityEvent();
    private UnityEvent _difficultyFruitPackEvent = new UnityEvent();
    private int _cuttedFruitCount;
    private int _cuttedFruitForPacksCount;
    private int _scoreSum;
    private bool _bestScoreReached;
    
    private GameConfig GameConfig => gameConfigController.GameConfig;
    
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
    
    public Text ScoreUI
    {
        get { return scoreUI; }
    }
    
    private void Start()
    {
        saveScoreController.LoadBestScore(bestScoreUI);
        lifeCountController.GameOverEvent.AddListener(SaveBestScore);
        _scoreSum = 0;
        _bestScoreReached = false;
    }

    public void AddScore(int score, Vector3 position)
    {
        _cuttedFruitCount++;
        _cuttedFruitForPacksCount++;
        
        if (_cuttedFruitCount == GameConfig.CuttedFruitsForDecreaseFruitDelay)
        {
            _difficultyDelayEvent.Invoke();
            _cuttedFruitCount = 0;
        }
        
        if (_cuttedFruitForPacksCount == GameConfig.CuttedFruitsForEncreaseFruitInPack)
        {
            _difficultyFruitPackEvent.Invoke();
            _cuttedFruitForPacksCount = 0;
        }
        
        _scoreSum += score;
        
        scoreUI.DOText(_scoreSum.ToString(), GameConfig.ScoreCountSpeed, false, ScrambleMode.Numerals);

        if (!_bestScoreReached)
        {
            _bestScoreReached = _scoreSum >= int.Parse(bestScoreUI.text);
        }

        if (_bestScoreReached)
        {
            bestScoreUI.DOText(_scoreSum.ToString(), GameConfig.ScoreCountSpeed, false, ScrambleMode.Numerals);
        }
        
        if (scoreImage.localScale.x == Vector3.right.x)
        {
            scoreImage.DOPunchScale(GameConfig.ScoreImageScale, GameConfig.ScoreImageSpeed);
        }

        var rot = Quaternion.Euler(0, 0, Random.Range(GameConfig.ScoreTextRotationMin, GameConfig.ScoreTextRotationMax));
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
