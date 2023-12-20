using UnityEngine;
using UnityEngine.AI;

public class BossJumping : MonoBehaviour
{
   // public NavMeshAgent agent;

    public GameObject player;
    public SidePlayerMasterScript playerLogic;

    public LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private LayerMask jumpableGround;

    //public float health;
    //gun related variables begin here
    public int damage;
  

    //bool shooting, readyToShoot;

    //public Camera fpsCam;
  
    public LayerMask whatIsEnemy;

 
    //Movement
    public Vector3 direction;
    public float orientation;
    public float speed;
    public float baseSpeed;
    public bool firstDetect;
    public float jumpForceV;
    public float jumpForceH;


    //attack variables
    //public GameObject projectile;
    public Transform dmgPoint;

    public Rigidbody2D rb;
    public BoxCollider2D coll;

    //public Transform detectOrigin;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float time;
    public GameObject projectile;
    public GameObject activeProjectile;
    Vector3 projectileDisplacement;

    /*
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    */

    public bool KnockFromRight;
    public float KBHForce;
    public float KBVForce;
   // public GameObject splashDMG;

    public int right = 1;
    public int left = -1;
    
    //States
    public float attackRange;
    public bool dummy;
    public Vector3 force;
    public Vector3 splashDmgAOE;

    private void Awake()
    {
        firstDetect = true;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        time = 0;
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        projectileDisplacement = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        float distance = this.transform.position.x - player.transform.position.x;

        time += Time.deltaTime;

        if (distance < 0)
        {
            orientation = 1;//run right

        }else if (distance > 0)
        {
            orientation = -1; //run left
        }

        direction = new Vector2(orientation, this.transform.position.y);

        if (Mathf.Abs(distance) <= attackRange && IsGrounded())
        {
            AttackPlayer();
        }else
        {
            ChasePlayer();
        }

        if (alreadyAttacked && IsGrounded() && firstDetect && rb.velocity.y <= 0)
        {
            splashDamage();
            Debug.Log("wasup");
            firstDetect = false;
        }

        if (alreadyAttacked)
        {
            if (timeBetweenAttacks - time <= 0 && IsGrounded())
            {
                Debug.Log("HERE");
                
                for (int i = 0; i < 3; i++)
                {
                    activeProjectile = Instantiate(projectile, dmgPoint.position + projectileDisplacement, transform.rotation);

                    if (orientation == 1)
                    {
                        activeProjectile.GetComponent<Projectile>().pSpeed = Mathf.Abs(activeProjectile.GetComponent<Projectile>().pSpeed); //projectile moves right
                    }
                    else if (orientation == -1)
                    {
                        activeProjectile.GetComponent<Projectile>().pSpeed = Mathf.Abs(activeProjectile.GetComponent<Projectile>().pSpeed) * -1; //projectile moves left
                    }
                    projectileDisplacement.y++;
                }
                projectileDisplacement.y = 0;
                ResetAttack();
            }
        }

    }

    
    private void ChasePlayer()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }


    private void AttackPlayer()
    {
        if (!alreadyAttacked && IsGrounded())
        {
            rb.velocity = new Vector3(jumpForceH * orientation, jumpForceV, 0); //orientation is negative or positive 1, meaning it affects left or right and thats it
            Debug.Log(rb.velocity + " THIS IS THE VELOCITY");
            time = 0;
            alreadyAttacked = true;
       
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        firstDetect = true;     
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void splashDamage()
    {
        
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(dmgPoint.position, splashDmgAOE, 0, whatIsPlayer);
        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("We hit " + player.name);
            playerLogic.takeDamage(damage);
            playerLogic.KBCounter = playerLogic.KBTotalTime;
            
            if (orientation == 1)
            {
                playerLogic.KnockFromRight = true;
                force = new Vector3(KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
            }
            else if (orientation == -1)
            {
                playerLogic.KnockFromRight = false;
                force = new Vector3(-KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
            }
        }


    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(dmgPoint.position, splashDmgAOE);

    }


}
