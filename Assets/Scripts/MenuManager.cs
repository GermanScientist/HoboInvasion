using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance { get; private set; }
    [SerializeField] private Menu[] menus;
    private Menu currentMenu;

    private void Awake() {
        Instance = this;
        currentMenu = menus[0];
    }

    public void OpenMenu(string menuName) {
        Debug.Log($"Open {menuName}");
        // Return if menu is already open
        if (currentMenu.MenuName == menuName) return;
        for (int i = 0; i < menus.Length; i++) {
            if (menus[i].MenuName == menuName) {
                currentMenu = menus[i];
                menus[i].OpenCloseMenu();
            }
            else if (menus[i].Open) menus[i].OpenCloseMenu();
        }
    }

    public void OpenCloseMenu(Menu menu) {
        Debug.Log($"Open close {menu.MenuName}");
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].Open) menus[i].OpenCloseMenu();

        menu.OpenCloseMenu();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
