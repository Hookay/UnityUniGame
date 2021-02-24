using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayer : MonoBehaviour
{ 

    // this script is to make the player stay on the moving platform
    public GameObject check;
    public GameObject player;
    public GameObject platform;
    private CharacterController cc;
    public float floorDistance = 0.4f;
    public LayerMask floorMask;//what object the floorCheck should check for
    public bool isGrounded;
    public Transform floorCheck;

    // Start is called before the first frame update
    void Start()
    {   //access the players character controller
        cc = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   // if movment is done enable the player's character controller so it can move
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {

            cc.enabled = true;
        }
        //if the floorMask is matching, then make the player independent from the platform. Moving platform has different layer then ground and platforms
        isGrounded = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);
        if (isGrounded == true)
        {
            player.transform.SetParent(null);
        }

    }
    // void OnCollisionEnter(Collision col)
    void OnTriggerEnter(Collider col)
    {       //if the trigger on the player get activated which is at the Unicorns leg make the character control
           //inactive and the player will be the child of the moving platform, hence moving with it
            if (col.gameObject == check)
            {
                cc.enabled = false;
                player.transform.parent = platform.transform;
            }
            
        }
    
}