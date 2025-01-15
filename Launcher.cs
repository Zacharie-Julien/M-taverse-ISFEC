using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("The maximum number of players per room. ")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    bool isConnecting;
    void Awake()
    {   
        //Permet de vérifier si l'utilisation de LoadLevel() est possible,
        //sur le masteClient et tous les joueurs de la même room

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start() {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        //Si déja connecter au cloud Photon, alors rejoindre une room aléatoire

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        } else  //Sinon connection au cloud Photon avec les paramètres
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;    
        }


    }


    //Fonction de CallBack

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }

        Debug.Log("PUN Launcher: OnConnectedToMaster() was called by PUN");
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

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");
            PhotonNetwork.LoadLevel("Room for 1");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {        
        isConnecting = false;
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        
        Debug.LogWarningFormat("PUN Launcher: OnDisconnected() was called by PUN with reason {0}");
    }
}
