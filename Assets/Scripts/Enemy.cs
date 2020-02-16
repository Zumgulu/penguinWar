using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxAmmo = 2;		    //the equipped item has maximal 2 shots.. for example.. 
    public int ammo = 2;			//from the equipped weapon you update the amount of ammo you 				
                                    //have left to fire and at start function you get your maxAmmo..

    public int points = 0;			// how many points did this penguin already make?
    public int aiState = 0;			//0 idle, 1 roam, 2 search, 3 follow, 4 shoot and 5 re-fill!
    public float attackRange = 25;	//at this range i shoot at the target
    public float followRange = 50;	//at this range i follow the player closest to me
    public Transform myTarget;		//this is the player i am currently locked on to. If he falls into the water 				
                                    //and this transform is = null, then i change into roam and search 				//mode...

    public float speed = 2;			// maybe some penguins are less sporty than others?
    public float JumpDistance;		// at this distance i jump to another platform

    public LayerMask layers;
    public Transform target;        // this is the target of this enemy
    public Animator myAnims;		//this is the animator to access animation in unity
    public Rigidbody myRigidbody; 	//the rigidbody from this GO
    public item myWeapon;		    //see class item and the weapons derived from it
    public GameObject weapon;		//this is the weapon model
    public Collider coll;			//this is my collider – use to detect collision with waterplane
    public Collider[] hitColliders; // detected player signatures!
    public LevelManager level;
    public bool grounded = false;
    private float oldDist = 0;
    private bool sliding = false;
    public GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("Level").GetComponent<LevelManager>();
        coll = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }


    private void lookForEnemies(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius, layers);
        int i = 0;
       /* while (i < hitColliders.Length)
        {
            float dist = Vector3.Distance(hitColliders[i].transform.position, this.transform.position);
            if (i > 0 && target != null)
            {
                oldDist = Vector3.Distance(target.position, this.transform.position);
                if (oldDist > dist)
                {
                   i++;         // we keep the old target, its closer ;D
                }
            }
            else if (target == null && hitColliders[i].gameObject.layer == 8)
            {
                target = hitColliders[i].transform;
                i++;
            }
        }*/
    }


    // Update is called once per frame
    void Update()
    {
        //control the ai. 0 idle, 1 roam, 2 search, 3 follow, 4 shoot and 5 re-fill!
        if (aiState == 0)   // this is idle
        {
            circle.SetActive(false);
            //play idle anims
            int random = Random.Range(0, 1);
            if (random == 1)
            {
                aiState = 1;
            }
            else
            {
                aiState = 2;
            }
        }

        else if (aiState == 1)
        {
            //roam around
        }

        else if (aiState == 2)
        {
            //here we look for enemies
            Debug.Log("we search for enemies!");
            // lookForEnemies(transform.position, followRange);
            //int i = 0;
            hitColliders = Physics.OverlapSphere(transform.position, followRange, layers);
            int myTargetID = Random.Range(0, hitColliders.Length);
            target = hitColliders[myTargetID].transform;
            if (target != null)
                
            aiState++;
        }

        else if (aiState == 3)
        {
            //follow player around!
            if (target != null && sliding == false)
            {
                transform.LookAt(target);
                float dist = Vector3.Distance(target.position, this.transform.position);

                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                circle.SetActive(true);
            }
            else { aiState=0; sliding = false; }
        }

        else if (aiState == 4)
        {
            //fire watergun - charge, aim, fire!
        }

        else if (aiState == 1)
        {
            //reload the watergun
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4) //if you hit water
        {
            //display on canvas that you lost a life!

            //display a restart timer on canvas... 3,2,1, go!!

            level.respawn(this.gameObject);

            //StartCoroutine(WaitAndPrint);

            //after time, the player is respawned somewhere..
        }

        if (other.gameObject.layer == 11) //if you hit terrain
        {
            // this is grounded!
            grounded = true;
        }

        if (other.gameObject.layer == 9) //if you hit an object that has force
        {
            // this is grounded!
            sliding = true;
            grounded = false;
            this.GetComponent<Rigidbody>().AddExplosionForce(121, other.transform.position, 13f, 13.0f);
            myRigidbody.velocity = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);

        }
    }


}
