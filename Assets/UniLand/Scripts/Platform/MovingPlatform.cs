using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{ //this script controlls the platform movments and the fly-gude one's
    
    public Vector3 start;
    public Vector3 finish;
    private Vector3 specifiedLocation;
    public float speed;
    private float speedTime;
    private int num = 2;
    int caseSwitch = 0;
    
   

    void Start()
    {   //initialise starting position and jamm prevention;
        start = transform.position;
        speedTime = speed * Time.deltaTime;
      
    }


    void Update()
    {
        //specify the two end of the path for the platform
        switch (caseSwitch)
        {
            case 0:
                specifiedLocation = finish;
                break;
            case 1:
                specifiedLocation = start;
                break;
        }

        if (num > -1)
        {   //find the actual location from the finish point
            Vector3 currentLocation = specifiedLocation - transform.position;
            //move platform closer
            transform.position += currentLocation.normalized * speed * Time.deltaTime;

            //if current location's magnitude gets smaller then the SpeedTime the platform overshoots and it will try to correct itself back resulting in a jamm.
            if (currentLocation.magnitude < speedTime)
            {
                num--;
                //go back to start
                caseSwitch = num;
            }

        }
        else
        {   //start again cycle
            num = 2;
        }

    }

    
}


