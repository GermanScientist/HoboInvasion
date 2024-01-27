using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour {
    private PhotonView view;

    private void Awake() {
        view = GetComponent<PhotonView>();
    }

    private void Start() {
        // Is true when photon view is owned by the local player
        if (view.IsMine) CreatePlayer();
    }

    private void CreatePlayer() {
        // Instantiate player
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
    }
}
