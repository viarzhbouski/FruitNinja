using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public float SpawnDelay = 1;
    
    public float Speed;
    
    public Vector3 GravityVector;
    
    public List<GameObject> Fruits;
    
    public List<SpawnZone> BottomSpawnZones;
}
