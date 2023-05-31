using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class RpcManager : MonoBehaviour
{
    public static RpcManager instance;
    private PhotonView photonView;

    private void Awake()
    {
        if (!instance) instance = this;
        photonView = GetComponent<PhotonView>();
    }

    public void SpawnShape()
    {
        //photonView.RPC("Spawn", RpcTarget.All,_team == 1 ? 0 : 1, _index, false);
    }

    [PunRPC]
    public void PlayerFilled(int[] filledTiles,Vector2 topLeftCoord, int index, bool isRpc = true)
    {
        if (isRpc)
        {
            photonView.RPC("PlayerFilled",RpcTarget.Others,filledTiles,topLeftCoord,index, false);
            return;
        }
        var startingCoord = new Vector2Int((int)topLeftCoord.x, (int)topLeftCoord.y);
        Debug.LogWarning("Rpc: " + topLeftCoord + "  " + startingCoord);
        ((OnlineMultiplayerMode)GameModeManager.instance.currentMode).OpponentFills(filledTiles,startingCoord,index);
    }
}
