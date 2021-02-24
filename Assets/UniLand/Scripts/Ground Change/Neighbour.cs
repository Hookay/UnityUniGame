using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "(),nq}")]
public class Neighbour : MonoBehaviour
{ //this script is very similar to the colour script with the difference it has interaction with the previous level to create a certain sequence.


    public GameObject neighbour;
    int caseSwitch = 0;
    public GameObject forest;
    public GameObject neighbourForest;
    public GameObject colide;
    public GameObject dryTree;
    public Rigidbody magicPrefab;
    public Transform magicEnd;
    public bool fire;
    public GameObject horn;


    //this script is for platforms to be able to change their colour and objects on them and modify their neighbour state
    void OnTriggerEnter(Collider col )
    {
        if (col.gameObject == colide )
        {
           
            caseSwitch++;
        }
        if (caseSwitch > 2)
        {
            caseSwitch = 0;
        }
        switch (caseSwitch)
        {
            case 1:
                GetComponent<Renderer>().material.color = new Color32(255, 246, 148, 255); //change color of platform
                forest.SetActive(false); //control the Game objects on the platform
                dryTree.SetActive(false);//controll the "enemy" objects on the land
                neighbour.GetComponent<Renderer>().material.color = new Color32(188, 91, 255, 255); //change the neighbours colour
                neighbourForest.SetActive(true); //controll the neighbour land's objects
                horn.SetActive(true);//controll the horn beam light
                fire = true;//activate beams
                break;
            case 2:
                GetComponent<Renderer>().material.color = new Color32(188, 91, 255, 255);
                forest.SetActive(true);
                dryTree.SetActive(false);
                neighbour.GetComponent<Renderer>().material.color = new Color32(255, 155, 215, 255);
                neighbourForest.SetActive(false);
                horn.SetActive(true);
                fire = true;
                break;
            default:
                GetComponent<Renderer>().material.color = new Color32(255, 155, 215, 255);
                forest.SetActive(false);
                dryTree.SetActive(true);
                neighbour.GetComponent<Renderer>().material.color = new Color32(255, 246, 148, 255);
                neighbourForest.SetActive(false);
                horn.SetActive(false);
                fire = false;
                break;
        }

    }// make sure if we get back to the ground the platform case does not stays on rendering it useless
    public void GetNeighbourScript()
    {
        forest.SetActive(false);
        horn.SetActive(false);
    }

    //Stay been choosen to be able to replicate the prefabs continously
    void OnTriggerStay(Collider col)
    {
        //if G is pressed down and the magical land is visible enable the Unicorn to shoot ray from his horn
        if (fire == true && Input.GetKey(KeyCode.G))
        {

            Rigidbody magicInstance;
            //the instance will create the prfab at the given gameObject position 
            magicInstance = Instantiate(magicPrefab, magicEnd.position, magicEnd.rotation) as Rigidbody;
            //with direction and force
            magicInstance.AddForce(magicEnd.right * 5000);
        }
    }
    private string DebuggerDisplay => ToString();

}
