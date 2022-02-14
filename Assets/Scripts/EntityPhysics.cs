using UnityEngine;

public class EntityPhysics : MonoBehaviour
{
    private float freezeSpeed = 1;
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
    
    public float FreezeSpeed
    {
        get { return freezeSpeed; }
        set { freezeSpeed = value; }
    }
    
    void Update()
    {
        directionVector = directionVector + (gravityVector * Time.deltaTime * freezeSpeed);
        transform.position = transform.position + (directionVector * Time.deltaTime * freezeSpeed);
    }
}
