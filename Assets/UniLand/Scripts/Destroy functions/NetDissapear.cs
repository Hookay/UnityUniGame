using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetDissapear : MonoBehaviour
{ //this script is used when the player shoots at the path blocking net 
   
    //when colliding with a GameObject with the tag puff this object gets distroyed 
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "puff")
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
