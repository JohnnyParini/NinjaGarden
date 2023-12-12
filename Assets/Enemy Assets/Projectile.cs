using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public float speed;
    public int damage;
    public int enemyLayer;
    public int projectileLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Physics2D.IgnoreLayerCollision(projectileLayer, enemyLayer, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        SidePlayerMasterScript player = collision.GetComponent<SidePlayerMasterScript>();
        if (player != null)
        {
            player.takeDamage(damage);
        }
        Destroy(gameObject);
    }
}
