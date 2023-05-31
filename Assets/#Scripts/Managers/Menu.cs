using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SelectMode(int modeIndex)
    {
        GameMode mode = (GameMode)modeIndex;
        GameManager.instance.gameMode = mode;
        if (mode == GameMode.OnlineMultiplayer)
        {
            Server.instance.JoinLobby();
            return;
        }

        SceneManager.LoadScene("Game");
    }
}
