using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   // public NavMeshAgent agent;

    public GameObject player;
    public SidePlayerMasterScript playerLogic;

    public LayerMask whatIsGround, whatIsPlayer;

    //public float health;
    //gun related variables begin here
    public int damage;
  

    //bool shooting, readyToShoot;

    //public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public float enemyInt;


    //Movement
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public Vector3 direction;
    public float orientation;
    public float speed;
    public bool accelerate;
    public float acceleration;
    public float deceleration;
    public float baseSpeed;
    public float chargeDistance;
    public float chargeDisplacement;
    public float maxSpeed;
    public Vector3 chargeDir;
    public bool firstDetect;
    public bool charge;
    public float chargePDistance;
    float yVel;
    Vector3 vel;
    public Rigidbody2D rb;
    public Collider2D coll;
    public float preChargePos;
    public Transform boundryLeft;
    public Transform boundryRight;
    public float jumpDetectDist;
    public float jumpForce;

    public Transform detectOrigin;

    //Attacking
    public float timeBetweenAttacks;
    public float time;
    public float chargeTime;
    bool alreadyAttacked;
    public GameObject projectile;
    public float doOffset;
    public bool recovering;
    /*
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    */

    public bool KnockFromRight;
    public float KBHForce;
    public float KBVForce;

    //States
    public float sightRange, attackRange, attackDistance;
    public bool playerInSightRange, playerInAttackRange;
    public bool dummy;
    public Vector3 force;


    
    

    private void Start()
    {
       // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("whatIsEnemy"), LayerMask.NameToLayer("whatIsEnemy"), true);
        firstDetect = true;
        recovering = false;
        baseSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        Physics2D.IgnoreLayerCollision(9, 9);
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        float distance = this.transform.position.x - player.transform.position.x;

        if (distance < 0 && !charge)
        {
            orientation = 1;//run right
            detectOrigin.transform.position = new Vector3(this.transform.position.x + doOffset, this.transform.position.y, 0); //flip attack pos origin orientation

        }

        else if (distance > 0 && !charge)
        {
            orientation = -1; //run left
            detectOrigin.transform.position = new Vector3(this.transform.position.x - doOffset, this.transform.position.y, 0); //flip attack pos origin orientation
        }

        direction = new Vector2(orientation, this.transform.position.y);

        if (Mathf.Abs(distance) <= attackDistance)
        {
            //Debug.Log("ITS IN HERE");
            if (firstDetect)
            {
                //Debug.Log("Player in charge range");
                chargeDir = new Vector2(orientation, this.transform.position.y);
                chargeDisplacement = 0;
                preChargePos = this.transform.position.x;
                time = 0;
                charge = true;
                
            }
        }

        if (charge && IsGrounded()) ChargePlayer();

        time += Time.deltaTime;

        if (dummy == false && charge)
        {
            Collider2D[] playerRange = Physics2D.OverlapCircleAll(detectOrigin.position, attackRange, whatIsPlayer);
            foreach (Collider2D player in playerRange)
            {
                AttackPlayer();
            }

            
        }
//        Debug.Log(charge + " charge");
        if (charge == false && Mathf.Abs(distance) >= attackDistance)
        {
            ChasePlayer();
        }

        //the following if statement allows the enemy to jump over obstacles
        if (wallAdjacent() && IsGrounded() && !charge && !recovering)
        {
            rb.velocity = new Vector3(jumpForce/2 * orientation, jumpForce, 0);

//            Debug.Log("JUMP");
        }

    }


    private void ChasePlayer()
    {
        yVel = rb.velocity.y;
        vel = new Vector3(baseSpeed, yVel, 0);
        rb.velocity = new Vector3(baseSpeed * orientation, yVel, 0);
    }



    private void ChargePlayer()
    {
        chargePDistance = Mathf.Abs(this.transform.position.x - player.transform.position.x);

        charge = true;

        if (chargeTime - time > 0 && speed <= maxSpeed)
        {
            speed += acceleration;
        }
        else if (chargeTime - time <= 0 && speed > 0)
        {
            speed -= deceleration;
            if (speed <= 0)
            {
                speed = 0;
                charge = false;
                recovering = true;
                Invoke("chargeRecover", 2.0f);
            }
        }
        chargeDisplacement = preChargePos - this.transform.position.x;
        yVel = rb.velocity.y;
        vel = new Vector3(speed, yVel, 0);
        rb.velocity = new Vector3(speed * orientation, yVel, 0);
        firstDetect = false;
    }

    private void chargeRecover()
    {
        speed = baseSpeed;
        firstDetect = true;
        recovering = false;
    }

    private void AttackPlayer()
    {
        
        if (!alreadyAttacked)
        {
            playerLogic.takeDamage(damage);
            playerLogic.KBCounter = playerLogic.KBTotalTime;
            
            if (orientation == 1)
            {
                playerLogic.KnockFromRight = true;
                force = new Vector3(KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
            }

            else if(orientation == -1)
            {
                playerLogic.KnockFromRight = false;
                force = new Vector3(-KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
            }
            
            
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
    }

    private bool wallAdjacent()
    {
        return Physics2D.BoxCast(coll.bounds.center, new Vector2(coll.bounds.size.x, coll.bounds.size.y - 0.4f), 0f, new Vector2(orientation, 0), jumpDetectDist, whatIsGround);
    }



    void OnDrawGizmos()
    {
        if (dummy == false)
        {
            Gizmos.DrawWireCube(coll.bounds.center, new Vector2(coll.bounds.size.x, coll.bounds.size.y - 0.4f));
            Gizmos.DrawRay(this.transform.position, Vector2.right);
        }
        
    }


}
