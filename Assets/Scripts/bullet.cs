using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float dmg = 5f;
    public float speed = 120f;
    public GameObject splash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        this.transform.Translate(0,0,speed*Time.deltaTime);
	}

    void OnCollisionEnter(Collision other)
    {
                print("i hit something");
        if (other.gameObject.layer == 8) //if you hit an object that has force
        {

            Rigidbody myRigidBody = other.rigidbody;
            if (myRigidBody != null)
            {

                myRigidBody.AddExplosionForce(121, other.transform.position, 13f, 13.0f);
                GameObject GO = Instantiate(splash, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.layer == 9) //if you hit an object that has force
        {

            Rigidbody myRigidBody = other.rigidbody;
            if (myRigidBody != null)
            {

                myRigidBody.AddExplosionForce(121, other.transform.position, 13f, 13.0f);
                GameObject GO = Instantiate(splash, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
