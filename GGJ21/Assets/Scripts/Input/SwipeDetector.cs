using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 startPressPosition;
    private Vector2 endPressPosition;
    private Vector2 currentSwipe;

    private float mininumSwipeLength = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    public static event Action<SwipeData> OnStartSwipe = delegate { };

    public static event Action<SwipeData> OnSwiping = delegate { };

    void Start()
    {

    }

    void OnDestroy()
    {
        OnSwipe = null;
        OnSwipe = delegate { };
        OnStartSwipe = null;
        OnStartSwipe = delegate { };
        OnSwiping = null;
        OnSwiping = delegate { };
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startPressPosition = touch.position;
                TriggerOnStartSwipe();
            }
            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                currentSwipe = touch.position;
                TriggerOnSwiping();
            }
            if(touch.phase == TouchPhase.Ended)
            {
                endPressPosition = touch.position;
                currentSwipe = new Vector2(endPressPosition.x - startPressPosition.x, endPressPosition.y - startPressPosition.y);
                DetectSwipe();
            }
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            startPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            TriggerOnStartSwipe();
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPressPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(endPressPosition.x - startPressPosition.x, endPressPosition.y - startPressPosition.y);
            DetectSwipe();
        }
#endif
    }

    private void DetectSwipe()
    {
        if (isAnActualSwipe())
        {
            TriggerSwipe(DetermineDirection());
        }
    }

    private bool isAnActualSwipe()
    {
        if (currentSwipe.magnitude > mininumSwipeLength)
            return true;
        return false;
    }

    private SwipeDirection DetermineDirection()
    {
        if (isVerticalSwipe())
        {
            if ((endPressPosition.y - startPressPosition.y) > 0)
                return SwipeDirection.Up;
            else
                return SwipeDirection.Down;
        }
        else
        {
            if ((endPressPosition.x - startPressPosition.x) > 0)
                return SwipeDirection.Right;
            else
                return SwipeDirection.Left;
        }
    }

    private bool isVerticalSwipe()
    {
        if (Mathf.Abs(endPressPosition.y - startPressPosition.y) > Mathf.Abs(endPressPosition.x - startPressPosition.x))
            return true;
        return false;
    }

    private void TriggerSwipe(SwipeDirection direction)
    {
        SwipeData data = new SwipeData()
        {
            startPosition = startPressPosition,
            endPosition = endPressPosition,
            actualSwipe = currentSwipe,
            swipeDirection = direction
        };
        //Debug.Log(OnSwipe.GetInvocationList().Length);
        OnSwipe(data);
    }

    private void TriggerOnStartSwipe()
    {
        SwipeData data = new SwipeData()
        {
            startPosition = startPressPosition,
            endPosition = new Vector2(0, 0),
            actualSwipe = new Vector2(0, 0),
            swipeDirection = SwipeDirection.None
        };
        OnStartSwipe(data);
    }

    private void TriggerOnSwiping()
    {
        SwipeData data = new SwipeData()
        {
            startPosition = new Vector2(0, 0),
            endPosition = new Vector2(0, 0),
            actualSwipe = currentSwipe,
            swipeDirection = SwipeDirection.None
        };
        OnSwiping(data);
    }
}

public struct SwipeData
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 actualSwipe;
    public SwipeDirection swipeDirection;
}

public enum SwipeDirection
{
    Up,
    Down,
    Right,
    Left,
    None
}
