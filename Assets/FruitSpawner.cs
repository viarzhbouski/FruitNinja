using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnPoint;
    
    [SerializeField]
    private GameObject fruit;
    
    [SerializeField]
    private Canvas gameField;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fruit != null && gameField != null)
        {
            Instantiate(fruit, new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.identity, gameField.transform);
        }
    }
}
