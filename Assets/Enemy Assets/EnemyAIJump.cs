using UnityEngine;
using UnityEngine.AI;

public class EnemyAIJump : MonoBehaviour
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
    
    /*
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    */

    public bool KnockFromRight;
    public float KBHForce;
    public float KBVForce;
    public GameObject splashDMG;

    //States
    public float attackRange;
    public bool dummy;
    public Vector3 force;

    private void Awake()
    {
        
        firstDetect = true;
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        time = 0;
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        
        //Physics2D.IgnoreLayerCollision(9, 10);
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        float distance = this.transform.position.x - player.transform.position.x;

        time += Time.deltaTime;
        //Debug.Log("Time = " + time);
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
        
        
        if (Mathf.Abs(distance) <= attackRange && IsGrounded())
        {
            AttackPlayer();
            
        }

        else
        {
            ChasePlayer();
        }

        

        if (alreadyAttacked == true)
        {
           // Debug.Log("FUCK");
            if (timeBetweenAttacks - time <= 0 && IsGrounded())
            {
               // Debug.Log("YOOOOOOOOOOOOOO");
                ResetAttack();
            }
        }

        if (alreadyAttacked == true && IsGrounded() && firstDetect)
        {
            Instantiate(splashDMG, dmgPoint.position, transform.rotation);
           // Debug.Log("HAHAHAHAHAHAHAHAHAHAHAHAHAHAHA");
            firstDetect = false;
            // firstDetect = false;
        }



    }

    
    private void ChasePlayer()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        //Debug.Log("LOOOOOOOOOOOOOOOOOL");
    }


    private void AttackPlayer()
    {
        //Debug.Log(IsGrounded());
        if (!alreadyAttacked && IsGrounded())
        {
          //  Debug.Log("LMAO");
            rb.velocity = new Vector3(jumpForceH, jumpForceV, 0);
            time = 0;
            alreadyAttacked = true;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        firstDetect = true;
        //time = 0;
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
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
