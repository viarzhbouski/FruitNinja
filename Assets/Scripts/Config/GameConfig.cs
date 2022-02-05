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
    private float spawnDelay;
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
    
    public float SpawnDelay
    {
        get { return spawnDelay; }
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
}
