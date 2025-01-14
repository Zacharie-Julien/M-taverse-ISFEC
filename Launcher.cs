using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("The maximum number of players per room. ")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    void Awake()
    {   
        //Permet de vérifier si l'utilisation de LoadLevel() est possible,
        //sur le masteClient et tous les joueurs de la même room

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start() {}

    public void Connect()
    {
        //Si déja connecter au cloud Photon, alors rejoindre une room aléatoire

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }

        //Sinon connection au cloud Photon avec les paramètres

        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;    
        }


    }


    //Fonction de CallBack

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Launcher: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        //Lors d'une impossibilité de rejoindre une room,
        // on en crée une nouvelle avec les paramètres MaxPlayers
        PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {        
        Debug.LogWarningFormat("PUN Launcher: OnDisconnected() was called by PUN with reason {0}");
    }
}
