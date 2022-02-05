using UnityEngine;

public class FruitFragmentController : MonoBehaviour
{
    [SerializeField] 
    private EntityPhysics entityPhysics;
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private float fragmentRotateSpeed;
    
    void Update()
    {
        if (!entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            Destroy(gameObject);
        }
        
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? fragmentRotateSpeed : -fragmentRotateSpeed, Space.Self);
    }
    
    public void SetFruitFragmentConfig(EntityOnGameFieldChecker entityOnGameFieldChecker, Vector3 directionVector, float fragmentRotateSpeed)
    {
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        this.fragmentRotateSpeed = fragmentRotateSpeed;
        entityPhysics.GravityVector = entityOnGameFieldChecker.GameConfigManager.GameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
}
