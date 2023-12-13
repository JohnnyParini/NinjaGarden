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
    public Vector3 force;
    public float KBHForce, KBVForce;
    public EnemyAIJump enemyScript;
    public float orientation;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
       // rb.velocity = transform.right * speed;
        Physics2D.IgnoreLayerCollision(projectileLayer, enemyLayer, true);
        enemyScript = this.transform.parent.gameObject.GetComponent<EnemyAIJump>();
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        orientation = enemyScript.orientation;
        
        SidePlayerMasterScript player = collision.GetComponent<SidePlayerMasterScript>();
        if (player != null)
        {
          //  Debug.Log("DAMGE TAKEN DDJKDHSHDSHJDHJDHJDJDJDDJHAJJA");
            player.takeDamage(damage);
            player.KBCounter = player.KBTotalTime;
            force = new Vector3(KBHForce, KBVForce, 0);
            if (orientation == 1)
            {
                player.KnockFromRight = true;

                // playerLogic.rb.gravityScale *= 2;
                // playerLogic.rb.velocity = new Vector3(KBHForce, KBVForce, 0); //u might want to change to forcemode impulse
                player.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
            }
            else if (orientation == -1)
            {
                player.KnockFromRight = false;
                //  playerLogic.rb.gravityScale *= 2;
                // playerLogic.rb.velocity = new Vector3(KBHForce, KBVForce, 0);
                player.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
                //rb.velocity = new Vector3(0, jumpForce, 0);
            }
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
