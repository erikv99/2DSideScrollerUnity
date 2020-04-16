using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    // This class is purly for basic functions that can be applied on living creatures.
    // Functions like crouch and idle will be diffrent per living creature since it is mainly an animation
    // I am using the protected keyword so it can only be used in classes that instanciate from this one
    // I am using delta time to make sure the amount of distance covered will not vary depending on the fps the player has
    // Non of the functions here include a build in groundcheck

    // Variables

    protected float walkSpeed = 2f;
    protected float jumpSpeed = 50;
    private Hashtable animationCooldowns = new Hashtable();

    // Functions

    protected void PlayAnimation(Animator anim, string animKey, string animationName, float animationCooldownTime)
    {
        // checking if the animation is not in the hashlabel
        if (!animationCooldowns.ContainsKey(animKey))
        {
            // adding it to the hashlabel
            animationCooldowns.Add(animKey, Time.time);
        }
        else
        {
            // this is if the hashlabel does contain the animation
            float currentTime = Time.time;
            // we have to cast it to a float here becouse the compiler doesnt know what it will return since we can also store other data types except from float in a hashlabel
            float animationStartTime = (float)animationCooldowns[animKey];
            // checking if the animation is still on cooldown
            if ((currentTime - animationStartTime < animationCooldownTime))
            {
                return;
            }
            else
            {
                animationCooldowns.Remove(animKey);
            }
        }
        anim.Play(animationName, -1, 0f);
    }

    // Does not contain ground check since this can very, i might not need it for some creatures
    protected void Jump(Transform objectToMove)
    {
        float force = jumpSpeed * Time.deltaTime;
        objectToMove.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

    protected void Walk(Transform objectToMove, char direction) // Valid directions are: l,r
    {
        switch (direction)
        {
            case 'r':
                objectToMove.position += transform.right * walkSpeed * Time.deltaTime;
                break;
            case 'l':
                objectToMove.position += transform.right * -walkSpeed * Time.deltaTime;
                break;
        }
    }

    protected bool IsTouchingLayer(Transform layerCheckPoint, float layerCheckRadius, LayerMask layerToCheck)
    {

        return Physics2D.OverlapCircle(layerCheckPoint.position, layerCheckRadius, layerToCheck);
    }

}
