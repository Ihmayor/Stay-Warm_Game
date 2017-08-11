using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Variables
    public float MaxSpeed = 2f;
    public float moveForce = 10f;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool jump;
    private string currentAnimation = null;
    private float jumpForce;

    #endregion

    #region Character Controls

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        jumpForce = 200f;
    }



    // Update is called once per frame
    void Update()
    {
        //if (MenuManager.MenuManager.Instance.GameOver)
        //    return;

        if (this.GetComponent<CharacterStatus>().isWarmingWithMatches)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            // setAnimation("notFire");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            this.GetComponent<CharacterStatus>().SacrificeHealth();
        }

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            setAnimation("up");
            rb2d.AddForce(Vector2.up);
        }
        else if (vertical < 0)
        {
            setAnimation("forward");
            rb2d.AddForce(Vector2.down * 10f);
        }
        else if (horizontal > 0)
        {
            setAnimation("right");
        }
        else if (horizontal < 0)
        {
            setAnimation("left");
        }


        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (horizontal * rb2d.velocity.x < MaxSpeed)
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontal * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rb2d.velocity.x) > MaxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * MaxSpeed, rb2d.velocity.y);



        //Check IsOnGround to Trigger Jump
        bool grounded = Physics2D.Linecast(transform.position, this.gameObject.transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Ground"));
        bool platformed = Physics2D.Linecast(transform.position, this.gameObject.transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Platform"));

        if (Input.GetButtonUp("Jump") && (grounded || platformed))
        {
            //Set Animation Jump. TODO: Jump animation clip.
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            //animator.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }

    //Author: Tjpark
    //http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
    public void setAnimation(string stringInput)
    {
        //    Animator anim = transform.GetComponent<Animator>();
        //    if (currentAnimation == stringInput)
        //    {
        //    }
        //    else
        //    {
        //        if (currentAnimation != null)
        //        {
        //            anim.ResetTrigger(currentAnimation);
        //        }
        //        anim.SetTrigger(stringInput);
        //        currentAnimation = stringInput;
        //    }
    }

    /// <summary>
    /// Code to stay on platforms
    /// </summary>
    /// <param name="other">Platform</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Platform"))
        {
            transform.parent = null;

        }
    }
    #endregion
}
