using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    // Start is called before the first frame update

    //public Rigidbody2D rb;
    
    public int damage;
    public int enemyLayer;
    public int projectileLayer;
    public float persistence;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
       // rb.velocity = transform.right * speed;
        Physics2D.IgnoreLayerCollision(projectileLayer, enemyLayer, true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        
        SidePlayerMasterScript player = collision.GetComponent<SidePlayerMasterScript>();
        if (player != null)
        {
          //  Debug.Log("DAMGE TAKEN DDJKDHSHDSHJDHJDHJDJDJDDJHAJJA");
            player.takeDamage(damage);
        }
        else
        {
          //  Debug.Log("YEAAAAAAAAAAAAH");
        }
        
    }

    private void Update()
    {
        Invoke(nameof(expire), persistence);
    }

    public void expire()
    {
        Destroy(gameObject);
        //Debug.Log("BOX COLLIDER GONE");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, new Vector2(GetComponent<BoxCollider2D>().bounds.size.x, GetComponent<BoxCollider2D>().bounds.size.y));


    }
}
