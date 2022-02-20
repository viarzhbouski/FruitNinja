using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityController : MonoBehaviour
{
    [SerializeField] 
    private protected SpriteRenderer shadowSpriteRenderer;
    [SerializeField] 
    private protected SpriteRenderer spriteRenderer;
    [SerializeField] 
    private protected EntityPhysicsController entityPhysicsController;
    
    private protected EntityControllersProvider EntityControllersProvider;
    private protected GameConfig GameConfig;
    private protected EntityConfig EntityConfig;
    private protected bool EntityCanCut;
    
    private SpriteRenderer _shadowSpriteRenderer;
    private bool _canSwipe;
    private float _rotateSpeed;
    
    private const float ShadowOffsetX = 1f;
    private const float ShadowOffsetY = 1.5f;
    private const float DefaultEntityScale = 1f;

    public void SetEntityConfig(Vector3 directionVector,
                                EntityConfig entityConfig,
                                EntityControllersProvider controllersProvider,
                                Sprite sprite = null)
    {
        EntityControllersProvider = controllersProvider;
        EntityConfig = entityConfig;
        GameConfig = controllersProvider.EntityOnGameFieldCheckerController.GameConfigManager.GameConfig;
        _shadowSpriteRenderer = Instantiate(shadowSpriteRenderer, transform.position, Quaternion.identity, transform.parent);
        controllersProvider.EntityRepositoryController.Entities.Add(this);

        _rotateSpeed = Random.Range(0, 2) == 0 ? -EntityConfig.RotateSpeed : EntityConfig.RotateSpeed;
        _shadowSpriteRenderer.sprite = sprite == null ? EntityConfig.EntitySprite : sprite;
        spriteRenderer.sprite = sprite == null ? EntityConfig.EntitySprite : sprite;
        
        entityPhysicsController.GravityVector = GameConfig.GravityVector;
        entityPhysicsController.DirectionVector = directionVector;
        _canSwipe = true;
        
        if (EntityConfig.EntityType != EntityType.FruitFragment)
        {
            EntityScale();
        }
    }

    private void EntityScale()
    {
        transform.DOScale(EntityConfig.ScaleSizeLimit, EntityConfig.ScaleSpeed).onComplete += () =>
        {
            transform.DOScale(DefaultEntityScale, EntityConfig.ScaleSpeed);
        };
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
        EntityRotate();
        
        if (!EntityControllersProvider.EntityOnGameFieldCheckerController.EntityOnGameField(transform.position.x, transform.position.y))
        {
            if (EntityConfig.EntityType == EntityType.Fruit)
            {
                EntityControllersProvider.LifeCountController.DecreaseLife();
            }
            
            EntityDestroy();
        }
        
        if (EntityControllersProvider.LifeCountController.GameOver)
        {
            return;
        }

        EntitySwipeCheckCollision();
    }

    private void EntityRotate()
    {
        transform.Rotate(Quaternion.identity.x, Quaternion.identity.y, _rotateSpeed, Space.Self);
    }

    private void UpdateShadowPosition()
    {
        var entityTransform = transform;
        var entityPosition = entityTransform.position;
        entityPosition.y = entityPosition.y - ShadowOffsetY;
        entityPosition.z = entityPosition.z + ShadowOffsetX;

        var spriteTransform = _shadowSpriteRenderer.transform;
        spriteTransform.position = entityPosition;
        spriteTransform.rotation = entityTransform.rotation;
    }
    
    public void DisableCutEntityByDelay(float delay)
    {
        _canSwipe = false;
        StartCoroutine(DisableCutEntity(delay));

    }

    IEnumerator DisableCutEntity(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canSwipe = true;
    }
    
    private void EntitySwipeCheckCollision()
    {
        if (!_canSwipe)
        {
            return;
        }
        
        var entityPosition = transform.position;
        var swipePosition = EntityControllersProvider.SwipeController.Swipe.transform.position;
        var from = new Vector2(entityPosition.x, entityPosition.y);
        var to = new Vector2(swipePosition.x, swipePosition.y);
        var distance = Vector3.Distance(from, to);

        if (distance <= GameConfig.MinDistanceForCutFruit && 
            EntityControllersProvider.SwipeController.Velocity > GameConfig.MinVelocityForCutFruit)
        {
            EntityCanCut = true;
        }
    }

    public void PushEntity(Vector3 vector)
    {
        entityPhysicsController.DirectionVector = vector;
    }
}
