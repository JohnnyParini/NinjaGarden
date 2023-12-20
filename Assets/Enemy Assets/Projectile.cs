using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public float pSpeed;
    public int damage;
    public int enemyLayer;
    public int projectileLayer;
    public float pLifetime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * pSpeed;
        Physics2D.IgnoreLayerCollision(projectileLayer, enemyLayer, true);
        Invoke("destroyProjectile", pLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        SidePlayerMasterScript player = collision.GetComponent<SidePlayerMasterScript>();
        if (player != null)
        {
            player.takeDamage(damage);
        }
        destroyProjectile();
    }
    private void destroyProjectile()
    {
        Debug.Log("FATALITY!!!!!!!!!!!!");
        Destroy(gameObject);
    }
}
