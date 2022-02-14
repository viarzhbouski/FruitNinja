using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig", menuName = "Game Config", order = 52)]
public class GameConfig : ScriptableObject
{
    [Header("\tMAIN")]
    [SerializeField]
    private float delayBeforeStart;
    [SerializeField]
    private int lifeCount;
    [SerializeField]
    private Vector3 gravityVector;
    
    [Header("\tGAMEFIELD BORDERS")]
    [SerializeField]
    private float xMinBorder;
    [SerializeField]
    private float xMaxBorder;
    [SerializeField]
    private float yMinBorder;
    [SerializeField]
    private float yMaxBorder;

    [Header("\tENTITY CONFIGS")]
    [SerializeField]
    private List<FruitConfig> fruits;
    [SerializeField]
    private FruitFragmentConfig fruitFragmentConfig;
    [SerializeField]
    private BombConfig bomb;
    [SerializeField]
    private BonusLifeConfig bonusLifeConfig;
    [SerializeField]
    private BonusFreezeConfig bonusFreezeConfig;
    
    [Header("\tSPAWNZONE CONFIGS")]
    [SerializeField]
    private List<SpawnZoneConfig> spawnZones;
    
    [Header("\tFRUIT CUT SETTINGS")]
    [SerializeField]
    private float minDistanceForCutFruit;
    [SerializeField]
    private float minVelocityForCutFruit;
    
    [Header("\tDIFFICULT")]
    [SerializeField]
    private int fruitCountInPack;
    [SerializeField]
    private int maxFruitCountInPack;
    [SerializeField]
    private int cuttedFruitsForEncreaseFruitInPack;
    [SerializeField]
    private int cuttedFruitsForDecreaseFruitDelay;
    [SerializeField]
    private float subtractForFruitDelay;
    [SerializeField]
    private float subtractForFruitPackDelay;
    [SerializeField]
    private float spawnFruitPackDelay;
    [SerializeField]
    private float spawnFruitDelay;
    
    [Header("\tEFFECTS")]
    [SerializeField]
    private ParticleSystem cutEffect;
    [SerializeField]
    private ParticleSystem cutBonusLifeEffect;
    [SerializeField]
    private ParticleSystem cutBonusFreezeEffect;
    [SerializeField]
    private ParticleSystem sprayEffect;
    [SerializeField]
    private ParticleSystem explodeEffect;
    
    [Header("\tSCORE TEXT")]
    [SerializeField]
    private int scoreTextRotationMin;
    [SerializeField]
    private int scoreTextRotationMax;
    
    [Header("\tCOMBO")]
    [SerializeField]
    private int comboMax;
    [SerializeField]
    public float comboTime;
    [SerializeField]
    public float comboMultiplierTime;
    
    public int ComboMax
    {
        get { return comboMax; }
    }
    
    public float ComboTime
    {
        get { return comboTime; }
    }
    
    public float ComboMultiplierTime
    {
        get { return comboMultiplierTime; }
    }
    
    public int ScoreTextRotationMin
    {
        get { return scoreTextRotationMin; }
    }
    
    public int ScoreTextRotationMax
    {
        get { return scoreTextRotationMax; }
    }
    
    public float XMinBorder
    {
        get { return xMinBorder; }
    }
    
    public float XMaxBorder
    {
        get { return xMaxBorder; }
    }
    
    public float YMinBorder
    {
        get { return yMinBorder; }
    }
    
    public float YMaxBorder
    {
        get { return yMaxBorder; }
    }
    
    public float SpawnFruitPackDelay
    {
        get { return spawnFruitPackDelay; }
    }
    
    public float SpawnFruitDelay
    {
        get { return spawnFruitDelay; }
    }
    
    public float DelayBeforeStart
    {
        get { return delayBeforeStart; }
    }

    public int LifeCount
    {
        get { return lifeCount; }
    }
    
    public Vector3 GravityVector
    {
        get { return gravityVector; }
    }
    
    public List<FruitConfig> Fruits
    {
        get { return fruits; }
    }
    
    public FruitFragmentConfig FruitFragment
    {
        get { return fruitFragmentConfig; }
    }
    
    public BombConfig Bomb
    {
        get { return bomb; }
    }
    
    public BonusLifeConfig BonusLife
    {
        get { return bonusLifeConfig; }
    }
    
    public BonusFreezeConfig BonusFreeze
    {
        get { return bonusFreezeConfig; }
    }
    
    public List<SpawnZoneConfig> SpawnZones
    {
        get { return spawnZones; }
    }
    
    public float MinDistanceForCutFruit
    {
        get { return minDistanceForCutFruit; }
    }
    
    public float MinVelocityForCutFruit
    {
        get { return minVelocityForCutFruit; }
    }
    
    public int FruitCountInPack
    {
        get { return fruitCountInPack; }
    }
    
    public int MaxFruitCountInPack
    {
        get { return maxFruitCountInPack; }
    }
    
    public int CuttedFruitsForEncreaseFruitInPack
    {
        get { return cuttedFruitsForEncreaseFruitInPack; }
    }
    
    public int CuttedFruitsForDecreaseFruitDelay
    {
        get { return cuttedFruitsForDecreaseFruitDelay; }
    }
    
    public float SubtractForFruitDelay
    {
        get { return subtractForFruitDelay; }
    }
    
    public float SubtractForFruitPackDelay
    {
        get { return subtractForFruitPackDelay; }
    }
    
    public ParticleSystem CutEffect
    {
        get { return cutEffect; }
    }
    
    public ParticleSystem CutBonusLifeEffect
    {
        get { return cutBonusLifeEffect; }
    }
    
    public ParticleSystem CutBonusFreezeEffect
    {
        get { return cutBonusFreezeEffect; }
    }
    
    public ParticleSystem SprayEffect
    {
        get { return sprayEffect; }
    }
    
    public ParticleSystem ExplodeEffect
    {
        get { return explodeEffect; }
    }
}
