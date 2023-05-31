using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Mode
{
    public GameMode mode;
    public GameObject modeObject;
}
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;
    [SerializeField] private List<Mode> gameModes;

    public ShapeHolder[] shapeHolders;
    public GameModeBase currentMode;
    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void SpawnModeObject(GameMode gameMode)
    {
        foreach (Mode element in gameModes)
        {
            if(element.mode != gameMode) continue;

            currentMode = Instantiate(element.modeObject, transform).GetComponent<GameModeBase>();
        }
    }
}
