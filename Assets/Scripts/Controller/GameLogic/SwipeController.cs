using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private GameObject swipe;
    [SerializeField]
    private bool mobile;
    
    private Vector3 _velocityVector;
    private Vector3 _previousPosition;
    private float _velocity;
    
    public float Velocity
    {
        get { return _velocity; }
    }
    
    public GameObject Swipe
    {
        get { return swipe; }
    }
    
    public Camera Camera
    {
        get { return camera; }
    }
    
    void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        _previousPosition = swipe.transform.position;
        
        switch (mobile)
        {
            case true:
                TouchInput();
                break;
            case false:
                PcInput();
                break;
        }

        CalculateSwipeVelocity();
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var positon = camera.ScreenToWorldPoint(touch.position);
            var swipePosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 5);
            swipe.transform.position = swipePosition;

            if (!swipe.activeSelf)
            {
                _previousPosition = swipe.transform.position;
                swipe.SetActive(true);
            }
        }
        else
        {
            swipe.SetActive(false);
        }
    }

    private void PcInput()
    {
        if (Input.GetMouseButton(0))
        {
            var positon = camera.ScreenToWorldPoint(Input.mousePosition);
            var swipePosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 5);
            swipe.transform.position = swipePosition;
            
            if (!swipe.activeSelf)
            {
                _previousPosition = swipe.transform.position;
                swipe.SetActive(true);
            }
        }
        else
        {
            swipe.SetActive(false);
        }
    }

    private void CalculateSwipeVelocity()
    {
        var currentVelocity = (swipe.transform.position - _previousPosition) / Time.deltaTime;
        _velocityVector = Vector3.Lerp(_velocityVector, currentVelocity, 0.1f);
        _velocity = _velocityVector.magnitude;
    }
}
