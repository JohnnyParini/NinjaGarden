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
    public float enemyInt;


    //Movement
    public Vector3 direction;
    public float orientation;
    public float speed;
    public float baseSpeed;
    public bool firstDetect;
    public float jumpForceV;
    public float jumpForceH;
    public Vector3 vel;
    Vector3 noVel = new Vector3 (0,0,0);
    float yVel;
    public Transform boundryLeft;
    public Transform boundryRight;

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
   // public GameObject splashDMG;

    public int right = 1;
    public int left = -1;
    
    //States
    public float attackRange;
    public bool dummy;
    public Vector3 force;
    public Vector3 splashDmgAOE;
    float distanceNew;
    float distanceOld;
    float disDif;
    public bool groundChecking;
    bool groundExitReturn;
    

    private void Start()
    {
      
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("whatIsEnemy"), LayerMask.NameToLayer("whatIsEnemy"), true);
        firstDetect = true;
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player);
        time = 0;
        playerLogic = player.GetComponent<SidePlayerMasterScript>();
        
        Physics2D.IgnoreLayerCollision(9, 9);
        distanceOld = this.transform.position.y;
        // agent = GetComponent<NavMeshAgent>();

        //readyToShoot = true;
    }

    private void Update()
    {
        if (IsGrounded())
        {
           // Debug.Log("I weep, for I remain chained to this material world");
        }
        distanceNew = this.transform.position.y;
        disDif = distanceNew - distanceOld;
        distanceOld = distanceNew;

        //Debug.Log(disDif +" DISTANCE IS ALL I HAVE");
       // Debug.Log(rb.velocity.y + " AND SPEED IS ALL I NEED");

        float distance = this.transform.position.x - player.transform.position.x;   
        /*
        Debug.Log(rb.velocity.y + " SPEED IS ALL I NEED");
        Debug.Log(this.transform.position.y + " AND DISTANCE IS ALL I HAVE");
        */

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
            //Debug.Log("RUNNING LEFT");
        }

        direction = new Vector3(orientation, this.transform.position.y, 0);

      
        //Debug.Log("WHY ARE YOU DOING THIS SDF; IOVEMMTEWIT EWEIVMMRMVRTVRTIRVTIREIEMVEVEUITUIPEIUEWTIU");
        
        
        if (Mathf.Abs(distance) <= attackRange && IsGrounded() && !alreadyAttacked)
        {
            rb.velocity = noVel;
            Debug.Log("FIRE IN THE HOLE");
            AttackPlayer();
            groundChecking = true; //tells code that it should begin checking to see if the enemy has left the ground after jumping (seen in below if statement)
            
            
        }

        /*
        if (groundChecking)
        {
            if (!IsGrounded())
            {
                groundExitReturn = true; //confirms that the enemy has lifted off the ground after jumping
                                         //prevents splashDamage() from being called before the enemy leaps into the air when attacking
            }
        }
        */



        else //if (!alreadyAttacked)
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
//        Debug.Log(IsGrounded());
        
        if (rb.velocity.y <= 0 && IsGrounded())
        {
            //print(rb.velocity.y + " y");
//            Debug.Log(rb.velocity.y + " y");
            //print(rb.velocity.x + " x");
        }
             
        if (alreadyAttacked && IsGrounded() && firstDetect && rb.velocity.y <= 0)
        {
            // Instantiate(splashDMG, dmgPoint.position, transform.rotation);
            Debug.Log("SPLASH");
            splashDamage();
            //Debug.Log("HAHAHAHAHAHAHAHAHAHAHAHAHAHAHA");
            firstDetect = false; //necessary variable to ensure boxcast does not persist beyond one instance of casting
            //groundChecking = false; //var
            //groundExitReturn = false;
        }



    }

    
    private void ChasePlayer()
    {
       yVel = rb.velocity.y;
       vel = new Vector3 (speed, yVel, 0);
       rb.velocity = new Vector3(speed * orientation, yVel, 0);
        //this.transform.position += direction * speed * Time.deltaTime;
        //rb.velocity.x = speed;
        //rb.velocity = vel * orientation;
        //Debug.Log("LOOOOOOOOOOOOOOOOOL");
    }


    private void AttackPlayer()
    {

        rb.velocity = new Vector3(jumpForceH * orientation, jumpForceV, 0); //orientation is negative or positive 1, meaning it affects left or right and thats it
        //Debug.Log("FORCE IS BEING APPLIED");
        time = 0;
        firstDetect = true;
        alreadyAttacked = true;
            
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;

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
            playerLogic.takeDamage(damage);
            playerLogic.KBCounter = playerLogic.KBTotalTime;
            
            if (orientation == 1)
            {
                playerLogic.KnockFromRight = true;
                force = new Vector3(KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
            }
            else if (orientation == -1)
            {
                playerLogic.KnockFromRight = false;
                force = new Vector3(-KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

      
        Gizmos.DrawWireCube(dmgPoint.position, splashDmgAOE);

    }


}
