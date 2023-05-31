using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public struct FillInfo
{
    public int[] paintedTiles;
    public Vector2Int leftTopCoord;
}
public class OnlineMultiplayerMode : GameModeBase
{
    public Transform TestImage;
    internal override void Initialize()
    {
        TestImage = GameObject.Find("TestImage").transform;
        base.Initialize();
        shapeHolders[1].gameObject.SetActive(true);
    }

    internal override void GameStart()
    {
        if(!PhotonNetwork.IsMasterClient) return;
        
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameManager.instance.SpawnShape(i,j,true);
            }
        }
    }
    
    internal override void ShapeDraggingEnd()
    {
        if (TileManager.instance.Preview(selectedShape))
        {
            FillInfo info = TileManager.instance.Fill(selectedShape);
            RpcManager.instance.PlayerFilled(info.paintedTiles,(Vector2)info.leftTopCoord, selectedShape.index);
               
            Destroy(selectedShape.gameObject);
        }
        else
        {
            selectedShape.TurnBack();
        }
        selectedShape = null;
        Debug.LogError(shapeHolders[0].HolderHasShape());
        if (!shapeHolders[0].HolderHasShape())
        {
            for (int i = 0; i < 3; i++)
            {
                GameManager.instance.SpawnShape(team,i,true);
            }
        }
    }

    public void OpponentFills(int[] paintedTiles, Vector2Int topLeftCoord, int index)
    {
        var shape = shapeHolders[1].GetShape(index);
        var targetPos = TileManager.instance.GetTilePos(topLeftCoord) + new Vector3(-121, 121, 0);

        Debug.LogError("pos " + TileManager.instance.GetTilePos(topLeftCoord) + "  " + targetPos);
        StartCoroutine(OpponentShapeMovement(shape, targetPos, paintedTiles, topLeftCoord));
    }

    private IEnumerator OpponentShapeMovement(Shape shape, Vector3 targetPos,int[] paintedTiles, Vector2Int topLeftCoord)
    {
        Vector3 pos = shape.transform.position;
        var col = shape.pieceColor;
        for (int i = 0; i < 51; i++)
        {
            shape.transform.position = Vector3.Lerp(pos, targetPos, i/50f);
            yield return null;
        }

        TestImage.position = targetPos;
        TileManager.instance.Fill(paintedTiles,topLeftCoord,col);
        Destroy(shape.gameObject);
        TurnManager.instance.NextTurn();
    }
}
