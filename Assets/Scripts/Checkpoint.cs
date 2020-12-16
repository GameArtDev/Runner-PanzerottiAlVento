using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    private void Start()
    {
        activated = false;
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (activated)
        {
            anim.SetBool("IsActivated", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activated = true;
            GameEvents.current.CheckpointReached(gameObject.transform);
        }
    }
}
