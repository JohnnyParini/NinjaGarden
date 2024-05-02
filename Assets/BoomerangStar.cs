using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangStar : MonoBehaviour
{
    public Rigidbody2D rb;
    public int damage;
    public int enemyLayer;
    public int projectileLayer;
    public float acceleration;  // acceleration should be positive to the right, negative to the left.
    public float velocityX;
    public float velocityY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX += acceleration, velocityY += acceleration);
    }
}
