using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    [SerializeField] private string menuName;
    public string MenuName { get { return menuName; } }

    [SerializeField] private bool open;

    public void OpenCloseMenu() {
        gameObject.SetActive(!open);
        open = !open;
    }
}
