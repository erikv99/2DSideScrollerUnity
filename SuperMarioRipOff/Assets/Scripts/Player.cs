using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private float crouchAnimCooldown, jumpAnimCooldown, jumpSidewaysCooldown, walkAnimCooldown;
    private Animator anim;
    private bool isTouchingGround;
    private bool canMove = true;
    private Transform player;

    [SerializeField]
    private Transform groundCheckPoint;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private float walkS, jumpS;

    // Use this for initialization
    void Start()
    {
        // Getting the animator
        anim = GetComponent<Animator>();
        // Getting the player transform
        player = GetComponent<Transform>();

        walkSpeed = walkS;
        jumpSpeed = jumpS;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!canMove)
            return;

        // checking if the player is on the ground or not
        isTouchingGround = IsTouchingLayer(groundCheckPoint, groundCheckRadius, groundLayer);

        // this would be crouched walking normaly but since i do not have sprites for that we will make sure it will walk
        // other wise it would keep switching between the walk and the crouch animation
        // Crouch + Go left
        if (isTouchingGround && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            PlayAnimation(anim, "walkLeft", "WalkLeftAnimation", walkAnimCooldown);
            Walk(player, 'l');

            return;
        }
        // Crouch + Go right
        if (isTouchingGround && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            PlayAnimation(anim, "walkRight", "WalkRightAnimation", walkAnimCooldown);
            Walk(player, 'r');

            return;
        }
        // fly in air left
        if (!isTouchingGround && Input.GetKey(KeyCode.A))
        {
            PlayAnimation(anim, "flyLeft", "FlyLeft", jumpSidewaysCooldown);
            Walk(player, 'l');

            return;
        }
        // fly in air right
        if (!isTouchingGround && Input.GetKey(KeyCode.D))
        {
            PlayAnimation(anim, "flyRight", "FlyRight", jumpSidewaysCooldown);
            Walk(player, 'r');
            return;
        }
        // normal crouch
        if (isTouchingGround && Input.GetKey(KeyCode.LeftShift))
        {
            Crouch();
        }
        // normal walk left
        if (Input.GetKey(KeyCode.A))
        {
            PlayAnimation(anim, "walkLeft", "WalkLeftAnimation", walkAnimCooldown);
            Walk(player, 'l');
        }
        // normal walk right
        if (Input.GetKey(KeyCode.D))
        {
            PlayAnimation(anim, "walkRight", "WalkRightAnimation", walkAnimCooldown);
            Walk(player, 'r');
        }
        // normal jump
        if (isTouchingGround && Input.GetKey(KeyCode.Space))
        {
            Jump(player);
        }
        // an extra safty so the animation doesnt keep playing when it shouldnt
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            Idle();
        }

    }

    private void Crouch()
    {
        PlayAnimation(anim, "crouch", "CrouchAnimation", crouchAnimCooldown);
    }

    private void Idle()
    {
        anim.Play("IdleAnimation", -1, 0f);
    }

    public void Die()
    {
        canMove = false;
        anim.Play("DieAnimation");
        Invoke("CanMoveTrue", 2);
        // invoke 2 seconds (anim time) then start game back to start with the same amount of lives
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            GameManagerS gameManagerScript;

            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerS>();

            gameManagerScript.RemoveLive();

        }
    }

    private void CanMoveTrue()
    {
        canMove = true;
    }
}
