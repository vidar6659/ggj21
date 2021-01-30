using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestSwipe : MonoBehaviour
{
    

    void Start()
    {
        SwipeDetector.OnSwipe += DetectedSwipe;
    }
    
    void Update()
    {
        
    }

    private void DetectedSwipe(SwipeData data)
    {
        Debug.Log(data.swipeDirection);
    }
}
