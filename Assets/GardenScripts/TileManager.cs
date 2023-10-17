using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin) //will return all positions within the bounds of the tilemap
        {
            //set each tile in the position to be the hiddenInteractableTile (sets invis and interactable)
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
            
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        
        TileBase tile = interactableMap.GetTile(position); //will need to get the tile at the position to determine interactability

        if(tile != null) //if the tile isn't null
        {
            Debug.Log("the tile's name is " + tile.name);
            if (tile.name == "Interactable_Invis")
            {
                return true;
            }
                
        }
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

  
}
