using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    public static Game game;
    public GameObject contentManager;
    public GameObject panelCards;
    public GameObject panelProfile;

    void Start()
    {
        game = new Game(contentManager, panelCards, panelProfile);
    }

    void Update()
    {
        game.Update();
    }
}
