using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public float SpawnDelay = 1;
    
    public float Speed;
    
    public Vector3 GravityVector;
    
    public List<GameObject> Fruits;
    
    public List<SpawnZone> SpawnZones;

    private void Start()
    {
        if (!SpawnZones.Any())
        {
            var defaultSpawnZone = new SpawnZone
            {
                From = -5,
                To = 5,
                MinAngle = 60,
                MaxAngle = 120,
                SpawnZonePosition = SpawnZonePosition.Bottom
            };
            
            SpawnZones.Add(defaultSpawnZone);
        }
    }
}
