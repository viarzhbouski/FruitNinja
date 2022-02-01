using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnZone", menuName = "Spawn Zone", order = 51)]
public class SpawnZone : ScriptableObject
{
    [Range(-10, 10)]
    public int From;
    [Range(-10, 10)]
    public int To;
    
    public int MinAngle;
    public int MaxAngle;
    
    [Range(0, 1)]
    public float Ratio;

    public SpawnZonePosition SpawnZonePosition;
}
