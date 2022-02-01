using System;
using UnityEngine;

public class TapMovement : MonoBehaviour
{
    public Canvas GameField;
    public GameObject Tap;
    public bool Mobile;
    
    private GameObject tapObject;
    private RectTransform canvasRectTransform;
    private bool isKeyUp = true;
    private Vector3 old;
    private float velocity;
    
    public float Velocity
    {
        get { return velocity; }
    }
    
    public GameObject TapObject
    {
        get { return tapObject; }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        if (GameField != null)
        {
            canvasRectTransform = GameField.GetComponent<RectTransform>();
        }
    }
    
    void FixedUpdate()
    {
        if (canvasRectTransform == null)
        {
            return;
        }
        
        Vector3 positon = Vector3.zero;
        
        if (Mobile)
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                positon = Camera.main.ScreenToWorldPoint(touch.position);
                
                var tapPosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);

                if (tapObject == null)
                {
                    tapObject = Instantiate(Tap, tapPosition, Quaternion.identity, GameField.transform);
                }
            }
            else
            {
                Destroy(tapObject);
                tapObject = null;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                positon = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                var tapPosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);

                if (tapObject == null)
                {
                    tapObject = Instantiate(Tap, tapPosition, Quaternion.identity, GameField.transform);
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(tapObject);
                tapObject = null;
            }
        }
        if (tapObject != null)
        {
            old = tapObject.transform.position;;
            tapObject.transform.position = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);
            CalculateVelocity();
        }
    }

    private void CalculateVelocity()
    {
        velocity = Vector3.Distance(old, tapObject.transform.position) / Time.deltaTime;
        Debug.Log(velocity);
    }
}
