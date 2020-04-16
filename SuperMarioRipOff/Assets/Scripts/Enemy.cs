using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {


    [SerializeField]
    private float movementSpeed = 2;

    [SerializeField]
    private float walkRadiusFromMid = 1;

    [SerializeField]
    private Transform enemy;
    private float leftX;
    private float rightX;

    private bool isDieing = false;
    private SpriteRenderer spriteRenderer;
    private GameManagerS gameManagerScript;
    private Animator anim;
    private char direction = 'r';

    // Use this for initialization
    void Start()
    {
        enemy = gameObject.transform;

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerS>();

        spriteRenderer = enemy.GetComponent<SpriteRenderer>();

        anim = enemy.GetComponent<Animator>();

        walkSpeed = movementSpeed;

        leftX = transform.position.x - walkRadiusFromMid;
        rightX = transform.position.x + walkRadiusFromMid;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDieing)
        {
            return;
        }

        // Go left
        if (transform.position.x >= rightX)
        {
            direction = 'l';
        }
        // Go right
        if (transform.position.x <= leftX)
        {
            direction = 'r';
        }

        switch (direction)
        {
            case 'l':
                Walk(enemy, 'l');
                spriteRenderer.flipX = false;
                break;
            case 'r':
                Walk(enemy, 'r');
                spriteRenderer.flipX = true;
                break;
        }

    }

    private void Die(float waitTimeBeforeDestroy)
    {
        // play die animation
        anim.Play("GombaDieAnim", -1, 0f);
        // Destroying the game object after the animation is done
        Destroy(enemy.gameObject, waitTimeBeforeDestroy);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // return if the gomba is busy dieing
        if (isDieing)
        {
            return;
        }

        // check if it is the player
        if (coll.gameObject.tag == "Player")
        {
            // this is so it doesnt remove a live from the player
            gameManagerScript.SetCanLoseLife(false, 0);
            gameManagerScript.SetCanLoseLife(true, 0.5f);

            // this is so it doesnt get called multible times
            isDieing = true;

            Debug.Log("on head");
            Die(0.30f);
        }
    }
}
