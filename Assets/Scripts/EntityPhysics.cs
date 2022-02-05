using UnityEngine;

public class EntityPhysics : MonoBehaviour
{
    private Vector3 gravityVector;
    private Vector3 directionVector;

    public Vector3 GravityVector
    {
        get { return gravityVector; }
        set { gravityVector = value; }
    }
    
    public Vector3 DirectionVector
    {
        get { return directionVector; }
        set { directionVector = value; }
    }
    
    void Update()
    {
        directionVector = directionVector + gravityVector * Time.deltaTime;
        transform.position = transform.position + directionVector * Time.deltaTime;
    }
}
