using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardSwipingManager : MonoBehaviour
{
    public GameObject currentFrontCard;
    private const float CARD_RIGHT_DESTINATION = 640f;
    private const float CARD_LEFT_DESTINATION = -200f;
    private const float TRANSITION_TIME = 0.25f;
    private Vector3 currentCardInitPosition;

    void Start()
    {
        SwipeDetector.OnSwipe += DetectedSwipe;
        currentCardInitPosition = currentFrontCard.transform.position;
    }

    private void DetectedSwipe(SwipeData data)
    {
        if (data.swipeDirection == SwipeDirection.Left)
        {
            SwipeLeft();
        }
        else if (data.swipeDirection == SwipeDirection.Right)
        {
            SwipeRight();
        }
    }

    public void SwipeRight()
    {

        CardSwipeAnimation(CARD_RIGHT_DESTINATION);
        StartCoroutine(ChangeSibling(true));
    }

    public void SwipeLeft()
    {
        CardSwipeAnimation(CARD_LEFT_DESTINATION);
        StartCoroutine(ChangeSibling(false));
    }

    private IEnumerator ChangeSibling(bool _isYes)
    {
        yield return new WaitForSeconds(TRANSITION_TIME);

        currentFrontCard.transform.SetAsFirstSibling();
        currentFrontCard.transform.position = currentCardInitPosition;
        currentFrontCard = currentFrontCard.transform.parent.GetChild(1).gameObject;
        GameApp.game.ValidateAction(_isYes);
        GameApp.game.LoadObject();

    }

    private void CardSwipeAnimation(float destination)
    {
        currentFrontCard.transform.DOMoveX(destination, TRANSITION_TIME);
    }
}
