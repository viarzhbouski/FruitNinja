using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] 
    private protected SpriteRenderer shadowSpriteRenderer;
    [SerializeField] 
    private protected SpriteRenderer spriteRenderer;
    [SerializeField] 
    private protected EntityPhysics entityPhysics;
    
    private protected EntityControllersProvider EntityControllersProvider;
    private protected GameConfig GameConfig;
    private protected EntityConfig EntityConfig;
    private protected bool EntityCanCut;
    
    private SpriteRenderer _shadowSpriteRenderer;
    private float _rotateSpeed;
    
    public void SetEntityConfig(Vector3 directionVector,
                                EntityConfig entityConfig,
                                EntityControllersProvider controllersProvider,
                                Sprite sprite = null)
    {
        EntityControllersProvider = controllersProvider;
        EntityConfig = entityConfig;
        GameConfig = controllersProvider.EntityOnGameFieldCheckerController.GameConfigManager.GameConfig;
        
        _rotateSpeed = Random.Range(0, EntityConfig.RotateSpeed);
        _shadowSpriteRenderer = Instantiate(shadowSpriteRenderer, transform.position, Quaternion.identity, transform.parent);
        controllersProvider.EntityRepositoryController.Entities.Add(this);
        
        _shadowSpriteRenderer.sprite = sprite == null ? EntityConfig.EntitySprite : sprite;
        spriteRenderer.sprite = sprite == null ? EntityConfig.EntitySprite : sprite;
        
        entityPhysics.GravityVector = GameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }

    private protected void EntityDestroy()
    {
        EntityControllersProvider.EntityRepositoryController.Entities.Remove(this);
        Destroy(_shadowSpriteRenderer.gameObject);
        Destroy(gameObject);
    }

    private protected void UpdateEntity()
    {
        UpdateShadowPosition();
        
        if (EntityControllersProvider.LifeCountController.GameOver)
        {
            return;
        }
        
        if (!EntityControllersProvider.EntityOnGameFieldCheckerController.EntityOnGameField(transform.position.x, transform.position.y))
        {
            if (EntityConfig.EntityType == EntityType.Fruit)
            {
                EntityControllersProvider.LifeCountController.DecreaseLife();
            }
            
            EntityDestroy();
        }

        EntitySwipeCheckCollision();
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? _rotateSpeed : -_rotateSpeed, Space.Self);
    }
    
    private void UpdateShadowPosition()
    {
        var entityTransform = transform;
        var entityPosition = entityTransform.position;
        entityPosition.y = entityPosition.y - 1.5f;
        entityPosition.z = entityPosition.z + 1;

        var spriteTransform = _shadowSpriteRenderer.transform;
        spriteTransform.position = entityPosition;
        spriteTransform.rotation = entityTransform.rotation;
    }
    
    private void EntitySwipeCheckCollision()
    {
        var entityPosition = transform.position;
        var swipePosition = EntityControllersProvider.SwipeController.Swipe.transform.position;
        var from = new Vector3(entityPosition.x, entityPosition.y, 0);
        var to = new Vector3(swipePosition.x, swipePosition.y, 0);
        var distance = Vector3.Distance(from, to);

        if (distance <= GameConfig.MinDistanceForCutFruit && 
            EntityControllersProvider.SwipeController.Velocity > GameConfig.MinVelocityForCutFruit)
        {
            EntityCanCut = true;
        }
    }

    public void PushEntity(Vector3 vector)
    {
        entityPhysics.DirectionVector = vector;
    }
}
