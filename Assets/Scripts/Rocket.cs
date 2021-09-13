using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody RocketRB;
    [Range(0, 10000)]
    public float FiringForce;

    private void Start()
    {
        RocketRB.AddForce(transform.forward * FiringForce, ForceMode.Impulse);
    }

    private void Update()
    {
        // destroy if it goes too far off screen
        if (Mathf.Abs(transform.position.x) > 50 || Mathf.Abs(transform.position.z) > 50)
        {
            Destroy(gameObject);
        }
    }
}
