using UnityEngine;

public class EntityPhysicsController : MonoBehaviour
{
    private Vector3 _gravityVector;
    private Vector3 _directionVector;

    public Vector3 GravityVector
    {
        get { return _gravityVector; }
        set { _gravityVector = value; }
    }
    
    public Vector3 DirectionVector
    {
        get { return _directionVector; }
        set { _directionVector = value; }
    }
    
    private void Update()
    {
        _directionVector = _directionVector + (_gravityVector * Time.deltaTime);
        transform.position = transform.position + (_directionVector * Time.deltaTime);
    }
}
