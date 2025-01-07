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
    public Vector2 orientation;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;
    public float arOffset;
    public int maxHealth;
    public int currentHealth;
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

    //attack
    public GameObject boomerang;
    public List<GameObject> boomerangs = new List<GameObject>();
    public int boomerangCount;

    void Start()
    {
    
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        lvlData = gameManager.GetComponent<GameManager>().levelDataStorage;

        attackPoint.transform.position = new Vector3(this.transform.position.x + arOffset, this.transform.position.y, 0);

        canAttack = true;
        currentHealth = maxHealth;

        healthText.text = "Health: " + maxHealth;
        if (healthText.text == null) { healthText.text = maxHealth.ToString(); }
        curLvl = GetActiveLevel.curLvl;
        Debug.Log(healthText + " this is health text");
    }

    private void OnEnable()
    {
        Debug.Log(healthText); 
        //healthText = GameObject.FindGameObjectWithTag("HealthText")
        canAttack = true;
        currentHealth = maxHealth;

        healthText.text = "Health: " + maxHealth;
        if (healthText.text == null) { healthText.text = maxHealth.ToString(); }
        curLvl = GetActiveLevel.curLvl;
    }


    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            horizontal = Input.GetAxisRaw("Horizontal"); //a,d,left,right
            vertical = this.transform.position.y;

            direction = new Vector2(horizontal, vertical); //this looks redundant, might comment it out later

            direction = new Vector2(horizontal, rb.velocity.y);
        }
        
        if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && IsTouchingWall())
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        

        if (Input.GetButtonDown("Attack") && canAttack && !PauseMenu.isPaused)
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
                anim.SetTrigger("Attack");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
                enemyTrack(hitEnemies);
                canAttack = false;
            }

            Invoke("AttackReset", attackInterval);
        }

        if (Input.GetKeyDown(KeyCode.E) && boomerangs.Count > 0)
        {
            boomerangs[0].GetComponent<BoomerangStar>().thrown = true;
            Instantiate(boomerangs[0], this.transform.position + new Vector3(1 * orientation.x, 0, 0), transform.rotation);
            boomerangs.RemoveAt(0);
        }

        WallSlide();

        if (direction.x > 0)
        {
            spriteR.flipX = false;
            orientation = Vector2.right;
            attackPoint.transform.position = new Vector3(this.transform.position.x + arOffset, this.transform.position.y, 0);
            anim.SetBool("running", true);
        }

        else if (direction.x < 0)
        {
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

        if (invincible == false)
        {
            currentHealth -= damage;
        }

        healthText.text = "Health: " + currentHealth;

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
        lvlData.lvls[curLvl] = new(lvlData.lvls[curLvl].Item1, lvlData.lvls[curLvl].Item2, 0);
        SceneManager.LoadScene("Level [" + curLvl + "] Scene (1)");
    }

    public void setHealthTxt()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
