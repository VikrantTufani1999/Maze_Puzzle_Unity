using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if(isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Crossing the area range
        if(swipeDelta.magnitude > 125)
        {
            //Direction of swipe

            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or right
                if (x < 0)
                    swipeRight = true;
                else
                    swipeLeft = true;
            }
            else
            {
                //Up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta {  get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
            SceneManager.LoadScene(2);
    }
}
