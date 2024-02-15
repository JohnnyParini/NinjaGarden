using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SidePlayerMasterScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    private float horizontal;
    private float vertical;
    
    private Vector3 direction;
    public BoxCollider2D coll;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround; 
    
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
    public int maxHealth;
    int currentHealth;
    public Vector3 atkDownOffset;
    bool canAttack;
    public float attackInterval;

    //game manager stuff
    public LevelDataStorage lvlData;
    public GameObject gameManager;


    //knockback variables
    public float KBCounter;
    public float KBTotalTime; //don't delete this. important in enemy ai script
    public bool KnockFromRight;
    public bool invincible;
    public int whatIsPlayer = 9;
    public int enemies = 10;

    //storage
    public TextMeshProUGUI healthText;
    public GameObject lvl;
    public int curLvl;

    void Start()
    {
        //attached to player already; no conflicts
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //gameManager and associated scripts are consistent throughout; no conflicts
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;



        canAttack = true;
        if (GameObject.FindGameObjectWithTag("HealthSet").GetComponent<HealthResets>().healthReset == true)
        {
            currentHealth = maxHealth;
            PlayerPrefs.SetInt("Health", currentHealth);
        }
        Debug.Log(PlayerPrefs.GetInt("Health")+ "    SAAAAAAAAAAAAAAAAA");
        currentHealth = PlayerPrefs.GetInt("Health");
        
        attackPoint.transform.position = new Vector3(this.transform.position.x + arOffset, this.transform.position.y, 0);
        healthText.text = "Health: " + PlayerPrefs.GetInt("Health").ToString();
        if (healthText.text == null) { healthText.text = maxHealth.ToString(); }

        lvl = GameObject.FindGameObjectWithTag("Level");
        curLvl = lvl.GetComponent<CurrentLevel>().thisLvl;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); //a,d,left,right
        vertical = this.transform.position.y;

        direction = new Vector2(horizontal, vertical); //this looks redundant, might comment it out later

        direction = new Vector2(horizontal, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && IsTouchingWall())
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        

        if (Input.GetButtonDown("Attack") && canAttack)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                Debug.Log("VERTICAL ATTACKING RN");
                anim.SetTrigger("Attack");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position - atkDownOffset, attackRange, enemyLayer);
                enemyTrack(hitEnemies);
                canAttack = false;
            }
            else
            {
                Debug.Log("ATTACKING RN");
                anim.SetTrigger("Attack");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
                enemyTrack(hitEnemies);
                canAttack = false;
            }

            Invoke("AttackReset", attackInterval);
        }

        WallSlide();

        if (direction.x > 0){
            //Debug.Log("flip false");
            spriteR.flipX = false;
            orientation = Vector2.right;
            attackPoint.transform.position = new Vector3(this.transform.position.x + arOffset, this.transform.position.y, 0);
            anim.SetBool("running", true);
        }

        else if (direction.x < 0)
        {
            //Debug.Log("flip true");
            spriteR.flipX = true;
            orientation = Vector2.left;
            attackPoint.transform.position = new Vector3(this.transform.position.x - arOffset, this.transform.position.y, 0);
            anim.SetBool("running", true);
        }

        else
        {
            anim.SetBool("running", false);
        }


    }

    private void enemyTrack(Collider2D[] hitEnemies) 
    {
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
        }
    }

    private void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            this.transform.position += direction * speed * Time.deltaTime;
            Vulnerable();
        }
        else
        {
            Invincible();
            KBCounter -= Time.deltaTime;
        }
        
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private bool IsTouchingWall()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, orientation, 0.1f, wallLayer);
    }

    private void AttackReset()
    {
        canAttack = true; 
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

    public void takeDamage(int damage)
    {
        
        Debug.Log(damage + " this is dmg");
        Debug.Log(currentHealth);
        if (invincible == false)
        {
            currentHealth -= damage;
        }
        
        Debug.Log(currentHealth);
        PlayerPrefs.SetInt("Health", currentHealth);
        healthText.text = "Health: " + PlayerPrefs.GetInt("Health").ToString();

        if (currentHealth <= 0)
        {
            death();
        }
    }

    public void Invincible()
    {
        invincible = true;
        Physics2D.IgnoreLayerCollision(whatIsPlayer, enemies, true);
    }

    public void Vulnerable()
    {
        invincible = false;
        Physics2D.IgnoreLayerCollision(whatIsPlayer, enemies, true);
    }

    void death()
    {
        Debug.Log("ded");

        GetComponent<Collider2D>().enabled = false;
        lvlData.lvls[curLvl] = new(lvlData.lvls[curLvl].Item1, lvlData.lvls[curLvl].Item2, 0);
        SceneManager.LoadScene("Level " + curLvl + " Scene 1");

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a sphere at the transform's position
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
