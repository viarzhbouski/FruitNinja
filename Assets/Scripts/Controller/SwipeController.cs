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
    
    private Vector3 velocityVector;
    private Vector3 previousPosition;
    private float velocity;
    
    public float Velocity
    {
        get { return velocity; }
    }
    
    public GameObject Swipe
    {
        get { return swipe; }
    }
    
    void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        previousPosition = swipe.transform.position;
        
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
            var swipePosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);
            swipe.transform.position = swipePosition;

            if (!swipe.activeSelf)
            {
                previousPosition = swipe.transform.position;
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
            var swipePosition = new Vector3(positon.x, positon.y, canvasRectTransform.position.z - 1);
            swipe.transform.position = swipePosition;
            
            if (!swipe.activeSelf)
            {
                previousPosition = swipe.transform.position;
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
        var currentVelocity = (swipe.transform.position - previousPosition) / Time.deltaTime;
        velocityVector = Vector3.Lerp(velocityVector, currentVelocity, 0.1f);
        velocity = velocityVector.magnitude;
    }
}
