using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{// this script gets attached to the little grass land if the sheep reaches
 //it they activate another heard of sheep and the light goes on for indication;
    
    public GameObject platformSheep;
    private GameObject cln;
    public GameObject light;
  
    void OnTriggerEnter(Collider col)
    {  //check if object with sheep tag walked into the trigger collider
        if (col.gameObject.tag == "sheep")
        {
            //activate light to indicate change
            light.SetActive(true);
            //activate the next set of sheeps
            platformSheep.SetActive(true);
           
        }
    }
}
