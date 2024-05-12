using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangStar : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D pRb;
    public int damage;
    public int enemyLayer = 9;
    public int projectileLayer = 11;
    public int playerLayer = 10;
    public float acceleration;
    public float velocityX;
    public float velocityY;
    public float maxVelocity;
    public GameObject player;
    public Vector2 pDistance;
    // Start is called before the first frame update
    void Start()
    {
        velocityX = maxVelocity;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.velocity = new Vector2(velocityX * player.GetComponent<SidePlayerMasterScript>().orientation.x, 0);
        pDistance = this.transform.position - player.transform.position;
        pRb = player.GetComponent<Rigidbody2D>();
       

        /*
        if (pDistance.x > 0)
        {
            acceleration *= -1;
        }
        */

    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(velocityX) >= maxVelocity)
        {
            Debug.Log("MAX VELOCITY");
        }

        pDistance = this.transform.position - player.transform.position;

        
        if (pDistance.y * velocityY > 0 && Mathf.Abs(rb.velocity.x) <= 0.1) //check if the two numbers are the same sign and if x velocity is approximately 0
        {
            velocityY *= -1;
        }

        if (acceleration * pDistance.x > 0 && Mathf.Abs(velocityX) >= maxVelocity)
        {
            acceleration *= -1;
        }
        
        if (acceleration * velocityX <= 0 || acceleration * velocityX > 0 && Mathf.Abs(velocityX) < maxVelocity)
        {
            velocityX += acceleration;
        }
   

        rb.velocity = new Vector2(velocityX, velocityY);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
        }
        
        
    }
}
