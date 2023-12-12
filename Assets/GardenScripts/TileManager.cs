using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

    public MouseInput mouseInput;

    public UI_Manager UI;

    private int testNum;
    public float tilePosX;
    public float tilePosY;
    public float tilePosZ;


    private void Awake()
    {
        mouseInput = new MouseInput();
        UI = GetComponent<UI_Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();

        testNum = 0;
        foreach(var position in interactableMap.cellBounds.allPositionsWithin) //will return all positions within the bounds of the tilemap
        {
            //set each tile in the position to be the hiddenInteractableTile (sets invis and interactable)
            TileBase tile = interactableMap.GetTile(position);
            tilePosX = position.x;
            tilePosY = position.y;
            tilePosZ = position.z;
            //Debug.Log("The tile at" + position+ " x pos is" + tilePosX); //trying to get pixel positions for better mouse integration. 

            //Debug.Log("The tile is at " + position + " iteration "+ testNum);
            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
            
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouseClick is being called");
            MouseClick();
        }
    }
    public void MouseClick()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //pixel
            //mouseInput.Mouse.MouseClick.ReadValue<Vector2>(); //gets it in pixel coordinates
        Debug.Log("the pixel position is  " + mousePosition);
        mousePosition =Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log("mousePosition is " + mousePosition);
        Vector3Int gridPosition = interactableMap.WorldToCell(mousePosition); //converts to tilemap coordinates
        Debug.Log("the world position is " + gridPosition);
        if (IsInteractable(gridPosition)) //also need to check the player distance
        {
            Debug.Log("You are a genius"); //i've successfully clicked on an interactable tile
            if (!UI.inventoryIsOn)
            {
                Debug.Log("Everything working as planned");
                SetInteracted(gridPosition);
            }

        }
        //if (interactableMap.HasTile(gridPosition)) { }
    }

    public bool IsInteractable(Vector3Int position)
    {
        
        TileBase tile = interactableMap.GetTile(position); //will need to get the tile at the position to determine interactability
        Debug.Log("the tile being checked is at " + position);

        if(tile != null) //if the tile isn't null
        {
            Debug.Log("the tile's name is " + tile.name);
            if (tile.name == "Interactable_Invis")
            {
                return true;
            }
                
        }
        Debug.Log("Tile was null");
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

    public string GetTileName(Vector3Int position)
    {
        if(interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null)
            {
                return tile.name;
            }
            
        }
        return "";
    }
  
}
