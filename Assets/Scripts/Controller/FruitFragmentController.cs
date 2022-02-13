using UnityEngine;

public class FruitFragmentController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private EntityPhysics entityPhysics;
    [SerializeField]
    private SpriteRenderer shadowSpriteRenderer;
    
    private SpriteRenderer _shadow;
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private float fragmentRotateSpeed;
    
    private void Update()
    {
        UpdateShadowPosition();
        
        if (!entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            Destroy(_shadow.gameObject);
            Destroy(gameObject);
        }
        
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? fragmentRotateSpeed : -fragmentRotateSpeed, Space.Self);
    }
    
    private void UpdateShadowPosition()
    {
        var fruitPosition = transform.position;
        fruitPosition.y = fruitPosition.y - 1.5f;
        fruitPosition.z = fruitPosition.z + 1;
        
        _shadow.transform.position = fruitPosition;
        _shadow.transform.rotation = transform.rotation;
    }
    
    public void SetFruitFragmentConfig(Vector3 directionVector, EntityOnGameFieldChecker entityOnGameFieldChecker, Sprite sprite, float fragmentRotateSpeed)
    {
        spriteRenderer.sprite = sprite;
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        this.fragmentRotateSpeed = Random.Range(0, fragmentRotateSpeed);
        
        _shadow = Instantiate(shadowSpriteRenderer, transform.position, Quaternion.identity, transform.parent);
        _shadow.sprite = sprite;
        entityPhysics.GravityVector = entityOnGameFieldChecker.GameConfigManager.GameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
}
