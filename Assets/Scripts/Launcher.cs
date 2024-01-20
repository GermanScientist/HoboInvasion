using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks {
    [SerializeField] private InputField inputRoomName;

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
    }

    // Failed to create room
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log(message);
    }
}
