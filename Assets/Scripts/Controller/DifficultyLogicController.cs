using UnityEngine;
using UnityEngine.Events;

public class DifficultyLogicController : MonoBehaviour
{
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private ScoreCountController scoreCountController;

    private float fruitDelay;
    private float fruitPackDelay;
    private int fruitCountInPack;
    public float FruitDelay
    {
        get { return fruitDelay; }
    }
    
    public float FruitPackDelay
    {
        get { return fruitPackDelay; }
    }
    
    public float FruitCountInPack
    {
        get { return fruitCountInPack; }
    }
    
    void Start()
    {
        fruitDelay = gameConfigController.GameConfig.SpawnFruitDelay;
        fruitPackDelay = gameConfigController.GameConfig.SpawnFruitPackDelay;
        fruitCountInPack = gameConfigController.GameConfig.FruitCountInPack;
        scoreCountController.DifficultyDelayEvent.AddListener(DecreaseDelay);
        scoreCountController.DifficultyFruitPackEvent.AddListener(EncreaseFruitInPack);
    }

    public void DecreaseDelay()
    {
        fruitDelay -= gameConfigController.GameConfig.SubtractForFruitDelay;
        fruitPackDelay -= gameConfigController.GameConfig.SubtractForFruitPackDelay;
    }
    
    public void EncreaseFruitInPack()
    {
        if (fruitCountInPack < gameConfigController.GameConfig.MaxFruitCountInPack)
        {
            fruitCountInPack++;
        }
    }
}
