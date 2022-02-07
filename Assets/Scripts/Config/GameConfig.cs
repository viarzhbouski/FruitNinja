using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig", menuName = "Game Config", order = 52)]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private float xMinBorder;
    [SerializeField]
    private float xMaxBorder;
    [SerializeField]
    private float yMinBorder;
    [SerializeField]
    private float yMaxBorder;
    [SerializeField]
    private float spawnFruitPackDelay;
    [SerializeField]
    private float spawnFruitDelay;
    [SerializeField]
    private float delayBeforeStart;
    [SerializeField]
    private int lifeCount;
    [SerializeField]
    private Vector3 gravityVector;
    [SerializeField]
    private List<FruitConfig> fruits;
    [SerializeField]
    private List<SpawnZoneConfig> spawnZones;
    [SerializeField]
    private float minDistanceForCutFruit;
    [SerializeField]
    private float minVelocityForCutFruit;
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
    private ParticleSystem cutEffect;
    [SerializeField]
    private ParticleSystem sprayEffect;

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
    public ParticleSystem SprayEffect
    {
        get { return sprayEffect; }
    }
}
