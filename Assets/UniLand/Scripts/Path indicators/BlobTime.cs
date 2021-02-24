using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobTime : MonoBehaviour
{//this is used to get rid of the Unicorns missiles once they got shot and met or did not met they target
    // Start is called before the first frame update
    void Start()
    {
        //if this script is attached to Game object it will get distroyed after 10 sec (useful for missiles) 
        Destroy(gameObject, 10.0f);
    }



}
