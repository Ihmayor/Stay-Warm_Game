using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Variables 
    //Variables to handle Speed and Force of player
    public float MaxSpeed = 2f;
    public float moveForce = 14f;
    public float pushForce { get; private set; }
    public float jumpForce { get; private set; }

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool jump;
    private bool pause;
    private string currentAnimation = null;

    #endregion

    #region Character Controls

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //Amount character can push objects
        pushForce = 1.2f;

        //Amount Character can jump up
        jumpForce = 270f;
        pause = false;
    }



    // Update is called once per frame
    void Update()
    {
        //if (MenuManager.MenuManager.Instance.GameOver)
        //    return;
        if (Input.GetKeyDown(KeyCode.M))
        {
            pause = !pause;
            MenuManager.Instance.TogglePauseMenu(pause);
        }

        if (pause)
            return;
 
        if (this.GetComponent<CharacterStatus>().isWarmingWithMatches)
            return;
        if (this.GetComponent<CharacterStatus>().Respawning)
            return;

        if (Input.GetKeyDown(KeyCode.U))
        {
            this.GetComponent<CharacterStatus>().Respawn();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            this.GetComponent<CharacterStatus>().SacrificeHealth();
        }

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        bool setIdle = false;

        if (vertical > 0)
        {
            //setAnimation("Walk");
            //rb2d.AddForce(Vector2.up);
        }
        else if (vertical < 0)
        {
            //setAnimation("Walk");
            //rb2d.AddForce(Vector2.down * 10f);
        }
        else if (horizontal > 0)
        {
            if (!jump)
            {
                if (gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock)
                    setAnimation("Push");
                else
                    setAnimation("Walk");
            }
        }
        else if (horizontal < 0)
        {
            if (!jump)
            {
                if (gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock)
                    setAnimation("Push");
                else
                    setAnimation("Walk");
            }
        }
        else if (horizontal == 0)
        {
            setIdle = true;
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
        bool grounded = false;
        int childrenCount = transform.childCount;
        for (int i = 0;i < childrenCount; i++)
        {
            Transform childCheck = gameObject.transform.GetChild(i);
            if (childCheck.gameObject.name.Contains("GroundCheck"))
            {
                grounded = Physics2D.Linecast(transform.position, childCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                if (grounded)
                    break;
            }
        }

        bool platformed = Physics2D.Linecast(transform.position, this.gameObject.transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Platform"));

        if (Input.GetButtonUp("Jump") && (grounded || platformed))
        {
            setAnimation("Jump");
            //Set Animation Jump. TODO: Jump animation clip.
            jump = true;
        }
        else if ((grounded || platformed) & setIdle)
        {
            setAnimation("Idle");
        }
       
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            setAnimation("Jump");
            rb2d.AddForce(new Vector2(0f,jumpForce));
            jump = false;
        }

    }

    //Author: Tjpark
    //http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
    public void setAnimation(string stringInput)
    {

        Animator anim = transform.GetComponent<Animator>();
        if (currentAnimation == stringInput)
        {
        }
        else
        {
            if (currentAnimation != null)
            {
                anim.ResetTrigger(currentAnimation);
            }
            anim.SetTrigger(stringInput);
            currentAnimation = stringInput;
        }
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
