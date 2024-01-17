using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance { get; private set; }
    [SerializeField] private Menu[] menus;

    private void Awake() {
        Instance = this;
    }

    public void OpenMenu(string menuName) {
        Debug.Log($"Open {menuName}");
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].MenuName == menuName) OpenMenu(menus[i]);
    }

    public void OpenMenu(Menu menu) {
        Debug.Log($"Open {menu.MenuName}");
        menu.OpenCloseMenu();
    }
}
