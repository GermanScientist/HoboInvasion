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
        for (int i = 0; i < menus.Length; i++) {
            if (menus[i].MenuName == menuName) OpenCloseMenu(menus[i]);
            else if (menus[i].Open) OpenCloseMenu(menus[i]);
        }
    }

    public void OpenCloseMenu(Menu menu) {
        Debug.Log($"Open {menu.MenuName}");
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].Open) menus[i].OpenCloseMenu();

        menu.OpenCloseMenu();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
