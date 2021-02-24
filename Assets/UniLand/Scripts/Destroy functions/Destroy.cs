using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{ //This is used to get rid of instansiated objectc when the character moves and the beam beads when the character shoots
    // Start is called before the first frame update
    void Start()
    {   //Destroy the object its attached to in 7 sec
        Destroy(gameObject, 2.1f);
    }

    
}
