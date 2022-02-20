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
    private float scaleSpeed;
    
    [SerializeField]
    private float scaleSizeLimit;
    
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
    
    public float ScaleSpeed
    {
        get { return scaleSpeed; }
    }
    
    public float ScaleSizeLimit
    {
        get { return scaleSizeLimit; }
    }
    
    public EntityType EntityType
    {
        get { return entityType; }
    }
}
