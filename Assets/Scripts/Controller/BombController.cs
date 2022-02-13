using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] 
    private EntityPhysics entityPhysics;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer shadowSpriteRenderer;
    
    private SpriteRenderer _shadow;
    private GameConfig _gameConfig;
    private BombConfig _bombConfig;
    private SwipeController _swipeController;
    private LifeCountController _lifeCountController;
    private EntityOnGameFieldChecker _entityOnGameFieldChecker;
    private float _rotateSpeed;
    private bool _bombCanCut;
    
    void Update()
    {
        UpdateShadowPosition();
        
        if (_bombCanCut)
        {
            _lifeCountController.DecreaseLife();
            SpawnExplodeEffect();
            Destroy(_shadow.gameObject);
            Destroy(gameObject);
        }
        
        if (!_entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            Destroy(_shadow.gameObject);
            Destroy(gameObject);
        }

        BombSwipeCheckCollision();
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? _rotateSpeed : -_rotateSpeed, Space.Self);
    }
    
    public void SetBombConfig(Vector3 directionVector,
        BombConfig bombConfig, 
        SwipeController swipeController,
        LifeCountController lifeCountController,
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        _bombConfig = bombConfig;
        _swipeController = swipeController;
        _lifeCountController = lifeCountController;
        _entityOnGameFieldChecker = entityOnGameFieldChecker;
        _rotateSpeed = Random.Range(0, _bombConfig.BombRotateSpeed);
        _gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        
        _shadow = Instantiate(shadowSpriteRenderer, transform.position, Quaternion.identity, transform.parent);
        _shadow.sprite = spriteRenderer.sprite;
        entityPhysics.GravityVector = _gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
    
    private void UpdateShadowPosition()
    {
        var fruitPosition = transform.position;
        fruitPosition.y = fruitPosition.y - 1.5f;
        fruitPosition.z = fruitPosition.z + 1;
        
        _shadow.transform.position = fruitPosition;
        _shadow.transform.rotation = transform.rotation;
    }
    
    private void BombSwipeCheckCollision()
    {
        var from = new Vector3(transform.position.x, transform.position.y, 0);
        var to = new Vector3(_swipeController.Swipe.transform.position.x, _swipeController.Swipe.transform.position.y, 0);
        var distance = Vector3.Distance(from, to);

        if (distance <= _gameConfig.MinDistanceForCutFruit && 
            _swipeController.Velocity > _gameConfig.MinVelocityForCutFruit)
        {
            _bombCanCut = true;
        }
    }
    
    private void SpawnExplodeEffect()
    {
        Instantiate(_bombConfig.ExplodeEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}
