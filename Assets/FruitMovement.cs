using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField] 
    private Vector3 gravityVector;

    private Vector3 velocityVector;
    private void Start()
    {
        var angleRadians = Random.Range(50, 90) * Mathf.PI / 180;
        var lengthX = Mathf.Cos(angleRadians);
        
        velocityVector = new Vector3(lengthX, 1 - lengthX, 0) * speed;
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    
    
    private void Movement()
    {
        velocityVector = velocityVector + gravityVector * Time.deltaTime;
        transform.position = transform.position + velocityVector * Time.deltaTime;
    }
}
