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
    
    void Start()
    {
        _fruitDelay = gameConfigController.GameConfig.SpawnFruitDelay;
        _fruitPackDelay = gameConfigController.GameConfig.SpawnFruitPackDelay;
        _fruitCountInPack = gameConfigController.GameConfig.FruitCountInPack;
        scoreCountController.DifficultyDelayEvent.AddListener(DecreaseDelay);
        scoreCountController.DifficultyFruitPackEvent.AddListener(EncreaseFruitInPack);
    }

    public void DecreaseDelay()
    {
        if (_fruitDelay > 0.05)
        {
            _fruitDelay -= gameConfigController.GameConfig.SubtractForFruitDelay;
        }

        if (_fruitPackDelay > 0.5)
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
