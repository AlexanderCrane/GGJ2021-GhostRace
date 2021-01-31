using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{

    public Camera cam;
    public InputMaster controls;
    Rigidbody rb;
    bool grounded;
    float MoveSpeed = 30f;
    float CameraRotateSpeed = 3f;
    float CameraFollowSpeed = 0.3f;
    Vector2 CameraRotation;
    private Vector3 velocity = Vector3.zero;

    Vector2 Movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new InputMaster();
        controls.Player.Move.Enable();
        controls.Player.Look.Enable();
        controls.Player.WestButton.Enable();
        controls.Player.EastButton.Enable();
        controls.Player.NorthButton.Enable();
        controls.Player.SouthButton.Enable();
        controls.Player.LeftBumper.Enable();
        controls.Player.RightBumper.Enable();
        controls.Player.LeftTrigger.Enable();
        controls.Player.RightTrigger.Enable();
        controls.Player.Confirm.Enable();
        controls.Player.Start.Enable();
        controls.Player.Back.Enable();
        controls.Player.LeftStick.Enable();
        controls.Player.RightStick.Enable();
        controls.Player.WestButton.performed += _ => WestButtonPressed();
        controls.Player.EastButton.performed += _ => EastButtonPressed();
        controls.Player.NorthButton.performed += _ => NorthButtonPressed();
        controls.Player.SouthButton.performed += _ => SouthButtonPressed();
        controls.Player.LeftBumper.performed += _ => LeftBumperPressed();
        controls.Player.RightBumper.performed += _ => RightBumperPressed();
        controls.Player.LeftTrigger.performed += _ => LeftTriggerPressed();
        controls.Player.RightTrigger.performed += _ => RightTriggerPressed();
        controls.Player.Confirm.performed += _ => SouthButtonPressed();
        controls.Player.Start.performed += _ => StartPressed();
        controls.Player.Back.performed += _ => EastButtonPressed();
        controls.Player.LeftStick.performed += _ => LeftStickPressed();
        controls.Player.RightStick.performed += _ => RightStickPressed();
        controls.Player.Move.performed += ctx => Movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => Movement = new Vector2(0,0);
        // controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        controls.Player.Look.performed += ctx => CameraRotation = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => CameraRotation = new Vector2(0,0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Movement.x) > 0.0f || Mathf.Abs(Movement.y) > 0.0f)
        {
            Vector3 m = new Vector3(Movement.x, 0, Movement.y) * 3 * Time.deltaTime;
            transform.Translate(m, Space.Self);
        }
 
        if(Mathf.Abs(CameraRotation.x) > 0.0f || Mathf.Abs(CameraRotation.y) > 0.0f)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, CameraRotation.x * CameraRotateSpeed);
        }
        
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);


        Vector3 cameraTargetPosition = transform.TransformPoint(new Vector3(0, 2, -4));

        // Smoothly move the camera towards that target position
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraTargetPosition, ref velocity, 0.2f);    
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "ground")
        {
            grounded = false;
        }

    }

    public void WestButtonPressed()
    {
        Debug.Log("West button pressed");
    }

    public void EastButtonPressed()
    {
        Debug.Log("Back/East button pressed");
    }

    void NorthButtonPressed()
    {
        Debug.Log("North button pressed");
    }

    void SouthButtonPressed()
    {
        Debug.Log("Confirm/South button pressed");
        if(grounded)
        {
            rb.AddForce(new Vector3(0,200f,0));
        }
    }

    void LeftBumperPressed()
    {
        Debug.Log("Left Bumper pressed");
    }

    void RightBumperPressed()
    {
        Debug.Log("Right Bumper pressed");
    }

    void LeftTriggerPressed()
    {
        Debug.Log("Left Trigger pressed");
    }

    void RightTriggerPressed()
    {
        Debug.Log("Right Trigger pressed");
    }

    void StartPressed()
    {
        Debug.Log("Start pressed");
    }

    void LeftStickPressed()
    {
        Debug.Log("Left Stick pressed");
    }

    void RightStickPressed()
    {
        Debug.Log("Right Stick pressed");
    }

    void Move(Vector2 movement)
    {
        // if(Mathf.Abs(movement.x) > 0.0f || Mathf.Abs(movement.y) > 0.0f)
        // {
            // Vector3 m = new Vector3(movement.x, 0, movement.y) * 30 * Time.deltaTime;
            // transform.Translate(m, Space.World);
        // }
        // if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x)){

        // }
        
        // if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y)){

        // }
    }

    void Look(Vector2 looking)
    {
        // Debug.Log(looking);

        if(Mathf.Abs(looking.y) > Mathf.Abs(looking.x)){

        }
        
        if(Mathf.Abs(looking.x) > Mathf.Abs(looking.y)){

        }
    }

}
