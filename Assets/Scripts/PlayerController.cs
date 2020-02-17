using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool hasGamepad = false;
    public Rigidbody rBody;
    public float moveSpeed = 2f;
    public float jumpSpeed = 22f;
    public float rotateSpeed = 40f;
    public LevelManager lvl;

    public bool grounded = false;
    public Quaternion defaultRotation;
    public float damp = 0.2f;
    public GameObject waterGun;
    public GameObject Circle;
    public CameraMultiTarget multiCam;
    private item waterGunItem;
    //public item waterGun;

    // Gamepad controls
    //PlayerControls controls;
    Vector2 inputMovement;

    private void Awake()
    {
        waterGunItem = waterGun.GetComponent<item>();
    }

    // Start is called before the first frame update
    void Start()
    {
        multiCam = Camera.main.GetComponent<CameraMultiTarget>();
        
        defaultRotation = this.transform.rotation;
        rBody = GetComponent<Rigidbody>();
        lvl = GameObject.Find("Level").GetComponent<LevelManager>();

        // De-/Activate gamepad controls
        /*if (hasGamepad)
        {
            controls.Gameplay.Enable();
        } else
        {
            controls.Gameplay.Disable();
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        //checked if player flipped over
        if (Vector3.Dot(transform.up, Vector3.down) > 0.5)
        {
            //Debug.Log("flipped over xD");
        }
        if (Vector3.Dot(transform.right, Vector3.right) > 0.7)
        {
            //Debug.Log("flipped over xD");
            //     rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
            //    rBody.isKinematic = true;
            //     transform.rotation = defaultRotation;
            //   transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, Time.deltaTime * damp);
            //  rBody.isKinematic = false;

        }



        if (hasGamepad == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

            // movement here
            if (Input.GetKey(KeyCode.A))
            {
                //move towards the left
                transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
                transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));

            }
            else if (Input.GetKey(KeyCode.D))
            {
                //move towards the right
                transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
                transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                //How do I start from here??
                transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                //walk backwards
                transform.Translate(Vector3.forward * -Time.deltaTime * Input.GetAxis("Vertical") * -1 * moveSpeed);

            }

        }
        else
        {
            //here write the steering for gamepads ;D

            if (inputMovement != Vector2.zero)
            {
                // Movement
                Vector3 movement = new Vector3(inputMovement.x, 0f, inputMovement.y) * Time.deltaTime * moveSpeed;
                transform.rotation = Quaternion.LookRotation(movement);
                transform.Translate(movement, Space.World);
            }
        }

    }

    private void Jump()
    {
        //i jump up!
        rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
        grounded = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4) //if you hit water
        {
            //display on canvas that you lost a life!

            //display a restart timer on canvas... 3,2,1, go!!

            lvl.respawn(this.gameObject);

            //StartCoroutine(WaitAndPrint);

            //after time, the player is respawned somewhere..
        }

        if (other.gameObject.layer == 11) //if you hit terrain
        {
            // this is grounded!
            grounded = true;
        }
    }

    private void OnMove(InputValue input)
    {
        inputMovement = input.Get<Vector2>();
    }

    private void OnJump()
    {
        Jump();
    }

    private void OnShootPressed()
    {
        Debug.Log("Charge shoot");
        waterGunItem.ChargeShoot();
    }

    private void OnShootReleased()
    {
        Debug.Log("Release shoot");
        waterGunItem.FireShoot();
    }
}
