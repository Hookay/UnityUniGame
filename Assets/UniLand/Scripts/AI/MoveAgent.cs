using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveAgent : MonoBehaviour
{  //based on the link in tutorial https://www.youtube.com/watch?v=nnrOhb5UdRc and unity https://docs.unity3d.com/Manual/nav-AgentPatrol.html
   // This scripts moves the ghost horse in a patroling movment until it gets close
   //to the player then it goes towards it if it reaches the player then all the magic lands the player unlocked so far will be nullified this is the loose conditionof the game
   //I do not like death(level restart) in game so I decided just take away progress


    private NavMeshAgent agent;
    private GameObject player;
    private NPC npc = NPC.walk;
    public Transform[] goal;
    private int whereTo = 0;
    private Animator anim;
    private bool firedOn = false;
    public float tooClose = 60.0f;
    public GameObject magicPlatform;
    public GameObject magicPlatform2;
    public GameObject magicPlatform3;
    public GameObject magicPlatform4;


 
    void Start()
    {
        //find player 
        player = GameObject.FindWithTag("Player");
        //get the navMesh controller
        agent = GetComponent<NavMeshAgent>();
        //get access to animation
        anim = GetComponentInChildren<Animator>();
        //start patrol
        walkNow();
        

    }

    void Update()
    {

        switch (npc)
        {
            case NPC.walk:

                // when approaching the destination pick the next one
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    walkNow();
                //check how far the player is from this ghost 
                float run = Vector3.Distance(transform.position, player.transform.position);
                //if close enough
                if (run < tooClose)
                    //go to attack mode
                    npc = NPC.attack;
                break;

            case NPC.attack:
                //go to move function
                moveToPlayer();

                if (firedOn == true)
                    npc = NPC.walk;

                break;
        }
    }
        private void walkNow()
        {

            // go back if no patrol locations given
            if (goal.Length == 0)
                return;

            // gost's current destination
            agent.destination = goal[whereTo].position;
            
           // pick next point providing continouity 
            whereTo = (whereTo + 1) % goal.Length;

        }

        private void moveToPlayer()
        {   //navmesh will guide the ghost to the player
            agent.destination = player.transform.position;
            //check for distance between them
            float run = Vector3.Distance(transform.position, player.transform.position);
            // if it is less then 30f then switch off all the active magic lands
            if (run < 30.0f)
            {
           
            magicPlatform.SetActive(false);
            magicPlatform2.SetActive(false);
            magicPlatform3.SetActive(false);
            magicPlatform4.SetActive(false);
            }
            //sorry I only made two animation in blender so I will switch those :D
            anim.Play("Move or Jump");

        }

    
    public enum NPC { walk, attack }


    void OnCollisionEnter(Collision col)
    { // if collided with one of the beams which have puff tags coming from the unicorn's horn
        if (col.gameObject.tag == "puff")
        {
            // go to walk mode by enabling this boolean
            firedOn = true;
            //and send the ghost back to position one so its far enough from us to break the pull attraction
            transform.position = goal[0].position;
        }
    }






}
