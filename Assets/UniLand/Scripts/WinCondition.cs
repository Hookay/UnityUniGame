using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{   //this is the win condition it chcks if all the magic lands are active and all the sheep hers are activated too.
    // if so move to the next level or finish the game with a win by elevating above the ground to have an overview of the scene.

    private GameObject player;
    public Vector3 specifiedLocation;
    private float speed = 10;
    private CharacterController cc;
    public GameObject win;
    public GameObject magicGround;
    private bool m1 = false;
    public int scene;
    public GameObject magicPlatform;
    private bool m2 = false;
    public GameObject magicPlatform1;
    private bool m3 = false;
    public GameObject magicPlatform2;
    private bool m4 = false;

    public GameObject baa1;
    private bool m5 = false;
    public GameObject baa2;
    private bool m6 = false;
    public float height;

    
    // Start is called before the first frame update
    void Start()
    {   //find player
        player = GameObject.FindWithTag("Player");
        //get character controller of player to be able to render it as a child and restrict its movments when in transition to next scene
        cc = player.GetComponent<CharacterController>();
      
    }

    // checke for all the conditions if the are met
    void Update()
    {

        if (magicGround.activeSelf)
        {
            m1 = true;
        }
        else
        {
            m1 = false;
        }
        if (magicPlatform.activeSelf)
        {
            m2 = true;
        }
        else
        {
            m2 = false;
        }
        if (magicPlatform1.activeSelf)
        {
            m3 = true;
        }
        else
        {
            m3 = false;
        }
        if (magicPlatform2.activeSelf)
        {
            m4 = true;
        }
        else
        {
            m4 = false;
        }
        if (baa1.activeSelf)
        {
            m5 = true;
        }
        else
        {
            m5 = false;
        }
        if (baa2.activeSelf)
        {
            m6 = true;
        }
        else
        {
            m6 = false;
        }

        //if so then winning condition activated
        if (m1 == true && m2 == true && m3 == true && m4 == true && m5 == true && m6 == true)
        {   //switch off caracter controller to restrict movment
            cc.enabled = false;
            //transform the player into a child of an object which has been set to move upwards
            player.transform.parent = win.transform;
            //move this wisp slowly upwards
            Vector3 lon = specifiedLocation - transform.position;
            //move Unicorn closer
            transform.position += lon.normalized * speed * Time.deltaTime;
            //if we reach a desired height move to the next scene modular so it can be used in many scenes
            if (player.transform.position.y > height) 
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
