using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    private float horizontal;
    private float vertical;
    //private new Vector2 directionCheck;
    private Vector3 direction;
    public BoxCollider2D coll;
    public Animator anim;
    [SerializeField] private LayerMask jumpableGround; 
    //[SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    public SpriteRenderer spriteR;
    private bool isGrounded;
    private bool isOnWall;
    public float wallSlideSpeed;
    private Vector2 orientation;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;
    public float arOffset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        attackPoint.transform.position = new Vector3(this.transform.position.x + arOffset, this.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //float dirX = Input.GetAxisRaw("Horizontal");
        //rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        horizontal = Input.GetAxisRaw("Horizontal"); //a,d,left,right
        vertical = this.transform.position.y;

        direction = new Vector2(horizontal, vertical); //this looks redundant, might comment it out later

        direction = new Vector2(horizontal, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && IsTouchingWall())
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("ATTACKING RN");
            anim.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            Debug.Log(hitEnemies.Length);
           // Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            //for (int i = 0; i < hitEnemies.Length-1; i++)
            // {
            //Debug.Log(hitEnemies[0]);
            //  }

            foreach (Collider2D enemy in hitEnemies)
            {
                
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
            }

            
        }

        
        //isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.7f, 0.09f), 0, jumpableGround);
       // isOnWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.15f, 1.35f), 0, wallLayer);
        WallSlide();





        if (direction.x > 0){
            //Debug.Log("flip false");
            spriteR.flipX = false;
            orientation = Vector2.right;
            attackPoint.transform.position = new Vector3(this.transform.position.x+ arOffset, this.transform.position.y, 0);
            anim.SetBool("running", true);
        }

        else if (direction.x < 0)
        {
            //Debug.Log("flip true");
            spriteR.flipX = true;
            orientation = Vector2.left;
            attackPoint.transform.position = new Vector3(this.transform.position.x- arOffset, this.transform.position.y, 0);
            anim.SetBool("running", true);
        }

        else
        {
            anim.SetBool("running", false);
        }

        if (isGrounded)
        {
            //Debug.Log(isGrounded + " GROUND CHECK");
        }

        if (IsTouchingWall() == true)
        {
           // Debug.Log(isOnWall + " WALL CHECK");
        }
        //Debug.Log(isGrounded + " GROUND CHECK");
        //Debug.Log(isOnWall + " WALL CHECK");

    }


    private void FixedUpdate()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private bool IsTouchingWall()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, orientation, 0.1f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsTouchingWall() && !isGrounded && horizontal != 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            isOnWall = true;
        }
        else
        {
            isOnWall = false;
        }
    }

   /* private void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
   */

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
