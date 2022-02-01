using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitShape : MonoBehaviour
{
    private Vector3 gravityVector;
    private Vector3 directionVector;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gravityVector == null && directionVector == null)
        {
            return;;
        }
        
        Movement();
        if (!FruitShapeOnGameField())
        {
            Destroy(gameObject);
        }
    }
    
    public void SetMovementConfig(Vector3 directionVector, Vector3 gravityVector)
    {
        this.directionVector = directionVector;
        this.gravityVector = gravityVector;
    }
    
    private void Movement()
    {
        directionVector = directionVector + gravityVector * Time.deltaTime;
        transform.position = transform.position + directionVector * Time.deltaTime;
        transform.Rotate(0f, 0f, directionVector.x > 0 ? 0.3f : -0.3f, Space.Self);
    }
    
    private bool FruitShapeOnGameField()
    {
        if (transform.position.x >= -10 && transform.position.x <= 10 &&
            transform.position.y >= -5 && transform.position.y <= 10)
        {
            return true;
        }

        return false;
    }
}
