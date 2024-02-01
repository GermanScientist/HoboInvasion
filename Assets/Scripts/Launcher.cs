using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks {
    public static Launcher Instance { get; private set; }
    [SerializeField] private InputField inputRoomName;
    [SerializeField] private Text roomNametext;

    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;

    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerListItemPrefab;

    [SerializeField] private GameObject startGameButton;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby() {
        Debug.Log("Joined lobby");
        MenuManager.Instance.OpenMenu("Title");
        PhotonNetwork.NickName = $"Player {Random.Range(0, 1000).ToString("0000")}";
    }

    public void CreateRoom() {
        if (string.IsNullOrEmpty(inputRoomName.text)) return;
        PhotonNetwork.CreateRoom(inputRoomName.text);
        MenuManager.Instance.OpenMenu("Loading");
    }

    // Succesfully created room
    public override void OnJoinedRoom() {
        Debug.Log("Succesfully created room");
        MenuManager.Instance.OpenMenu("Room");
        roomNametext.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        // Destroy old player prefabs
        foreach (Transform child in playerListContent)
            Destroy(child.gameObject);

        // Create new prefabs for current players
        for (int i = 0; i < players.Length; i++)
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient) {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    // Failed to create room
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log(message);
    }

    public void LeaveRoom() {
        Debug.Log($"Leaving {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnLeftRoom() {
        MenuManager.Instance.OpenMenu("Title");
    }

    public void JoinRoom(RoomInfo info) {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        // Clear list before update
        foreach (Transform trans in roomListContent) Destroy(trans.gameObject);
        // Instanciate list prefab and call the setup function
        for (int i = 0; i < roomList.Count; i++) {
            if (roomList[i].RemovedFromList) continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame() {
        MenuManager.Instance.OpenMenu("Loading");
        PhotonNetwork.LoadLevel(1);
    }
}
