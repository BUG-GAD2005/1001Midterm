using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameMode gameMode = GameMode.SinglePlayer;
    public int team = 0;

    private PhotonView photonView;
    private int[] rotations = new int[] { 0, 90, 180, 270 };

    public bool isOnline { get => gameMode == GameMode.OnlineMultiplayer ; private set{} }

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("Game")) return;

        if (isOnline)
        {
            photonView = GetComponent<PhotonView>();
            team = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        }
        GameModeManager.instance.SpawnModeObject(gameMode);
    }
    
    [PunRPC]
    public void SpawnShape(int _team, int _index, bool online = false)
    {
        ShapeType type = (ShapeType)Random.Range(0, 5);
        int rotation = rotations[Random.Range(0, 4)];
        
        Spawn(_team,_index,type,rotation);
        if(!online) return;
        
        photonView.RPC("Spawn",RpcTarget.Others,_team, _index,type,rotation);
        
    }
    
    [PunRPC]
    public void Spawn(int _team, int _index ,ShapeType shape, int rotation)
    {
        EventManager.instance.SpawnShapeOnHolder(_team, _index, shape, rotation);
    }
    
}
