using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{

    public Camera cam;
    public InputMaster controls;
    Rigidbody rb;
    bool grounded;
    float MoveSpeed = 5f;
    float CameraRotateSpeed = 3f;
    float CameraFollowSpeed = 0.3f;
    Vector2 CameraRotation;
    private Vector3 velocity = Vector3.zero;
    bool nearMcGuffin;
    Vector2 Movement;
    GameObject McGuffin;
    bool McGuffinEquipped;

    public GameObject SpawnPoint;

    public GameObject Projectile;

    Animator animationController;
    public GameObject model;
    
    float lookX = 0f;
    float lookY = 0f;
    float xRotation = 0f;
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
        controls.Player.Move.canceled += ctx => MoveStop();
        // controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        controls.Player.Look.performed += ctx => CameraRotation = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => CameraRotation = new Vector2(0,0);

    }

    // Start is called before the first frame update
    void Start()
    {
        McGuffin = GameObject.FindWithTag("mcguffin");
        animationController = model.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Movement.x) > 0.0f || Mathf.Abs(Movement.y) > 0.0f)
        {
            Vector3 m = new Vector3(Movement.x, 0, Movement.y) * MoveSpeed * Time.deltaTime;
            transform.Translate(m, Space.Self);
            animationController.SetBool("Walk", true);
        }
 
        cam.transform.RotateAround(transform.position, Vector3.up, CameraRotation.x * CameraRotateSpeed);
        
        // var x = lookX * 10 * Time.deltaTime;
        // var y = lookY * 10 * Time.deltaTime;

        // xRotation -= lookY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // GhostEyes.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // transform.Rotate(Vector3.up * lookX);
        
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);

        Vector3 cameraTargetPosition = transform.TransformPoint(new Vector3(1f, 1.25f, -2.7f));

        // Smoothly move the camera towards that target position
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraTargetPosition, ref velocity, 0.2f);    
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "ground")
        {
            grounded = true;
        }

        if(other.gameObject.tag == "trap")
        {
            TakeDamage();
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
        Attack();
    }

    public void EastButtonPressed()
    {
        Debug.Log("Back/East button pressed");
        TakeDamage();
    }

    void NorthButtonPressed()
    {
        Debug.Log("North button pressed");

        if(nearMcGuffin)
        {
            McGuffin.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            McGuffin.gameObject.transform.SetParent(this.transform);
            McGuffinEquipped = true;
        }
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

    }

    void MoveStop()
    {
        Movement = new Vector2(0,0);
        animationController.SetBool("Walk", false);
    }

    void Look(Vector2 looking)
    {
        lookX = looking.x;
        lookY = looking.y;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "mcguffin")
        {
            nearMcGuffin = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "mcguffin")
        {
            nearMcGuffin = false;
        }

    }

    void TakeDamage()
    {
        Debug.Log("Took damage");
        animationController.SetTrigger("GetHit");

        if(McGuffinEquipped)
        {
            McGuffin.transform.SetParent(null);
            McGuffin.transform.position = new Vector3(McGuffin.transform.position.x, McGuffin.transform.position.y - 2f, McGuffin.transform.position.z);

            McGuffinEquipped = false;
        }
        transform.position = SpawnPoint.transform.position;
        cam.transform.position = SpawnPoint.transform.position;
    }

    void Attack()
    {
        if(!McGuffinEquipped)
        {
            animationController.SetTrigger("Spell1");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit ;
            Vector3 targetPoint ;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint( 1000 ) ; // You may need to change this value according to your needs
            }
            // Create the bullet and give it a velocity according to the target point computed before
            GameObject bullet = GameObject.Instantiate(Projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = ( targetPoint - transform.position ).normalized * 10;
        }
    }

}
