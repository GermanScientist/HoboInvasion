using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks {
    public static RoomManager Instance { get; private set; }

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable() {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable() {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.buildIndex == 1) { // If game scene loaded
            GameObject playerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            //playerManager.GetComponent<PlayerManager>().role = PlayerManager.Role.RichGuy;
            AssignRole(playerManager.GetComponent<PlayerManager>());
        }
    }

    private void AssignRole(PlayerManager playerManager) {
        Player[] players = PhotonNetwork.PlayerList;
        Debug.Log("assignroles");

        // Get random player to be the rich guy
        int randomPlayer = Random.Range(0, players.Length);
        if (players[randomPlayer].NickName == playerManager.View.Controller.NickName) playerManager.role = PlayerManager.Role.RichGuy;
    }
}
