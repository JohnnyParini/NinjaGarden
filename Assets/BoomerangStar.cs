using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangStar : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D pRb;
    public int damage;
    public int enemyLayer;
    public int projectileLayer;
    public float acceleration;
    public float baseAcceleration;// acceleration should be positive to the right, negative to the left.
    public float velocityX;
    public float velocityY;
    public float maxVelocity;
    public GameObject player;
    public Vector2 pDistance;
    public Vector2 pDistanceNew;
    public float speedXOld;
    public float speedXNew;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = new Vector2(velocityX * player.GetComponent<SidePlayerMasterScript>().orientation.x, 0);
        pDistance = this.transform.position - player.transform.position;
        pRb = player.GetComponent<Rigidbody2D>();

        if (pDistance.x > 0)
        {
            acceleration *= -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) <= maxVelocity)
        {
            rb.velocity = new Vector2(velocityX += acceleration, velocityY);
        }
        

        pDistance = this.transform.position - player.transform.position;

        if (pDistance.y * velocityY > 0) //check if the two numbers are the same sign
        {
            velocityY *= -1;
        }

        if (pDistance.x * acceleration > 0) //check if the two numbers are the same sign
        {
            acceleration *= -1;
        }

        
        if (rb.velocity.x == 0)
        {
            acceleration *= 2;
        }
        else if (rb.velocity.x >= maxVelocity)
        {
            acceleration = baseAcceleration;
        }
        
    }
}
