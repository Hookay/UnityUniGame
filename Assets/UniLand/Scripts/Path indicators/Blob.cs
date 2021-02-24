using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public Rigidbody blobPrefab;
    public Transform blobHill;

    //when this is attached the prefab gets created at the given Transform position and force is being added to it to move.
    void FixedUpdate()
    {
        Rigidbody blobInstance;
        blobInstance = Instantiate(blobPrefab, blobHill.position, blobHill.rotation) as Rigidbody;
        blobInstance.AddForce(-blobHill.forward * 50);
    }
   
}
