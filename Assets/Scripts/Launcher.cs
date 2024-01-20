using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks {
    [SerializeField] private InputField inputRoomName;
    [SerializeField] private Text roomNametext;

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        Debug.Log("Joined lobby");
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
}
