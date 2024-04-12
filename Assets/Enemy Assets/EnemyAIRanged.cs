using UnityEngine;
using UnityEngine.AI;

public class EnemyAIRanged : MonoBehaviour
{
   // public NavMeshAgent agent;

    public GameObject player;
    public SidePlayerMasterScript playerLogic;

    public LayerMask whatIsGround, whatIsPlayer;
    public float enemyInt;

    //public float health;
    //gun related variables begin here
    public int damage;
  

    //bool shooting, readyToShoot;

    //public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

 
    //Movement
    public Vector3 direction;
    public float orientation;
    public float speed;
    public float baseSpeed;
    public bool firstDetect;
    public Transform boundryLeft;
    public Transform boundryRight;
    public Vector3 ePosition;
    public bool bounded;
    float yVel;
    Vector3 vel;
    public float jumpDetectDist;
    public float jumpForce;

    //attack variables
    public GameObject projectile;
    public Transform shootPoint;
    public GameObject activeProjectile;


    public Rigidbody2D rb;
    public BoxCollider2D coll;
    //public Transform detectOrigin;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    
    /*
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    */

    public bool KnockFromRight;
    public float KBHForce;
    public float KBVForce;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool dummy;
    public Vector3 force;

    private void Start()
    {
       // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("whatIsEnemy"), LayerMask.NameToLayer("whatIsEnemy"), true);
        firstDetect = true;
        baseSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        Physics2D.IgnoreLayerCollision(9, 9);
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        ePosition = this.transform.position;
        float distance = ePosition.x - player.transform.position.x;

        if (distance < 0 && Bounded())
        {
            orientation = 1;//run right
        }

        else if (distance > 0 && Bounded())
        {
            orientation = -1; //run left
        }

        direction = new Vector2(orientation, this.transform.position.y);


        /*
         * the code below is ordered in priotirty of execution
         * first if statement indicates the enemy should always remain in bounds, and cannot do anything else until this condition is met 
         * second if statement indicates the enemy should attack whenever it can, so long as it is bounded
         * third if statement indicates the enemy should move towards the player WITHIN its boundries if the player is not in attacking range
         */


        //NOTE: bounding system needs to be reworked
        //its current state is a) not adequately functional due to border stuttering and b) not comparable to systems in other games
        //in the reworked system, the enemy should chase the player until the player exits their chase range, at which point they return to a given origin coordinate
        //the enemy should remain at said coordinate until the player re-enters their detection range, which will low them to chase the player once again
        //this should be less buggy and will be more fun
        if (!Bounded())
        {
            returnToBounds();
        }

        else if (Mathf.Abs(distance) <= attackRange)
        {
            AttackPlayer();
        }

        else if (Bounded() && ePosition.x != boundryRight.position.x && ePosition.x != boundryLeft.position.x)
        {
            ChasePlayer();
        }

        if (wallAdjacent() && IsGrounded() && Bounded())
        {
            rb.velocity = new Vector3(jumpForce / 2 * orientation, jumpForce, 0);

           // Debug.Log("JUMP");
        }


    }

    
    private void ChasePlayer()
    {
        yVel = rb.velocity.y;
        vel = new Vector3(speed, yVel, 0);
        rb.velocity = new Vector3(speed * orientation, yVel, 0);
        //this.transform.position += direction * speed * Time.deltaTime;
        //Debug.Log("LOOOOOOOOOOOOOOOOOL");
    }


    private void AttackPlayer()
    {
        
        if (!alreadyAttacked)
        {
            activeProjectile = Instantiate(projectile, shootPoint.position, transform.rotation);
            if (orientation == 1)
            {
                activeProjectile.GetComponent<Projectile>().pSpeed = Mathf.Abs(activeProjectile.GetComponent<Projectile>().pSpeed); //projectile moves right
            }
            else if(orientation == -1)
            {
                activeProjectile.GetComponent<Projectile>().pSpeed = Mathf.Abs(activeProjectile.GetComponent<Projectile>().pSpeed) * -1; //projectile moves left
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private bool Bounded()
    {
        if (ePosition.x > boundryRight.position.x || ePosition.x < boundryLeft.position.x)
        {
            bounded = false;
        }
        else if (ePosition.x <= boundryRight.position.x && ePosition.x >= boundryLeft.position.x)
        {
            bounded = true;
        }
        //Debug.Log(bounded);
        return bounded;
    }

    private void returnToBounds()
    {
        if (ePosition.x < boundryLeft.position.x)
        {
            //direction = new Vector2(1, this.transform.position.y);
            //this.transform.position += direction * speed * Time.deltaTime;
            rb.velocity = new Vector3(speed, yVel, 0);
        }
        else if (ePosition.x > boundryRight.position.x)
        {
            //direction = new Vector2(-1, this.transform.position.y);
            //this.transform.position += direction * speed * Time.deltaTime;
            rb.velocity = new Vector3(speed * -1, yVel, 0);
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
    }

    private bool wallAdjacent()
    {
        return Physics2D.BoxCast(coll.bounds.center, new Vector2(coll.bounds.size.x, coll.bounds.size.y - 0.4f), 0f, new Vector2(orientation, 0), jumpDetectDist, whatIsGround);
    }
}
