using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manny : MonoBehaviour
{
    // T14 Defines Mannys speed horizontally, speed amount is a guess
    public float speed = 5;

    // T14 define all of Mannys (character) components, (then we jump down to Awake() and 
    // write code to check that these defined components are started)
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    // Defines Mannys facing direction (go FixedUpdate to see direction movement)
    public bool facingRight = true;

    // T14 public jump float so we can change it, default 5, is a guess
    public float jumpSpeed = 5f;

    // T14 track weather manny is jumping or, by default, not
    // Will flip depending if on ground
    bool isJumping = false;

    // T14 Used to check if Manny is on the ground
    // You draw a line out away from the sprite a defined
    // distance and check if it collides with something
    // If it hits nothing Raycast() returns null
    // this is the size of the rays length, very small
    private float rayCastLength = 0.005f;

    // T14 Sprite width and height
    private float width;
    private float height;

    // T14 check How long is the jump button held
    private float jumpButtonPressTime;

    // T14 define the maximum the jumpButtonPressTime (spacebar) is pressed for (go to FixedUpdate to setup jump)
    private float maxJumpTime = 0.2f;

    // T14 using a rigidBody 2D so we will use FixedUpdate, so we can start Manny moving
    void FixedUpdate()
    {
        // Get raw horizontal movement -1 Left, or 1 Right, from input
        float horzMove = Input.GetAxisRaw("Horizontal");

        // Need to reference Mannys x y for rigidbody velocity, 
        // (but first we will go above and define the rigidBody 2d component ahead of time)
        Vector2 vect = rb.velocity;

        // Change rigidbody x and keep y as is
        rb.velocity = new Vector2 (horzMove * speed, vect.y);

        // Set the Speed, the Speed we set up in the animator editor, to move between Idle and Run
        // so the right Animation is played, to the horzMove s absolute value
        animator.SetFloat("Speed", Mathf.Abs(horzMove));

        // Makes sure Manny is facing the right direction in movement, call FlipManny() to flip Mannys animation, 
        // facing right when going right, or left when going left
        if (horzMove > 0 && !facingRight)
        {
            FlipManny();
        }
        else if(horzMove < 0 && facingRight)
        {
            FlipManny();
        }

        // Get vertical (jump) movement, if spacebar is being held down or not
        float vertMove = Input.GetAxis("Jump");

        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
            }
        }
        // If button is held pass max time, set vertical move to 0
        // so Manny doesn't jump off the scree
        if (jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0f;
        }
        // If is jumping and we have a valid jump press length,
        // make Manny jump
        if (isJumping && (jumpButtonPressTime < maxJumpTime))
        {
            // get the rigidbody to make Manny jump, by changing the y direction
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        // If we have moved high enough, make Manny fall
        // Set Mannys Rigidbody 2d Gravity Scale to 2
        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
        }
    }

    // T14 call Awake(), Makes sure components have been created when the 
    // game starts
    void Awake()
    {
        // see that these components are created and awake
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   //(now finish the code for our vect rigidbody)

        // Gets Mannys collider, with GetComponent, width and height and
        // then adds more to it. Used to raycast to see
        // if Manny is colliding with anything so we
        // can jump
        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }

    // T14 moving in a direction face in that direction
    void FlipManny()
    {

        // Flip the facing value, by making it opposite of what it currently is
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        // make the scale opposite of what it currently is
        scale.x *= -1;
        // now its the opposite of what it was
        transform.localScale = scale;
    }

    // T14 called to see if spacebar is being held down, jumping, false or true, on ground
    // by checking if the ground is directly below Manny, with the rayCast()
    public bool IsOnGround()
    {

        // Check if contacting the ground straight down from Mannys position, - Mannys height
        bool groundCheck1 = Physics2D.Raycast(new Vector2(
                                transform.position.x,
                                transform.position.y - height),
                                -Vector2.up, rayCastLength);

        // Check if contacting ground to the right
        bool groundCheck2 = Physics2D.Raycast(new Vector2(
            transform.position.x + (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        // Check if contacting ground to the left
        bool groundCheck3 = Physics2D.Raycast(new Vector2(
            transform.position.x - (width - 0.2f),
            transform.position.y - height),
            -Vector2.up, rayCastLength);

        // lets us know if Mnay is on the ground
        if (groundCheck1 || groundCheck2 || groundCheck3)
            return true;

        // Manny is jumping
        return false;
    }

    // T14 If Manny falls off the screen destroy the game object
   
	void OnBecameInvisible(){
		Debug.Log ("Manny Destroyed");
		Destroy (gameObject);
	}


    /*Start is called before the first frame update NOT USED HERE
    void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
