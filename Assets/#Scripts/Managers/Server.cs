using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviourPunCallbacks
{
    private bool isConnecting = false;
    public static Server instance;
    
    private void Awake()
    {
        if (!instance) instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    private void Start()
    {
        if(!GameManager.instance.isOnline) Destroy(this);
        
        ConnectToPhotonServer();
    }

    public void ConnectToPhotonServer()
    {
        isConnecting = true;

        if (PhotonNetwork.IsConnected)
        {
            //Debug.Log("Already connected to a Photon server");
            JoinLobby();
        }
        else
        {
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //Debug.Log("Joined lobby");
        CreateOrJoinRoom();
    }

    private void CreateOrJoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("Joined room");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
