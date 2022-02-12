using UnityEngine;

public class FruitFragmentController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private EntityPhysics entityPhysics;
    [SerializeField]
    private GameObject shadowPrefab;
    
    private GameObject shadow;
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private float fragmentRotateSpeed;

    private void Start()
    {
        shadow = Instantiate(shadowPrefab, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void Update()
    {
        UpdateShadowPosition();
        
        if (!entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            Destroy(shadow);
            Destroy(gameObject);
        }
        
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? fragmentRotateSpeed : -fragmentRotateSpeed, Space.Self);
    }
    
    private void UpdateShadowPosition()
    {
        var fruitPosition = transform.position;
        fruitPosition.y = fruitPosition.y - 1.5f;
        fruitPosition.z = fruitPosition.z + 1;
        
        shadow.transform.position = fruitPosition;
    }
    
    public void SetFruitFragmentConfig(Vector3 directionVector, EntityOnGameFieldChecker entityOnGameFieldChecker, Sprite sprite, float fragmentRotateSpeed)
    {
        spriteRenderer.sprite = sprite;
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        this.fragmentRotateSpeed = Random.Range(0, fragmentRotateSpeed);
        
        entityPhysics.GravityVector = entityOnGameFieldChecker.GameConfigManager.GameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
}
