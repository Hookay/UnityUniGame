using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialise : MonoBehaviour
{ //in this script the boyd movment is being used to controll a flock of sheep which will only move when the character
  //gets close enough and they main aim is going to be to stay ahead of the player meanwhile flocking depending on the distance since they are sheep
  //I provided them with strong flocking behaviour. This script is attached to every individual sheep as it gets attached to the prefab.
  //the following tutorial was used and modified to achive this https://www.youtube.com/watch?v=eMpI1eCsIyM

    private Animator anim;
    private float speed = 15.0f;
    private GameObject player;
  

    // Start is called before the first frame update
    void Start()
    {   //find player
        player = GameObject.FindWithTag("Player");
        //get animation 
        anim = GetComponent<Animator>();
        //base state sheep is idle
        anim.Play("idle");
    

    }

    // Update is called once per frame
    void Update()
    {    //record the distance between the sheep and the player
        float approach = Vector3.Distance(transform.position, player.transform.position);
          //if player is nearby the sheep jumps
          if (100.0f < approach && approach < 200.0f)
          {// the sheeps will start hopping every time the player gets close
             anim.Play("jump");
          }
           //only move sheep if the player is close
          if (approach < 100.0f)
          {  //if the flocking is applied all the time the sheeps move too erratic 
            if (Random.Range(0, 5) < 1)
            Flock();
            //give some 
            transform.Translate(0, 0, Time.deltaTime * speed);
          //walking animation initialised
            anim.Play("walk");
        }
    }

    void Flock()
    {
        
        //get the flock array
            GameObject[] sheeps;
            sheeps = moveAhead.sheepHerd;

            //first rule point toward the center of the group if you are close
            Vector3 groupCenter = Vector3.zero;
            //second rule point towards the general group direction (it's in the moveAhead script the flea from the player position)
            Vector3 runHere = moveAhead.hopp;
            float groupSpeed = 10.0f;
            //third rule avoid your neighbours
            Vector3 avoidNeighbour = Vector3.zero;
            //create a 'circle' around this sheep to see if they belong to a group
            float radius;
            //see how many sheep is in the this sheep's group
            int group = 0;
            // store at what distance  this sheep detects neighbour;
            float neighbour = 120.0f;
            //store at what distance  this sheep feels neighbour is too close;
            float tooClose = 10.0f;
            //how fast this sheep turns
            float rotateSpeed = 6.0f;

            //go through the flock
            foreach (GameObject sheep in sheeps)
            {
                //give how does this sheep relate to other ones but not to itself
                if (sheep != this.gameObject)
                {   //get the distance between this sheep and the one we look at
                    radius = Vector3.Distance(sheep.transform.position, this.transform.position);
                    //if this sheep gets too close to neighbour
                    if (radius < tooClose)
                    {   // record the vector which facing in the other direction
                    avoidNeighbour += (this.transform.position - sheep.transform.position);
                    }
                    //if the 'radius' falls within the neighbours distance 
                    if (radius <= neighbour)
                    {   //add up each neighbours position
                        groupCenter += sheep.transform.position;
                        // and count how many is in this group
                        group++;
                    }
                 

                    //add  all the neighbour sheep's speed which is in this sheep's group to the group's speed
                    initialise flock = sheep.GetComponent<initialise>();
                    groupSpeed += flock.speed;
                }


            }
            //if this sheep is in a group
            if (group > 0)
            {   //find the average position from all the neighbours meanwhile heading towards the goal
                groupCenter = groupCenter / group + (runHere - transform.position); 
                //adjust the speed to the flock. This is the average speed 
                speed = groupSpeed / group;
                //add up the vectors to find the direction and take away this sheeps current position;
                Vector3 turnThisWay = (groupCenter + avoidNeighbour) - transform.position;
                //if there is a turn
                if (turnThisWay != Vector3.zero)
                    //rotate sheep to direction
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnThisWay), rotateSpeed * Time.deltaTime);
            }
    }
}
