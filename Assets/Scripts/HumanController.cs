using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : InputBehaviour
{

    //Public
    public NetworkManagerScriptableObject networkManagerScriptableObject;
    public SpawnManagerScriptableObject SpawnManagerScriptableObject;
    public Camera cam;
    public GameObject Projectile;
    public GameObject model;
    public ParticleSystem deathAura;
    public Animator animationController;
    public bool nearMcGuffin;
    public bool McGuffinEquipped;
    public bool nearGoal;
    public bool canAttack = true;

    //Private
    Rigidbody rb;
    bool grounded;
    float MoveSpeed = 7.5f;
    float CameraRotateSpeed = 1f;
    float CameraFollowSpeed = 0.3f;
    Vector2 CameraRotation;
    Vector3 velocity = Vector3.zero;
    Vector2 Movement;
    GameObject McGuffin;
    float lookX = 0f;
    float lookY = 0f;

    [SerializeField]
    private Transform projectileSpawnTransform;
    // float xRotation = 0f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        deathAura.gameObject.SetActive(false);
        // Debug.Log("instantiating camera");
        // cam = GameObject.Instantiate(cam, transform.TransformPoint(Vector3.forward) , transform.rotation);
        // GetComponent<PlayerManager>().playerCamera = cam;
        
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
    

    /// <summary>
    /// Public Functions Start
    /// /// </summary>
    public void pickUpMcGuffin()
    {
        McGuffin.transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        McGuffin.gameObject.transform.SetParent(this.transform);
        McGuffinEquipped = true;
    }

    public void TakeDamage()
    {
        Debug.Log("Took damage");
        animationController.SetTrigger("GetHit");
        deathAura.gameObject.SetActive(true);
        deathAura.Play();
        Transform spawnPoint = SpawnManagerScriptableObject.getSpawnPoint(GetComponent<PlayerNetworkManager>().teamNumber - 1);
        transform.position = spawnPoint.position;
        cam.transform.position = spawnPoint.position;
    }

    public void DropMcGuffin()
    {
        if(McGuffinEquipped)
        {
            McGuffin.transform.SetParent(null);
            McGuffin.transform.position = new Vector3(McGuffin.transform.position.x, McGuffin.transform.position.y - 2f, McGuffin.transform.position.z);

            McGuffinEquipped = false;
            nearMcGuffin = false;
        }
    }
    
    /// <summary>
    /// Public functions end
    /// </summary>
/// 
    protected override void FirePressed()
    {
        base.FirePressed();
        Attack();
    }

    protected override void NorthButtonPressed()
    {
        GetComponent<PlayerNetworkCommands>().Cmd_InteractWithMcGuffin();
    }

    protected override void SouthButtonPressed()
    {
        if(grounded)
        {
            rb.AddForce(new Vector3(0,300f,0));
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

    protected override void StartPressed()
    {
        base.StartPressed();
        networkManagerScriptableObject.leaveGame();
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
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    void Attack()
    {
        if(!McGuffinEquipped && canAttack)
        {
            //TODO: Instantiate bullet and set position and direction, then call network.spawn, and bullet itself moves forward on its own (maybe its personal transform.forward)

            Quaternion projectileSpawnRotation = Quaternion.FromToRotation(Vector3.up, this.transform.position - projectileSpawnTransform.position);
            GetComponent<PlayerNetworkCommands>().Cmd_Attack(projectileSpawnTransform.position, cam.transform.rotation);

            
            // Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            // RaycastHit hit;
            // Vector3 targetPoint;
            // if (Physics.Raycast(ray, out hit))
            // {
            //     targetPoint = hit.point;
            // }
            // else
            // {
            //     targetPoint = ray.GetPoint( 1000 ) ; // May need to change this value according to needs
            // }
            // // Create the bullet and give it a velocity according to the target point computed before
            // GameObject bullet = GameObject.Instantiate(Projectile, transform.TransformPoint(Vector3.forward * 1.1f) , transform.rotation);
            // bullet.GetComponent<Rigidbody>().velocity = ( targetPoint - transform.position ).normalized * 20;
        }
    }

    private IEnumerator CountdownToAttack()
    {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }

    public void commandAttackReceiver()
    {
        canAttack = false;
        StartCoroutine(CountdownToAttack());
    }
}
