using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {


    public int type = 0;    //0 is pulsar, 1 is minigun, 3 is rockets, 4 is homing missiles, 5 is railgun, 6 is howitzer
    public string name;
    public GameObject projectile;
    public float projectileVelocity = 5f;
    public float rpm;       //this is the rate of fire of your weapon!
    public float rof;       //this is the rate of fire of your weapon!
    public bool charging = false;
    public GameObject muzzle;
    public Transform aimTarget;
    public float charge = 0;
    public GameObject Cone;

    // Cone increasing by charge
    public Vector3 coneStartScale = new Vector3(0.037f, 0.037f, 0.037f);

    // Use this for initialization
    public void Start()
    {
        rpm = 1 / rof;
    }

    private void InvokeRepeating(string v1, double v2, double v3)
    {
        throw new NotImplementedException();
    }

    IEnumerator  fire() 
        //counter for ROF
    {
        print("i shoot");
        GameObject GO = Instantiate(projectile, transform.position, transform.rotation);
        GO.transform.LookAt(aimTarget);
        bullet myBullet = GO.GetComponent<bullet>();
        myBullet.speed += charge/2;
        charge = 0;
        yield return new WaitForSeconds(rpm);
    }
	
	// Update is called once per frame
	void Update () 
    {
        projectileVelocity *= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ChargeShoot();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            FireShoot();
        }
        else
        {
            //Cone.SetActive(false);
        }

        if (charging)
        {
            Cone.SetActive(true);

            charge++;
            // Scale cone by charge
            Cone.transform.localScale = coneStartScale * charge * 0.1f;
        } 
	}

    public void ChargeShoot()
    {
        charging = true;
    }

    public void FireShoot()
    {
        StartCoroutine(fire());
        Cone.SetActive(false);
        charging = false;
    }
}
