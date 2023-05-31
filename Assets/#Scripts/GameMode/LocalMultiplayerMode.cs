using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerMode : GameModeBase
{
    internal override void GameStart()
    {
        shapeHolders[1].gameObject.SetActive(true);
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                GameManager.instance.SpawnShape(j,i);
            }
        }
    }
}
