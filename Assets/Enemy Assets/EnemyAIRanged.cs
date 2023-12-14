using UnityEngine;
using UnityEngine.AI;

public class EnemyAIRanged : MonoBehaviour
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

 
    //Movement
    public Vector3 direction;
    public float orientation;
    public float speed;
    public float baseSpeed;
    public bool firstDetect;

    //attack variables
    public GameObject projectile;
    public Transform shootPoint;
    public GameObject activeProjectile;
    
    

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
        
        firstDetect = true;
        baseSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        //Physics2D.IgnoreLayerCollision(9, 10);
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        float distance = this.transform.position.x - player.transform.position.x;

        Debug.Log(Mathf.Abs(distance));
        //Debug.Log(Mathf.Abs(distance) + "ORIGINAL DISTANCE CALC");


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
        if (Mathf.Abs(distance) <= attackRange)
        {
            AttackPlayer();
        }

        else {
            ChasePlayer();
        }
       
    }

    
    private void ChasePlayer()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        Debug.Log("LOOOOOOOOOOOOOOOOOL");
    }


    private void AttackPlayer()
    {
        
        if (!alreadyAttacked)
        {
            activeProjectile = Instantiate(projectile, shootPoint.position, transform.rotation);
            if (orientation == 1)
            {
                activeProjectile.GetComponent<Projectile>().speed = Mathf.Abs(speed); //projectile moves right
            }
            else if(orientation == -1)
            {
                activeProjectile.GetComponent<Projectile>().speed = Mathf.Abs(speed) * -1; //projectile moves left
            }
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
        /*
        if (dummy == false)
        {
            Gizmos.DrawWireSphere(detectOrigin.position, attackRange);
        }
        */
        
    }


}
