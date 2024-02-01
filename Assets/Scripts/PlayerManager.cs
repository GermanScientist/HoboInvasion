using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour {
    public PhotonView View { get; private set; }
    public enum Role {
        Hobo,
        RichGuy
    }
    public Role role;

    private void Awake() {
        View = GetComponent<PhotonView>();
        Debug.Log($"View {View.name}");
    }

    private void Start() {
        // Is true when photon view is owned by the local player
        if (View.IsMine) CreatePlayer();
    }

    private void CreatePlayer() {
        // Instantiate player
        if (role == Role.Hobo) PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Hobo"), Vector3.zero, Quaternion.identity);
        else if (role == Role.RichGuy) PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "RichGuy"), Vector3.zero, Quaternion.identity);
    }
}
