using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAhead : MonoBehaviour
{
    // this script is being attached to the guide for the sheep flock the "main sheep"
    //itt will move the sheep will follow if the player is in close enough proximity if not they can get
    //separated and the flock starts to break up to smaller groups easyer

    private GameObject player;
    public float tooClose = 200.0f;//6.0f
    public static Vector3 hopp;
    public float height; 
    static int sheepNum = 12;
    public static GameObject[] sheepHerd = new GameObject[sheepNum];// static so the script of the sheep can access this
    public GameObject preFab;
    public int field = 15;

    // Start is called before the first frame update
    void Start()
    {   //get the player
        player = GameObject.FindWithTag("Player");

        for (int i = 0; i < sheepNum; i++)
        {   //give every sheep a random location within the specified field but make sure the y direction matches the main sheeps level
            Vector3 location = new Vector3(Random.Range(-field, field), height, Random.Range(-field, field));
            sheepHerd[i] = (GameObject)Instantiate(preFab, location, Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {    //if the player is at a certain distance 
        float run = Vector3.Distance(transform.position, player.transform.position);
        if (run < tooClose)
        {
            //get the distance between the main sheep and the player
            Vector3 keepAway = new Vector3(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z);
            //add to the sheeps location resulting in keeping the sheep always ahead of the player
            transform.position += keepAway;
            //hopp is static its the main sheeps location it will be accessed by the rest of the initialised sheep script to use it as goal position
            hopp = transform.position;


        }
        
    }
}
