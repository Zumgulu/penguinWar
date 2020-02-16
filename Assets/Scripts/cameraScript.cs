using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private GameObject[] penguins;       //later use these to cast a rectangle which gives covers all penguins so you see all of them always!
    public GameObject target;       //this is the target the camera looks at

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.Find("player");
            //for more advanced script find middle point between all players
        }
        else
        {
            transform.LookAt(target.transform);
        }
    }
}
