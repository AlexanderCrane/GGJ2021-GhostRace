using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : InputBehaviour
{

    //Public
    public Camera cam;
    public GameObject SpawnPoint;
    public GameObject Projectile;
    public GameObject model;

    //Private
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
    Animator animationController;
    float lookX = 0f;
    float lookY = 0f;
    bool canAttack = true;
    // float xRotation = 0f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        cam = GameObject.Instantiate(cam, transform.TransformPoint(Vector3.forward) , transform.rotation);
        GetComponent<PlayerManager>().playerCamera = cam;
        
        rb = GetComponent<Rigidbody>();
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
 
        // transform.RotateAround(transform.position, Vector3.up, CameraRotation.x * CameraRotateSpeed);
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

    protected override void WestButtonPressed()
    {
        Debug.Log("Attacking");
        Attack();
    }

    protected override void EastButtonPressed()
    {
        TakeDamage();
    }

    protected override void NorthButtonPressed()
    {
        Debug.Log("North button pressed");

        if(nearMcGuffin)
        {
            McGuffin.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            McGuffin.gameObject.transform.SetParent(this.transform);
            McGuffinEquipped = true;
        }
    }

    protected override void SouthButtonPressed()
    {
        Debug.Log("Confirm/South button pressed");
        if(grounded)
        {
            rb.AddForce(new Vector3(0,200f,0));
        }
    }

    protected override void Move(Vector2 v2Movement)
    {
        Movement = v2Movement;
        if(v2Movement == Vector2.zero)
        {
            animationController.SetBool("Walk", false);
        }
    }

    protected override void Look(Vector2 looking)
    {
        CameraRotation = looking;
        lookX = looking.x;
        lookY = looking.y;
    }

    void MoveStop()
    {
        Movement = new Vector2(0,0);
        animationController.SetBool("Walk", false);
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

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "mcguffin")
        {
            nearMcGuffin = true;
        } 
        if(other.gameObject.tag == "bullet"){
            TakeDamage();
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
        if(!McGuffinEquipped && canAttack)
        {
            canAttack = false;
            StartCoroutine(CountdownToAttack());
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
                targetPoint = ray.GetPoint( 1000 ) ; // May need to change this value according to needs
            }
            // Create the bullet and give it a velocity according to the target point computed before
            GameObject bullet = GameObject.Instantiate(Projectile, transform.TransformPoint(Vector3.forward * 1.1f) , transform.rotation);
            bullet.GetComponent<DespawnOnHitOrTime>().CountDownToDestroy();
            bullet.GetComponent<Rigidbody>().velocity = ( targetPoint - transform.position ).normalized * 20;
        }
    }

    private IEnumerator CountdownToAttack()
    {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
