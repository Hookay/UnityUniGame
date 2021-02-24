using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{ //this script hides the main aim of the game this and the neighbour sript hase a logic to controll at what stage which objects are visible.
  //the player controls the change by jumping. When the magic land is active the Unicor has power to shoot from his horn to repell enemyes or destroy barriers. 
  //This script was inspired by watching the begginer scripting unity video series https://api.unity.com/v1/oauth2/authorize?client_id=unity_learn&locale=en_US&redirect_uri=https%3A%2F%2Flearn.unity.com%2Fauth%2Fcallback%3Fredirect_to%3D%252Fproject%252Fbeginner-gameplay-scripting&response_type=code&scope=identity+offline&state=45909c62-8f9e-43f6-a9df-f88a6c5f1449


    int caseSwitch = 0;
    public GameObject forest;
    public Neighbour ns;
    public GameObject dryTree;
    public GameObject colide;
    public Rigidbody magicPrefab;
    public Transform magicEnd;
    private bool fire;
    public GameObject horn;
  

    void Start()
    {
        
        ns.GetNeighbourScript(); 
    }

    // void OnCollisionEnter(Collision col)
    void OnTriggerEnter(Collider col)
    {


       //colide game object created at the bottom of the hoofs to make sure if simply bumping into things terrain do not change
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
                GetComponent<Renderer>().material.color = new Color32(255, 246, 148, 255);//change the land this code is attached to yellow
                forest.SetActive(false);// magic land hidden
                dryTree.SetActive(true); //waistland active
                fire = false; //unicorn magic power inactive
                horn.SetActive(false);//this controls the indicating light game object for the horn that it can fire beams
                break;
            case 2:
                GetComponent<Renderer>().material.color = new Color32(188, 91, 255, 255);//purple
                forest.SetActive(true);
                dryTree.SetActive(false);
                fire = true;//enable the method in the Trigger stay
                horn.SetActive(true);
                break;
            default:
                GetComponent<Renderer>().material.color = new Color32(255, 155, 215, 255);//red
                forest.SetActive(false);
                dryTree.SetActive(true);
                fire = false;
                horn.SetActive(false);
                break;
        }

    }

    //Stay been choosen to be able to replicate the prefabs continously
    void OnTriggerStay(Collider col)
    {    //this combined with the distroy function gets rid of obstackles 
        //if G is pressed down and the magical land is visible enable the Unicorn to shoot ray from his horn
        if (fire == true && Input.GetKey(KeyCode.G)){
         
            Rigidbody magicInstance;
            //the instance will create the prfab at the given gameObject position 
            magicInstance = Instantiate(magicPrefab, magicEnd.position, magicEnd.rotation) as Rigidbody;
            //with direction and force
            magicInstance.AddForce(magicEnd.right * 5000);
        }
    }

  
}
    
