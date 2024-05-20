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
    public bool thrown = false;
    public SidePlayerMasterScript pScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<SidePlayerMasterScript>();
        if (thrown == true)
        {
            velocityX = maxVelocity;
            pRb = player.GetComponent<Rigidbody2D>();
            //Debug.Log(player.GetComponent<SidePlayerMasterScript>().orientation.x + " player direction thing");
            acceleration *= player.GetComponent<SidePlayerMasterScript>().orientation.x * -1;
            velocityX *= player.GetComponent<SidePlayerMasterScript>().orientation.x;
            rb.velocity = new Vector2(velocityX, 0);
            //Debug.Log(rb.velocity + " boomerang speed thing");
            //Debug.Log(acceleration + " boomerang acc thing");
            pDistance = this.transform.position - player.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (thrown == true)
        {
            pDistance = this.transform.position - player.transform.position;

            //0.3 is the error bound because finding a value of exacty 0 for the velocity is unrealistic, so code checks an approximate value
            if (pDistance.y * velocityY > 0 && Mathf.Abs(rb.velocity.x) <= 0.3) //check if the two numbers are the same sign and if x velocity is approximately 0
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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CHECK");
        if (thrown == true)
        {
            if (collision.gameObject.layer == enemyLayer)
            {
                collision.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
            }
            else if (collision.gameObject == player && velocityX * pDistance.x <= 0)
            {
                Destroy(gameObject);
            }

        }
        else if (thrown == false)
        {
            Debug.Log("CHECK 1");
            Debug.Log(collision.gameObject);
            if (collision.gameObject == player)
            {
                Debug.Log("CHECK 2");
                pScript.boomerangs.Add(pScript.boomerang);
                Debug.Log("Collected");
                Destroy(this.gameObject);
            }
        }


    }

    
}
