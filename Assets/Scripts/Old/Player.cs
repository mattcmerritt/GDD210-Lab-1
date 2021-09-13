using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public bool SheepCollected = false;
    public bool InRange = false;
    public GameObject NearbySheep; // assigned when triggered
    public GameObject HeldSheep; // assigned when picked up
    public Vector3 SheepPosition; // location where sheep will be held

    public float MaxWalkSpeed, WalkForce;
    public Rigidbody PlayerRB;
    [Range(0, 100)]
    public float ThrowingForce;
    [Range(0, 1000)]
    public float Sensitivity;

    private void Update()
    {
        // lateral movment relative to the direction the player is facing
        // PlayerRB.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * WalkForce, ForceMode.Acceleration);
        // PlayerRB.AddForce(transform.right * Input.GetAxisRaw("Horizontal") * WalkForce, ForceMode.Acceleration);
        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * WalkForce * Time.deltaTime;
        transform.position += transform.right * Input.GetAxisRaw("Horizontal") * WalkForce * Time.deltaTime;

        // rotation along y-axis
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * Sensitivity * Time.deltaTime, Space.Self);

        // sheep interactions
        if (Input.GetKeyDown("space"))
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
                // SheepRB.freezeRotation = true;
                SheepPosition = HeldSheep.transform.localPosition;
            }
            // if holding a sheep, throw the sheep
            else if (SheepCollected)
            {
                HeldSheep.transform.parent = null;
                Rigidbody SheepRB = HeldSheep.GetComponent<Rigidbody>();
                SheepRB.useGravity = true; // activating gravity before leaving the player's hand
                // SheepRB.freezeRotation = false;
                SheepRB.AddForce((transform.forward + Vector3.up) * ThrowingForce, ForceMode.Impulse);
                HeldSheep = null;
                SheepCollected = false;
            }
        }

        if (HeldSheep)
        {
            HeldSheep.transform.localPosition = SheepPosition; // keeping the sheep in the player's hands
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