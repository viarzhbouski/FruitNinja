using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnZone", menuName = "Spawn Zone", order = 51)]
public class SpawnZoneConfig : ScriptableObject
{
    [Range(-10, 10)]
    [SerializeField]
    private int from;
    
    [Range(-10, 10)]
    [SerializeField]
    private int to;
    
    [SerializeField]
    private int minAngle;
    
    [SerializeField]
    private int maxAngle;
    
    [Range(0, 1)]
    [SerializeField]
    private float chance;

    [SerializeField]
    private SpawnZonePosition spawnZonePosition;
    
    [Range(0.1f, 1)]
    [SerializeField]
    private float speedMultiplier;

    public int From
    {
        get { return from; }
    }
    
    public int To
    {
        get { return to; }
    }
    
    public int MinAngle
    {
        get { return minAngle; }
    }
    
    public int MaxAngle
    {
        get { return maxAngle; }
    }
    
    public float Chance
    {
        get { return chance; }
    }
    
    public SpawnZonePosition SpawnZonePosition
    {
        get { return spawnZonePosition; }
    }
    
    public float SpeedMultiplier
    {
        get { return speedMultiplier; }
    }
}
