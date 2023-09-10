//V1: 22/04/2019 
//V2: 24/04/2019
//V3: 01/05/2019
//V4: 13/05/2019
//V5: 15/05/2019

//Dev: Faizan A. Anwar 
//Its Time For a Hero!

//purpose of this script is to set the movement of the 2d sprite (A and D keys)
//as well as the direction of the animation movement
//eg. Change sprite along x axis based on left-right
//to set conditions for death: tagged by "enemy"
//set conditions to play "walk" anim
//inherits from following classes 
using UnityEngine;
using System.Collections;

//class
public class Move : MonoBehaviour {

//variables
    bool right = false;
    bool left = false;

//public access modifier
    public bool ground = false;
    public float speed;
    public float jump;
//above are accessible outside of the class
    public float groundcheck;
    Animator animator;
    SpriteRenderer sprite;
    Rigidbody rig;

	// Use this for initialization
	void Start () {

//variables inherit from these three classes
        animator = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        rig = this.gameObject.GetComponent<Rigidbody>();

	}
//method one, inherits from collision class
    void OnCollisionEnter(Collision col)
    {
//checks users pos relative to enemy, calls death method (anim) if true
        if(!(col.transform.position.y < this.transform.position.y - this.gameObject.GetComponent<BoxCollider>().size.y + 0.1f) && col.gameObject.tag == "enemy")
        {
            Dead();
        }
        
    }

//nherits from collision, parameter = col
    void OnCollisionStay(Collision col)
    {
        if (col.transform.position.x > this.transform.position.x && col.transform.position.y > this.transform.position.y - this.gameObject.GetComponent<BoxCollider>().size.y)
        {
            right = false;
        }

        if (col.transform.position.x < this.transform.position.x && col.transform.position.y >= this.transform.position.y - this.gameObject.GetComponent<BoxCollider>().size.y)
        {
            left = false;
        }
    }

//death anim method
//called on collision, user cannot move now
    void Dead()
    {
        anim.SetBool("dead", true);
        this.gameObject.GetComponent<Move>().enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.D))
        {
            //moving to right
            right = true;
            
        //up means its no longer being pressed, stop moving r
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //moving to left
            left = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }

        if(right)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x + 1, 
                this.transform.position.y, this.transform.position.z), speed * Time.deltaTime);
        }

        if (left)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), speed * Time.deltaTime);
        }
//or operator
        if(right || left)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

//ray is construct, var with data 
//method ray
        Ray ray = new Ray(this.transform.position, Vector3.down * groundCheck);
        Debug.DrawRay(this.transform.position, Vector3.down * groundCheck);

       
//sends ray to ground, judges a hit or not
//two parameters
        if(Physics.Raycast(ray,groundCheck))
        {
        //if there is distance
            ground = true;
            animator.SetBool("jump", false);

        }
        else
        {
            ground = false;
            animator.SetBool("jump", true);
            
        }

        if(Input.GetKeyDown(KeyCode.Space) && ground)
        {
            rig.AddForce(Vector3.up * jump, ForceMode.Force);
            
        }
       

    }
}
