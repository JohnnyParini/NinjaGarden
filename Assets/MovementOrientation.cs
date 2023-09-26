using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOrientation : MonoBehaviour
{

    public SpriteRenderer spriteR;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
