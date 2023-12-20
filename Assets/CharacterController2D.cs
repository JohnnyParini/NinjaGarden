using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]//automatically ads the rb2d
public class CharacterController2D : MonoBehaviour
{

    Rigidbody2D rigidBody2D;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        rigidBody2D.velocity = motionVector * speed;
    }



}
