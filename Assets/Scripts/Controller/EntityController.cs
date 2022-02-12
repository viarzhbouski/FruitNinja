using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private EntityPhysics entityPhysics;
    
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private GameConfig gameConfig;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual void SetEntityConfig(Vector3 directionVector,
        EntityOnGameFieldChecker entityOnGameFieldChecker,
        Sprite sprite)
    {
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        spriteRenderer.sprite = sprite;
        gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        entityPhysics.GravityVector = gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
}
