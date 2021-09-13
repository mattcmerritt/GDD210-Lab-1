using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab, ConcussivePrefab;
    [Range(0, 1000)]
    public float TurnSpeed;
    public float MoveSpeed;
    public float MaxPosition;
    public GameObject FirePoint;
    public float RocketTimer, RocketCooldown;
    public bool RocketAvailable;

    private void Update()
    {
        // only allow a rocket every time the cooldown is finished
        RocketTimer -= Time.deltaTime;
        if (RocketTimer < 0)
        {
            RocketAvailable = true;
        }

        // rotate left (counter-clockwise)
        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, -1 * TurnSpeed * Time.deltaTime, Space.Self);
        }
        // rotate right (clockwise)
        else if (!Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, 1 * TurnSpeed * Time.deltaTime, Space.Self);
        }

        // move left/right
        if ((Input.GetAxisRaw("Horizontal") > 0 && transform.position.x < MaxPosition) ||
            (Input.GetAxisRaw("Horizontal") < 0 && transform.position.x > -MaxPosition))
        {
            transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime;
        }

        // fire bullet
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        }
        // fire rocket
        else if (Input.GetMouseButtonDown(1) && RocketAvailable)
        {
            Instantiate(ConcussivePrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            RocketAvailable = false;
            RocketTimer = RocketCooldown;
        }
    }
}
