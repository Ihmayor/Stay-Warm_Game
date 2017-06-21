using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool jump;
    private string currentAnimation = null;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Author: Tjpark
    //http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
    void setAnimation(string stringInput)
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

    // Update is called once per frame
    void Update()
    {

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
            rb2d.AddForce(Vector2.right * 10f);
        }
        else if (horizontal < 0)
        {
            setAnimation("left");
            rb2d.AddForce(Vector2.left * 10f);
        }


        //Check IsOnGround to Trigger Jump
        bool grounded = Physics2D.Linecast(transform.position, this.gameObject.transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonUp("Jump") && grounded)
        {
            //Set Animation Jump. TODO: Jump animation clip.
            jump = true;

        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            //            animator.SetTrigger("Jump");
            float jumpForce = 150f;
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }
}
