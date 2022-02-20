using UnityEngine;

public class DifficultyLogicController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private ScoreCountController scoreCountController;

    private float _fruitDelay;
    private float _fruitPackDelay;
    private int _fruitCountInPack;

    private const float FruitSpawnDelayLimit = 0.05f;
    private const float FruitSpawnPackDelayLimit = 0.5f;
    
    public float FruitDelay
    {
        get { return _fruitDelay; }
    }
    
    public float FruitPackDelay
    {
        get { return _fruitPackDelay; }
    }
    
    public float FruitCountInPack
    {
        get { return _fruitCountInPack; }
    }
    
    private void Start()
    {
        ResetDiffucult();
        scoreCountController.DifficultyDelayEvent.AddListener(DecreaseDelay);
        scoreCountController.DifficultyFruitPackEvent.AddListener(EncreaseFruitInPack);
    }

    public void ResetDiffucult()
    {
        _fruitDelay = gameConfigController.GameConfig.SpawnFruitDelay;
        _fruitPackDelay = gameConfigController.GameConfig.SpawnFruitPackDelay;
        _fruitCountInPack = gameConfigController.GameConfig.FruitCountInPack;
    }

    public void DecreaseDelay()
    {
        if (_fruitDelay > FruitSpawnDelayLimit)
        {
            _fruitDelay -= gameConfigController.GameConfig.SubtractForFruitDelay;
        }

        if (_fruitPackDelay > FruitSpawnPackDelayLimit)
        {
            _fruitPackDelay -= gameConfigController.GameConfig.SubtractForFruitPackDelay;
        }
    }
    
    public void EncreaseFruitInPack()
    {
        if (_fruitCountInPack < gameConfigController.GameConfig.MaxFruitCountInPack)
        {
            _fruitCountInPack++;
        }
    }
}
