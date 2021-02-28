using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GhostController : InputBehaviour {
    public NetworkManagerScriptableObject networkManagerScriptableObject;
    public Camera GhostEyes;
    public float moveSpeed = 1.0f;

    private Rigidbody body;

    // I hear physicist screaming internally at my vasiable naming
    private Vector2 magnitude = Vector2.zero;

    private float up = 0f;
    private float down = 0f; 

    float lookX = 0f;
    float lookY = 0f;

    float xRotation = 0f;
    bool canActivate;
    GameObject nearbyTrap;

    // Start is called before the first frame update
    void Start() {
        this.body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        // not sure if this should remain in update but it seems fine?
        var x = lookX * 10 * Time.deltaTime;
        var y = lookY * 10 * Time.deltaTime;

        // I miss Brackeys already ;-;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        GhostEyes.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookX);
    }

    void FixedUpdate() {
        var y = up - down;

        var moveD = new Vector3(magnitude.x, y, magnitude.y) * moveSpeed;

        // using force because I want floaty movement for the ghost
        body.AddRelativeForce(moveD, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "trap"){
            canActivate = true;
            nearbyTrap = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "trap"){
            nearbyTrap = null;
        }
    }

    protected override void Look(Vector2 looking) {
        lookX = looking.x;
        lookY = looking.y;
    }

    protected override void Move(Vector2 movement) {
        magnitude = movement;
    }

    protected override void WestButtonPressed()
    {
        if(canActivate && nearbyTrap != null)
        {
            GetComponent<PlayerNetworkCommands>().Cmd_ActivateTrap(nearbyTrap);
            // nearbyTrap.GetComponent<TrapActivate>().Activate();
        }
    }

    protected override void RightBumperPressed() => up = 1f;

    protected override void RightBumperReleased() => up = 0f;

    protected override void LeftBumperPressed() => down = 1f;

    protected override void LeftBumperReleased() => down = 0f;

    protected override void StartPressed()
    {
        base.StartPressed();
        networkManagerScriptableObject.leaveGame();
    }

    protected override void BackPressed()
    {
        base.BackPressed();
        networkManagerScriptableObject.loadWinLoseScene();
    }
}
