using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public int Turn { get => currentTurn ; private set{} }
    
    private int currentTurn = -1;
    private PhotonView photonView;

    public bool isMyTurn(int team) => team == currentTurn;
    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        if (GameManager.instance.isOnline)
            photonView = GetComponent<PhotonView>();
    }
    
    public void NextTurn()
    {
        currentTurn++;
        currentTurn %= 2;
        if (!GameManager.instance.isOnline) return;
        //if (!PhotonNetwork.IsMasterClient) return;
        photonView.RPC("SyncTurn",RpcTarget.AllViaServer,currentTurn);
    }

    [PunRPC]
    private void SyncTurn(int team)
    {
        currentTurn = team;
    }
}
