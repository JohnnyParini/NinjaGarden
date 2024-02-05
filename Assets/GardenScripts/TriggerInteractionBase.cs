using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour, IInterface
{
    public GameObject Player { get; set; }
    public bool canInteract { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //if (canInteract)
        //{
         //   if (UserInput.wasInteractPressed)
          //  {
           //     Interact();
            //}
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public virtual void Interact() {}
}
