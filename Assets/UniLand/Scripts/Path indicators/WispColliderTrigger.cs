using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispColliderTrigger : MonoBehaviour
{//this makes an object emmit light on collision another script enables to move when it detects collision.
    public GameObject wisp;
    
  
    void OnTriggerEnter(Collider other)
   {   //check the layer number 10 witch is puff for collision this way many objects can be assigned to collide with the wisp
       if (other.gameObject.layer == 10)
       { 
            //enable emmision on the given object with a certain colour
            wisp.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            wisp.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(6, 251, 131, 90));
      
        }

    }
}
