using UnityEngine;

public class EntityConfig : ScriptableObject
{
    [SerializeField]
    private EntityController entityController;
    
    [SerializeField]
    private Sprite entitySprite;
    
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float rotateSpeed;
    
    [SerializeField]
    private EntityType entityType;
    
    public EntityController EntityController
    {
        get { return entityController; }
    }
    
    public Sprite EntitySprite
    {
        get { return entitySprite; }
    }
    
    public float Speed
    {
        get { return speed; }
    }
    
    public float RotateSpeed
    {
        get { return rotateSpeed; }
    }
    
    public EntityType EntityType
    {
        get { return entityType; }
    }
}
