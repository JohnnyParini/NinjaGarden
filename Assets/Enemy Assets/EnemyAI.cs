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

    public Transform detectOrigin;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float doOffset;
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
        float distance = this.transform.position.x - player.transform.position.x;

        if (distance < 0 && !charge)
        {
            orientation = 1;//run right
            detectOrigin.transform.position = new Vector3(this.transform.position.x + doOffset, this.transform.position.y, 0);

        }

        else if (distance > 0 && !charge)
        {
            orientation = -1; //run left
            detectOrigin.transform.position = new Vector3(this.transform.position.x - doOffset, this.transform.position.y, 0);
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
                charge = true;
                
            }
        }

        if (charge) ChargePlayer();

        if (dummy == false && charge)
        {
            Collider2D[] playerRange = Physics2D.OverlapCircleAll(detectOrigin.position, attackRange, whatIsPlayer);
            foreach (Collider2D player in playerRange)
            {
                AttackPlayer();
            }
            if (charge == false)
            {
                ChasePlayer();
            }
            
        }
       
    }

    
    private void ChasePlayer()
    {
        this.transform.position += direction * speed * Time.deltaTime;
        Debug.Log("LOOOOOOOOOOOOOOOOOL");
    }

    

    private void ChargePlayer()
    {
        chargePDistance = Mathf.Abs(this.transform.position.x - player.transform.position.x);

        charge = true;
       // Debug.Log(speed);
        //Debug.Log(Mathf.Abs(chargeDisplacement) + "DISTANCE HERE");
        if (Mathf.Abs(chargeDisplacement) <= chargeDistance && speed <= maxSpeed)
        {
            //Debug.Log("NOW ACCELERATING: " + speed);
            speed += acceleration;
        }
        else if (Mathf.Abs(chargeDisplacement) >= chargeDistance && speed > 0)
        {
            //Debug.Log("NOW DECELERATING: " + speed);
            //Debug.Log(speed);
            speed -= deceleration;
            if (speed <= 0)
            {
                //Debug.Log("LMAO");
                speed = 0;
                charge = false;
                Invoke("chargeRecover", 2.0f);
            }
        }
        this.transform.position += chargeDir * speed * Time.deltaTime;
        chargeDisplacement += chargeDir.x * speed * Time.deltaTime;
        firstDetect = false;

    }

    private void chargeRecover()
    {
        speed = baseSpeed;
        firstDetect = true;
        Debug.Log("FUCK");
    }

    private void AttackPlayer()
    {
        
        if (!alreadyAttacked)
        {
            //Attack code here
            //Debug.Log("We hit " + player.name);
            playerLogic.takeDamage(damage);
            playerLogic.KBCounter = playerLogic.KBTotalTime;
            
            if (orientation == 1)
            {
                playerLogic.KnockFromRight = true;

                // playerLogic.rb.gravityScale *= 2;
                // playerLogic.rb.velocity = new Vector3(KBHForce, KBVForce, 0); //u might want to change to forcemode impulse
                force = new Vector3(KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
            }
            else if(orientation == -1)
            {
                playerLogic.KnockFromRight = false;
                //  playerLogic.rb.gravityScale *= 2;
                // playerLogic.rb.velocity = new Vector3(KBHForce, KBVForce, 0);
                force = new Vector3(-KBHForce, KBVForce, 0);
                playerLogic.rb.AddForce(force, ForceMode2D.Impulse);
                Debug.Log("ATTACK ENTER");
                //rb.velocity = new Vector3(0, jumpForce, 0);
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
        if (dummy == false)
        {
            Gizmos.DrawWireSphere(detectOrigin.position, attackRange);
        }
        
    }


}
