using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    private bool SheepCollected = false;
    private bool InRange = false;
    private GameObject NearbySheep; // assigned when triggered
    private GameObject HeldSheep; // assigned when picked up

    public float MaxWalkSpeed, WalkForce;
    public Rigidbody PlayerRB;
    public float ThrowingForce;

    private void Update()
    {
        // lateral movment relative to the direction the player is facing
        PlayerRB.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * WalkForce, ForceMode.Acceleration);
        PlayerRB.AddForce(transform.right * Input.GetAxisRaw("Horizontal") * WalkForce, ForceMode.Acceleration);

        // sheep interactions
        if (Input.GetKeyDown("Space"))
        {
            // if not holding a sheep, grab the one nearby
            if (!SheepCollected && InRange)
            {
                HeldSheep = NearbySheep;
                SheepCollected = true;
                HeldSheep.transform.parent = transform;
                Rigidbody SheepRB = HeldSheep.GetComponent<Rigidbody>();
                SheepRB.useGravity = false; // disable gravity so sheep stays in hand
                SheepRB.position = Vector3.zero + Vector3.up; // this should be changed to a position in front of the player
            }
            // if holding a sheep, throw the sheep
            else if (SheepCollected)
            {
                HeldSheep.transform.parent = null;
                Rigidbody SheepRB = HeldSheep.GetComponent<Rigidbody>();
                SheepRB.useGravity = true; // activating gravity before leaving the player's hand
                SheepRB.AddForce((transform.forward + Vector3.up) * ThrowingForce, ForceMode.Impulse);
                HeldSheep = null;
                SheepCollected = false;
            }
        }
    }

    // if the player goes near a sheep, the sheep will activate this function
    public void FoundSheep(GameObject sheep)
    {
        NearbySheep = sheep;
        InRange = true;
    }

    // if the player is not near the sheep anymore, the sheep will call this
    public void LostSheep()
    {
        NearbySheep = null;
        InRange = false;
    }

}