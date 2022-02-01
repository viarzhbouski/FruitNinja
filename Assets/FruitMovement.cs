using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitMovement : MonoBehaviour
{
    private Vector3 gravityVector;
    private Vector3 directionVector;
    
    void FixedUpdate()
    {
        Movement();

        if (!FruitOnGameField())
        {
            Destroy(gameObject);
        }
    }
    
    private void Movement()
    {
        directionVector = directionVector + gravityVector * Time.deltaTime;
        transform.position = transform.position + directionVector * Time.deltaTime;
    }

    public void SetMovementConfig(Vector3 directionVector, Vector3 gravityVector)
    {
        this.directionVector = directionVector;
        this.gravityVector = gravityVector;
    }

    private bool FruitOnGameField()
    {
        if (transform.position.x >= -20 && transform.position.x <= 20 &&
            transform.position.y >= -10 && transform.position.y <= 10)
        {
            return true;
        }

        return false;
    }
}
