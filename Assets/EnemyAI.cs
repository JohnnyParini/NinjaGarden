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
    public float timeBetweenShooting, spread, range, timeBetweenShots;
    public int bulletsPerTap;

    //bool shooting, readyToShoot;

    //public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

 
    //Movement
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public Vector3 direction;
    public float orientation;
    public float speed;
    public Transform detectOrigin;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    /*
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    */

    public bool KnockFromRight;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool dummy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        //Physics2D.IgnoreLayerCollision(9, 10);
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        float distance = this.transform.position.x - player.transform.position.x;

            //Vector3.Distance(this.transform.position, player.transform.position);
        if (distance < 0)
        {
            orientation = 1;//run right
        }

        else if (distance > 0)
        {
            orientation = -1; //run left
        }

        direction = new Vector2(orientation, this.transform.position.y);
        //Debug.Log("WHY ARE YOU DOING THIS SDF; IOVEMMTEWIT EWEIVMMRMVRTVRTIRVTIREIEMVEVEUITUIPEIUEWTIU");
        //Check for sight and attack range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (dummy == false)
        {
            Collider2D[] playerRange = Physics2D.OverlapCircleAll(detectOrigin.position, attackRange, whatIsPlayer);

            foreach (Collider2D player in playerRange)
            {
                AttackPlayer();
            }

            ChasePlayer();
        }
        
        //Debug.Log(playerRange.Length);

        

        

       // if (!playerInSightRange && !playerInAttackRange) Patroling();
       

        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    
    /*
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        
    }
    
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, 0);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    */
    private void ChasePlayer()
    {
       
        this.transform.position += direction * speed * Time.deltaTime;
        

    }

    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            //Attack code here
            //Debug.Log("We hit " + player.name);
            playerLogic.KBCounter = playerLogic.KBTotalTime;
            if (orientation == 1)
            {
                playerLogic.KnockFromRight = true;
            }
            else if(orientation == -1)
            {
                playerLogic.KnockFromRight = false;
            }
            playerLogic.takeDamage(damage);
            
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (dummy == false)
        {
            Gizmos.DrawWireSphere(detectOrigin.position, attackRange);
        }
        
    }


}
