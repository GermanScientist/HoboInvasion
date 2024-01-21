using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks {
    private Player player;
    [SerializeField] private Text text;

    public void SetUp(Player _player) {
        player = _player;
        text.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        if (player == otherPlayer) Destroy(gameObject);
    }

    public override void OnLeftRoom() {
        Destroy(gameObject);
    }
}
