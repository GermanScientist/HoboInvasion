using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour {
    [SerializeField] private Text text;
    public RoomInfo Info { get; private set; }

    public void SetUp(RoomInfo _info) {
        Info = _info;
        text.text = Info.Name;
    }

    public void OnClick() {
        Launcher.Instance.JoinRoom(Info);
    }
}
