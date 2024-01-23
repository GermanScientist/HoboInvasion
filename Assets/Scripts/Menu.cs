using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    [SerializeField] private string menuName;
    public string MenuName { get { return menuName; } }

    [SerializeField] private bool open;
    public bool Open { get { return open; } }

    private void Start() {
        gameObject.SetActive(open);
    }

    public void OpenCloseMenu() {
        Debug.Log($"{gameObject.name} {open}");
        gameObject.SetActive(!open);
        open = !open;
    }
}
