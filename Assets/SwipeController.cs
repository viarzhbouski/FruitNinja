using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public float SwipeThreshold = 20f;
    public bool IsSwipe;
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        var vertical = Mathf.Abs(fingerDown.y - fingerUp.y);
        var horizontal = Mathf.Abs(fingerDown.x - fingerUp.x);
        
        if (vertical > SwipeThreshold && vertical > horizontal)
        {
            if (fingerDown.y - fingerUp.y > 0 ||
                fingerDown.y - fingerUp.y < 0)
            {
                IsSwipe = true;
            }
            fingerUp = fingerDown;
        }
        else if (horizontal > SwipeThreshold && horizontal > vertical)
        {
            if (fingerDown.x - fingerUp.x > 0 ||
                fingerDown.x - fingerUp.x < 0)
            {
                IsSwipe = true;
            }
            fingerUp = fingerDown;
        }
    }
}
