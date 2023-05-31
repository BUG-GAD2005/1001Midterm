using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public enum GameMode
{
    SinglePlayer,
    SinglePlayerVsAI,
    LocalMultiplayer,
    OnlineMultiplayer
}
public class GameModeBase : MonoBehaviour
{
    internal Shape selectedShape;
    internal ShapeHolder[] shapeHolders;
    internal PhotonView photonView;
    internal bool isOnline;
    internal int team;
    internal virtual void Awake()
    {
        Initialize();
    }

    internal virtual void Start()
    {
        isOnline = GameManager.instance.isOnline;
        team = GameManager.instance.team;
        if (isOnline)
        {
            photonView = gameObject.GetComponent<PhotonView>();
            photonView.ViewID = 800;    
        }
        
        GameStart();
        TurnManager.instance.NextTurn();
    }

     internal virtual void OnEnable()
     {
         EventManager.instance.OnShapeInteracted += ShapeInteracted;
         EventManager.instance.OnShapeDragged += ShapeDragging;
         EventManager.instance.OnShapeDraggingEnd += ShapeDraggingEnd;
     }
    
     internal virtual void OnDisable()
     {
         EventManager.instance.OnShapeInteracted -= ShapeInteracted;
         EventManager.instance.OnShapeDragged -= ShapeDragging;
         EventManager.instance.OnShapeDraggingEnd -= ShapeDraggingEnd;
     }
    internal virtual void Initialize()
    {
        shapeHolders = transform.root.GetComponent<GameModeManager>().shapeHolders;
    }

    internal virtual void GameStart()
    {
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.SpawnShape(0,i);
        }
    }
    
    internal virtual void ShapeInteracted(int _team, int index)
    {
        Shape shape = shapeHolders[_team == team ? 0 : 1].GetShape(index);
        selectedShape = shape;
    }

    internal virtual void ShapeDragging(int _team,Vector2 pos)
    {
        selectedShape.transform.position = pos;
        TileManager.instance.Preview(selectedShape);
    }

    internal virtual void ShapeDraggingEnd()
    {
        if (TileManager.instance.Preview(selectedShape))
        {
            TileManager.instance.Fill(selectedShape);
            if(GameManager.instance.gameMode != GameMode.SinglePlayer)
                TurnManager.instance.NextTurn();
                
            GameManager.instance.SpawnShape(selectedShape.team,selectedShape.transform.parent.GetSiblingIndex());
            
            Destroy(selectedShape.gameObject);
            
        }
        else
        {
            selectedShape.TurnBack();
        }
        selectedShape = null;
    }
}
