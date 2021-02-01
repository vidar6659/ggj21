using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public ContentManager contentManager;
    private Profile currentProfile;
    private int profileIndex;
    private int objectsIndex;
    public static GameState gameState;
    private int rightGuesses;
    private int wrongGuesses;
    private const int GUESSES_TO_WIN = 8;
    private const int GUESSES_TO_LOSE = 3;
    private int rightProfiles;
    private int wrongProfiles;
    private const int PROFILES_TO_WIN = 3;
    private const int PROFILES_TO_LOSE = 2;
    private GameObject panelCards;

    public Game(GameObject _contentManager, GameObject _panelCards, GameObject _panelProfile)
    {
        contentManager = _contentManager.GetComponent<ContentManager>();
        gameState = GameState.READY;
        contentManager.GenerateProfiles();
        profileIndex = 0;
        objectsIndex = 0;
        rightGuesses = 0;
        wrongGuesses = 0;
        panelCards = _panelCards;
        currentProfile = contentManager.sessionProfiles[profileIndex++];
        _panelProfile.transform.GetChild(0).GetComponent<Image>().sprite = currentProfile.image;
        _panelProfile.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = currentProfile.desc;
        panelCards.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentProfile.lostObjs[objectsIndex].image;
        panelCards.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = currentProfile.lostObjs[objectsIndex].desc;
        panelCards.transform.GetChild(0).GetComponent<ObjectCard>().isOwned = currentProfile.lostObjs[objectsIndex++].isOwned;
        panelCards.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = currentProfile.lostObjs[objectsIndex].image;
        panelCards.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = currentProfile.lostObjs[objectsIndex++].desc;
        panelCards.transform.GetChild(1).GetComponent<ObjectCard>().isOwned = currentProfile.lostObjs[objectsIndex++].isOwned;
    }

    public void Update()
    {
        switch (gameState)
        {
            case GameState.READY:
                break;
            case GameState.PLAY:
                break;
            case GameState.END:
                break;
        }
    }

    public void ValidateAction(bool isYes)
    {
        if (isYes)
        {
            if (panelCards.transform.GetChild(1).GetComponent<ObjectCard>().isOwned)
                rightGuesses++;
            else
                wrongGuesses++;

        }
        else
        {
            if (panelCards.transform.GetChild(1).GetComponent<ObjectCard>().isOwned)
                wrongGuesses++;
            else
                rightGuesses++;
        }
        if (rightGuesses == GUESSES_TO_WIN)
        {
            rightProfiles++;
            ChangeProfile();
        }
        else if (wrongGuesses == GUESSES_TO_LOSE)
        {
            wrongProfiles++;
            ChangeProfile();
        }
        if (rightProfiles == PROFILES_TO_WIN)
            gameState = GameState.END;
        else if (wrongProfiles == PROFILES_TO_LOSE)
            gameState = GameState.END;

    }

    public void LoadObject()
    {
        if (gameState == GameState.PLAY)
        {
            if (objectsIndex < currentProfile.lostObjs.Count)
            {
                panelCards.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentProfile.lostObjs[objectsIndex].image;
                panelCards.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = currentProfile.lostObjs[objectsIndex].desc;
                panelCards.transform.GetChild(0).GetComponent<ObjectCard>().isOwned = currentProfile.lostObjs[objectsIndex++].isOwned;
            }
        }
    }

    private void ChangeProfile()
    {
        if (gameState == GameState.PLAY)
        {
            currentProfile = contentManager.sessionProfiles[profileIndex++];
        }
    }
}

public enum GameState
{
    READY,
    PLAY,
    END
}
