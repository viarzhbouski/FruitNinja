using UnityEngine;

public class TapMovement : MonoBehaviour
{
    public bool Mobile;
    private bool isKeyUp = true;
    
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

        if (!isKeyUp)
        {
            transform.position = new Vector3(positon.x, positon.y, -1);
        }
    }
}
