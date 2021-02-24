using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    //to achive the third person view this tutorial was used https://www.youtube.com/watch?v=4HpC--2iowE&t=10s and modified 
    //this code controlls the player via caracter controller and cinemachine which helps to provide a good camera angle always.
    // gravity, movment and jump functions are given here and animation and instantiation of some objects related to movments.

    public CharacterController co;
    public Transform camera;
    public float speed;
    public float smoothTime;
    private float turnSmooth;
    private Vector3 velocity;
    public float gravity = -9.8f;
    public Transform floorCheck;
    public float floorDistance = 0.4f;
    public LayerMask floorMask;//what object the floorCheck should check for
    public bool isGrounded;
    public float JumpHeight;
    private Animator animation;
    public GameObject galopp;
    public Rigidbody dustPrefab;
    public Transform hoofEnd;
    public GameObject target;
    public LayerMask platformMask;
    public bool isPlatform;




    private void Start()
    {   //create dimensional whirlpool around unicorn at intro 
        Invoke("SpawnObject", 0);
        animation = gameObject.GetComponentInChildren<Animator>(); 
    }
   


    void FixedUpdate()
    {
        Gravity();
        Movement();
        Jump(); 
    }


    void SpawnObject()
    {   // tell where to create the dimensional whirlpool at intro
        Instantiate(target, new Vector3(-210f, 380f, -497f), Quaternion.identity);
    }
    void Gravity()
    {
       
        //Apply gravity with increasing speed as time passes
        velocity.y += gravity * Time.deltaTime;
        co.Move(velocity * Time.deltaTime);
        //Create a small sphere with radious floorDistance if colides with anything in the floorMask Gravity will be true
        isGrounded = Physics.CheckSphere(floorCheck.position, floorDistance, floorMask);
        //needs a separate gravity for platform to be able to switch on off child mode when traveling as the character becomes the child of the platform in another script
        isPlatform = Physics.CheckSphere(floorCheck.position, floorDistance, platformMask);
        //force player to the ground (reset gravity)othervise it constantly falls with increasing speed
        if (isGrounded && velocity.y < 0 || isPlatform && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }


    void Movement()
    {   //get the Axes 
        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        //normalise if moving two direction it does not go faster
        Vector3 dir = new Vector3(hor, 0f, vert).normalized;
        

        if (dir.magnitude >= 0.1f)
        {


            //atan find angle between the two axis to get to the point z comes first to get 0 on x axis (normaly it should be x and y) then clockwise motion plus add camera angle to syncronise
            float turnAngle = Mathf.Atan2(dir.z, -dir.x) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //smothen turn
            float smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle, ref turnSmooth, smoothTime);
            //rotate
            transform.rotation = Quaternion.Euler(0f, smooth, 0f);
            //to move not just point in the right direction, I centered the character in the wrong direction in Blender sorry its my first :D so my forward motion is left.
            Vector3 movDir = Quaternion.Euler(0f, turnAngle, 0f) * Vector3.left;
            //move character
            co.Move(movDir.normalized * speed * Time.deltaTime);
         
        }// Sound and animation for movment 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
        //sound
        galopp.SetActive(true);
        //animation
        animation.SetTrigger("hopp");
        

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
        //stop sound
        galopp.SetActive(false);
        }
    }
    
   void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            //triger jump animation
            animation.SetTrigger("hopp");
            //instantiate dust at jump
            Rigidbody dustInstance;
            // instantiate the prefab at the given transform location and add force to move it
            dustInstance = Instantiate(dustPrefab, hoofEnd.position, hoofEnd.rotation) as Rigidbody;
            dustInstance.AddForce(-hoofEnd.forward * 1000);
          
        }
    }
    
}
