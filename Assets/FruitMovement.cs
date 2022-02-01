using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitMovement : MonoBehaviour
{
    private TapMovement tapMovement;
    private GameObject tapObject;
    private Vector3 gravityVector;
    private Vector3 directionVector;
    
    public GameObject FirstShape;
    public GameObject SecondShape;

    private bool fruitIsCatched;
    
    void FixedUpdate()
    {
        if (fruitIsCatched)
        {
            SpawnShapes();
            Destroy(gameObject);
            return;
        }

        Movement();
        FruitTapCollision();

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

    public void SetMovementConfig(Vector3 directionVector, Vector3 gravityVector, GameObject tapObject)
    {
        this.directionVector = directionVector;
        this.gravityVector = gravityVector;
        this.tapObject = tapObject;
        tapMovement = tapObject.GetComponent<TapMovement>();
    }

    private bool FruitOnGameField()
    {
        if (transform.position.x >= -10 && transform.position.x <= 10 &&
            transform.position.y >= -5 && transform.position.y <= 10)
        {
            return true;
        }

        return false;
    }

    private void FruitTapCollision()
    {
        var from = new Vector3(transform.position.x, transform.position.y, 0);
        var to = new Vector3(tapObject.transform.position.x, tapObject.transform.position.y, 0);

        var distance = Vector3.Distance(from, to);
        Debug.DrawLine(from, to, Color.green);

        if (distance <= 0.7f)
        {
            fruitIsCatched = true;
        }
    }

    private void SpawnShapes()
    {
        GameObject shape1 = Instantiate(FirstShape, transform.position, Quaternion.identity, transform.parent);
        GameObject shape2 = Instantiate(SecondShape, transform.position, Quaternion.identity, transform.parent);
        
        shape1.GetComponent<FruitShape>().SetMovementConfig(Vector3.left * Random.Range(2,4), gravityVector);
        shape2.GetComponent<FruitShape>().SetMovementConfig(Vector3.right * Random.Range(2,4), gravityVector);
    }
}
