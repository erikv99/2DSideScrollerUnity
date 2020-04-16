using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerS : MonoBehaviour {

    public int startingLives = 3;
    public float protectionAfterDeath = 1.5f;

    private bool hasLostLive = false;
    private Text livesText;
    private int currentLives;
    private Hashtable cooldowns = new Hashtable();

    private Player playerMovement;
    private bool canLoseLive = true;

    // Use this for initialization
    void Start()
    {

        currentLives = startingLives;
        livesText = GameObject.Find("Gui Elements").GetComponentInChildren<Text>();
        playerMovement = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        livesText.text = "x " + currentLives;

    }

    public void RemoveLive()
    {
        // If the player cant lose a life we return
        // This boolean is so the player doesnt lose a life if he jump on top of it to kill it 
        if (!canLoseLive)
            return;

        // checking if the hashtable doesnt contain the key
        if (!cooldowns.ContainsKey("removelive"))
        {
            // adding it to the hashtable
            cooldowns.Add("removelive", Time.time);
        }

        // this if the hashtable does contain the cooldown
        float currentTime = Time.time;
        float startTime = (float)cooldowns["removelive"];

        // checking if the player still has protection and if the player has lost a live before
        if (currentTime < startTime + protectionAfterDeath && hasLostLive)
        {
            Debug.Log("still on protection");
            return;
        }
        else
        {
            // This if statement is so it doesnt keep setting it to true while it is already true
            if (!hasLostLive)
            {
                hasLostLive = true;
            }

            cooldowns.Remove("removelive");
            currentLives -= 1;

            // Play the death animation
            playerMovement.Die();

            if (currentLives <= 0)
            {
                Debug.Log("game over");
                currentLives = 0;
            }
        }
    }

    public void SetCanLoseLife(bool value, float waitingTime)
    {
        switch (value)
        {
            case true:
                Invoke("SetCanLoseLifeTrue", waitingTime);
                break;
            case false:
                Invoke("SetCanLoseLifeFalse", waitingTime);
                break;
        }
    }

    private void SetCanLoseLifeTrue()
    {
        canLoseLive = true;
    }

    private void SetCanLoseLifeFalse()
    {
        canLoseLive = false;
    }
}
