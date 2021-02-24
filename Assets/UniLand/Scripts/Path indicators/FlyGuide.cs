using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyGuide : MonoBehaviour
{   // attached to the object helps to find the direction.Used with another script when it collides with this object should light up and move
    public MovingPlatform m;

    // to guide the player in direction if the player interacts with this object the trigger will be activated to enable the object movment script to show the way.
    void OnTriggerEnter(Collider other)
    {
        m.enabled = true;
    }
}
