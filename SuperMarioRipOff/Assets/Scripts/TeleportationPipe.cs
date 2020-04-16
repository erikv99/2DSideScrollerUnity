using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationPipe : MonoBehaviour {


    [SerializeField]
    private string sceneToLoadBeforeTp = "null";

    [SerializeField]
    private Vector2 locationToTeleportTo;

    private GameObject player;
    private Animator anim;
    private bool isStandingOnTpTrigger;

    // This is a bool that makes sure the player doesnt get teleported 400 times in a second
    private bool canTeleport = true;



    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        anim = player.GetComponent<Animator>();
    }

    void Update()
    {
        // If the player didnt teleport before and is standing on the trigger
        if (Input.GetKey(KeyCode.LeftShift) && canTeleport && isStandingOnTpTrigger)
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isStandingOnTpTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isStandingOnTpTrigger = false;
        }
    }

    private void TeleportPlayer()
    {
        canTeleport = false;

        // Disabling the player his boxCollider so he or she falls thru
        player.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Teleport", 1);
    }

    private void Teleport()
    {
        // Checking if there is a scene we need to load before tp'ing
        if (sceneToLoadBeforeTp != "null")
        {
            SceneManager.LoadScene(sceneToLoadBeforeTp);
        }

        // Enabling the player his collider again so he doesnt fall thru the floor when teleported
        player.GetComponent<BoxCollider2D>().enabled = true;

        // Teleporting the player
        player.transform.position = locationToTeleportTo;
    }
}
