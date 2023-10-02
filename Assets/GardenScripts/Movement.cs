using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;

    public Animator animator;
    public SpriteRenderer spriteR;

    private Vector3 direction; 

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //a,d,left,right
        float vertical = Input.GetAxisRaw("Vertical"); //w,s, up, down

        direction = new Vector3(horizontal, vertical,0);
        //Debug.Log(direction);

        AnimateMovement(direction);



        

    }

    private void FixedUpdate()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0) //moving
            {
                animator.SetBool("isMoving", true);

                animator.SetFloat("horizontal", direction.x); 
                animator.SetFloat("vertical", direction.y);

                if (direction.x > 0)
                {
                    //Debug.Log("flip false");
                    spriteR.flipX = false;
                }

                else if (direction.x < 0)
                {
                    //Debug.Log("flip true");
                    spriteR.flipX = true;
                }


            }
            else //not moving
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    //main goals, get input from player
    //apply movement to sprite
    //need to know: current pos (transform.position)



}
