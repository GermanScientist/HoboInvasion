using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    private PlayerController playerController;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject == playerController.gameObject) return;
        playerController.SetGroundedState(true);
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject == playerController.gameObject) return;
        playerController.SetGroundedState(false);
    }

    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject == playerController.gameObject) return;
        playerController.SetGroundedState(true);
    }
}
