using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody BulletRB;
    public float Acceleration;

    private void Update()
    {
        // destroy if it goes too far off screen
        if (Mathf.Abs(transform.position.x) > 50 || Mathf.Abs(transform.position.z) > 50)
        {
            Destroy(gameObject);
        }
    }

    // apply constant force to move bullet forward
    private void FixedUpdate()
    {
        BulletRB.AddForce(transform.forward * Acceleration, ForceMode.Acceleration);
    }
}
