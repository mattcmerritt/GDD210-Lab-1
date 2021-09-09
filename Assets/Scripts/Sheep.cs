using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheep : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // if in front of the player, let the player know
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().FoundSheep(gameObject);
        }
        // if in the pen, remove the sheep (prevents others from bouncing out)
        else if (other.tag == "Goal")
        {
            Destroy(gameObject);
            // increment score
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if no longer in front of player, let the player know
        if (other.tag == "Player") 
        {
            other.GetComponent<Player>().LostSheep();
        }
    }
}