using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof(Item))]
public class collectable : MonoBehaviour
{ 
    //player walks into collectable
    //add collectable to player
    //delete collectable

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>(); //sees if the collision is from the player

        if (player)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                player.Inventory.Add(item); //adds the collectable
                Destroy(this.gameObject);
            }
        }
    }

}

