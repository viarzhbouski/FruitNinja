using System;
using UnityEngine;

public class TapMovement : MonoBehaviour
{
    private RectTransform canvasRectTransform;
    public bool Mobile;
    private bool isKeyUp = true;
    private Vector3 old;

    private float velocity;
    
    public float Velocity
    {
        get { return velocity; }
    }
    
    private void Start()
    {
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        Vector3 positon = Vector3.zero;
        if (Mobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                positon = Camera.main.ScreenToWorldPoint(touch.position);
                isKeyUp = false;
            }
            else
            {
                isKeyUp = true;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                positon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isKeyUp = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isKeyUp = true;
            }
        }
        old = transform.position;;
        if (!isKeyUp)
        {
            
            transform.position = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);
        }

        CalculateVelocity();
    }

    private void CalculateVelocity()
    {
        var distance = Vector3.Distance(old, transform.position);
        velocity = distance;
    }
}
