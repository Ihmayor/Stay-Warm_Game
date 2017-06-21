using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool jump;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    //Author: Tjpark
    //http://answers.unity3d.com/questions/801875/mecanim-trigger-getting-stuck-in-true-state.html
    private string currentAnimation = null;
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
            animator.SetTrigger("forward");
            Debug.Log("test2");
            animator.ResetTrigger("left");
            animator.ResetTrigger("up");
            animator.ResetTrigger("right");
            rb2d.AddForce(Vector2.up);
        }
        else if (vertical < 0)
        {
            animator.SetTrigger("forward");
            Debug.Log("test2");
            animator.ResetTrigger("left");
            animator.ResetTrigger("up");
            animator.ResetTrigger("right");
            rb2d.AddForce(Vector2.down * 10f);
        }
        else if (horizontal > 0)
        {
            animator.SetTrigger("right");
            Debug.Log("test3");
            animator.ResetTrigger("left");
            animator.ResetTrigger("forward");
            animator.ResetTrigger("up");
            rb2d.AddForce(Vector2.right * 10f);
        }
        else if (horizontal < 0)
        {
            animator.SetTrigger("left");
            Debug.Log("test4");
            animator.ResetTrigger("right");
            animator.ResetTrigger("forward");
            animator.ResetTrigger("up");
            rb2d.AddForce(Vector2.left * 10f);
        }
        bool grounded = Physics2D.Linecast(transform.position, this.gameObject.transform.Find("GroundCheck").position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonUp("Jump") && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("test???");
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
