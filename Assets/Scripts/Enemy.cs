using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material Green, Yellow, Red;
    public MeshRenderer BodyRenderer;
    public GameObject Body;
    public int Health;
    public Rigidbody EnemyRB;
    public float WalkSpeed;
    public float ExplosionStrength, ExplosionRadius;
    public Score ScoreCounter;

    private void Start()
    {
        ScoreCounter = FindObjectOfType<Score>();
    }

    private void Update()
    {
        // updating color based on health
        if (Health == 1)
        {
            BodyRenderer.material = Red;
        }
        else if (Health == 2)
        {
            BodyRenderer.material = Yellow;
        }
        else if (Health <= 0)
        {
            Destroy(gameObject);
            ScoreCounter.IncrementScore();
        }
        else
        {
            BodyRenderer.material = Green;
        }

        // kill enemies that fall off the sides
        if (transform.position.y < -10 || Mathf.Abs(transform.position.x) > EnemyManager.MaxPosition)
        {
            Destroy(gameObject);
            ScoreCounter.IncrementScore();
        }

        // lose a life for every enemy that makes it into the base
        if (transform.position.z < -25)
        {
            Destroy(gameObject);
            ScoreCounter.DecrementLives();
        }
    }

    private void FixedUpdate()
    {
        // accelerate forward with constant force
        EnemyRB.AddForce(-transform.forward * WalkSpeed, ForceMode.Acceleration); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Bullets just do damage
        if (collision.gameObject.tag == "Bullet")
        {
            Health--;
            Destroy(collision.gameObject);
        }
        // Rockets do extra knockback
        else if (collision.gameObject.tag == "Concussive")
        {
            Health--;
            EnemyRB.AddExplosionForce(ExplosionStrength, collision.gameObject.transform.position, ExplosionRadius);
            Destroy(collision.gameObject);
        }
    }
}
