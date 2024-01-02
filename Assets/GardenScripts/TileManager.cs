using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap alphaTilemap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile plantableTile;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;

    public MouseInput mouseInput;

   

    public UI_Manager UI;

    private InventoryManager inventoryManager;

    public Player player;

    private int testNum;
    public float tilePosX;
    public float tilePosY;
    public float tilePosZ;


    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        mouseInput = new MouseInput();
        UI = GetComponent<UI_Manager>();
        player = FindObjectOfType<Player>();
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }

    // Start is called before the first frame update
    void Start()
    {

       //foreach(TileData tileData in tileDatas)
        //{
          //  foreach(TileBase tile in tileData.tiles)
           // {
             //   dataFromTiles.Add(tile, tileData);
           // }
        //}

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
        foreach(var position in alphaTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = alphaTilemap.GetTile(position);

        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouseClick is being called");
            MouseClick();
            GetTileBase(Input.mousePosition);
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

            string tileName = GetTileName(gridPosition);


            if (!string.IsNullOrWhiteSpace(tileName))
            {
                //if we get something back that isn't empty:
                if (tileName == "Interactable_Invis")
                {
                    
                    Debug.Log("Truly a coding savant");

                    if ( player.inventory.toolbar.selectedSlot.itemName== "FarmingStaff") //&& if holding the farming staff)
                    {
                        Debug.Log("farming staff being held");
                        if (!UI.inventoryIsOn)
                        {
                            Debug.Log("Everything working as planned");
                            SetInteracted(gridPosition);
                        }
                    }
                    else
                    {
                        Debug.Log("not holding FarmingStaff");
                    }
                }
               
            }
        

        }
        if (IsPlantable(gridPosition))
        {
            Debug.Log("plantable land");
        }
        //if (interactableMap.HasTile(gridPosition)) { }
    }


    public bool IsInteractable(Vector3Int position)
    {
        
        TileBase tile = interactableMap.GetTile(position); //will need to get the tile at the position to determine interactability
        Debug.Log("the tile being checked is at " + position);

        if (tile != null) //if the tile isn't null
        {
            Debug.Log("the tile's name is " + tile.name);
            if (tile.name == "Interactable_Invis")
            {
                return true;
            }

        }
        else {
            Debug.Log("Tile was null"); 
        }
        return false;
    }

    public bool IsPlantable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position); //will need to get the tile at the position to determine interactability
        Debug.Log("the tile being checked is at " + position);

        if(tile != null)
        {
            Debug.Log("not null tile");
            if(tile.name == "Summer_Plowed")
            {
                return true;
            }
        }
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
    }


    public TileBase GetTileBase(Vector2 mousePosition)
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3Int gridPosition = alphaTilemap.WorldToCell(worldPosition);

        TileBase tile = alphaTilemap.GetTile(gridPosition);

        
        Debug.Log("The alpha tile in " + gridPosition + " is " + tile);
        if(tile == null)
        {
            Debug.Log("your code sucks you dumb person");
        }

        return null;
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
