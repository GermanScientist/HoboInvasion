using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject camHolder;
    [SerializeField] private float runSpd, walkSpd, jumpForce, smoothTime;
    private float mouseSensitivity = 5;
    private float verticalCamRotarion;
    private Vector3 moveAmount;
    private Vector3 smoothVel;
    private bool grounded;

    private Rigidbody rb;
    private PhotonView pv;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Start() {
        // Destroy camera gameobject to also remove audiolistener
        if (!pv.IsMine) {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    private void Update() {
        if (!pv.IsMine) return;
        CameraControls();
        Jump();
    }

    private void FixedUpdate() {
        if (!pv.IsMine) return;
        MoveControls();
    }

    private void CameraControls() {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalCamRotarion += Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalCamRotarion = Mathf.Clamp(verticalCamRotarion, -90, 90);
        camHolder.transform.localEulerAngles = Vector3.left * verticalCamRotarion;
    }

    private void MoveControls() {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? runSpd : walkSpd), ref smoothVel, smoothTime);
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.deltaTime);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) rb.AddForce(transform.up * jumpForce);
    }

    public void SetGroundedState(bool _grounded) {
        grounded = _grounded;
    }
}
