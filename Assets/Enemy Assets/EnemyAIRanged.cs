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

    private void Awake()
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
         * third if statement indicates the enemy should move towards the player WITHIN its boundries
         */
        if (!Bounded())
        {
            returnToBounds();
        }

        else if (Mathf.Abs(distance) <= attackRange)
        {
            AttackPlayer();
        }

        else if (Bounded())
        {
            ChasePlayer();
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
        return bounded;
    }

    private void returnToBounds()
    {
        if (ePosition.x < boundryLeft.position.x)
        {
            direction = new Vector2(1, this.transform.position.y);
            this.transform.position += direction * speed * Time.deltaTime;
        }
        else if (ePosition.x > boundryRight.position.x)
        {
            direction = new Vector2(-1, this.transform.position.y);
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        /*
        if (dummy == false)
        {
            Gizmos.DrawWireSphere(detectOrigin.position, attackRange);
        }
        */
        
    }


}
