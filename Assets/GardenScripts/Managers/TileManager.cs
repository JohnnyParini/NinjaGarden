using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class CropTile
{
    public int growTimer;

    public Crop crop;
}

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap, unitMap;
    //[SerializeField] private Tilemap alphaTilemap;
    [SerializeField] private int levelIndex;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile plantableTile;
    [SerializeField] private Tile firstPlantedTile;
    [SerializeField] private Tile secondPlantedTile;
    [SerializeField]     private Tile thirdPlantedTile;
    [SerializeField] private Tile growntile;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;
    [SerializeField] private Tile Strawberry;
    [SerializeField] private Tile Turnip;
    [SerializeField] private Tile Melon;
    [SerializeField] private Tile[] secretTiles;


    public MouseInput mouseInput;

   

    public UI_Manager UI;

    private InventoryManager inventoryManager;

    public Player player;

    private int secretNum1 = 0;
    public float tilePosX;
    public float tilePosY;
    public float tilePosZ;

    /*private void OnLevelWasLoaded(int level)
    {
        inventoryManager = GetComponent<InventoryManager>();
        mouseInput = new MouseInput();
        UI = GetComponent<UI_Manager>();
        player = FindObjectOfType<Player>();
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }*/
    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        mouseInput = new MouseInput();
        UI = GetComponent<UI_Manager>();
        player = FindObjectOfType<Player>();
        dataFromTiles = new Dictionary<TileBase, TileData>();

    }

    public void saveMap()
    {
        //var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

//        newLevel.LevelIndex = levelIndex;
  //      newLevel.name = $"Level {levelIndex}";

        //newLevel.GroundTiles

        /*IEnumerable<Tile> GetTilesFromMap(Tilemap map)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    var levelTile = map.GetTile<Tile>(pos);
                    //yield return new SavedTile()
                    //{
                      //  Position = pos,
                        

                    //};
                }
            }
        }*/
    }

    public void clearMap()
    {

    }

    public void loadMap()
    {

    }

    public void setAwake()
    {
        Awake();
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

        secretNum1 = 0;
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
        //foreach(var position in alphaTilemap.cellBounds.allPositionsWithin)
        //{
         //   TileBase tile = alphaTilemap.GetTile(position);

//        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) //change to .GetMouseButtonDown(for only one)
        {
            if (!Input.GetKey(KeyCode.RightShift))
            {
                Debug.Log("mouseClick is being called");
                MouseClick();
                //GetTileBase(Input.mousePosition);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                Debug.Log("singleClick is being called");
                MouseClick();
            }
            
        }
        
    }

   
    public void MouseClick()
    {
        if (player.inventory.toolbar.selectedSlot.itemName != "" && player.inventory.toolbar.selectedSlot.itemName != null)
        {
            Debug.Log("Item name is " + player.inventory.toolbar.selectedSlot.itemName);
        }
        

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
                    
//                    Debug.Log("Truly a coding savant");

                    if ( player.inventory.toolbar.selectedSlot.itemName== "FarmingStaff") //&& if holding the farming staff)
                    {
                        Debug.Log("farming staff being held");
                        if (!UI.inventoryIsOn)
                        {
                          //f  Debug.Log("Everything working as planned");
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
        if (IsPlantable(gridPosition) && player.inventory.toolbar.selectedSlot.itemName == "PlantStaff")
        {
            Debug.Log("plantable land");
            SetPlanted(gridPosition); 
        }
        if(IsPlantable(gridPosition) && player.inventory.toolbar.selectedSlot.itemName != "FarmingStaff")
        {
            
            plantType( gridPosition, player.inventory.toolbar.selectedSlot.itemName);
            player.inventory.toolbar.selectedSlot.removeItem();
            UI.RefreshAll();
            
            Debug.Log(player.inventory.toolbar.selectedSlot.itemName + " has been ticked down");
            Debug.Log("Planted then removed");
        }
        if (isSecret(gridPosition))
        {
        
            Debug.Log("secretNum1 increased");
            if(secretNum1 >= 4)
            {
                winSecret();
            }
        }
        else
        {
            secretNum1 = 0;
            Debug.Log("secretNum1 reset to 0");
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

    public void SetPlanted(Vector3Int position)
    {
        
            Debug.Log("this area is plantable");
            interactableMap.SetTile(position, firstPlantedTile);
        //find way to make this save between scenes
            //get something from inventory and remove it
        
    }


    // public TileBase GetTileBase(Vector2 mousePosition)
    //{

    //  Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

    //        Vector3Int gridPosition = alphaTilemap.WorldToCell(worldPosition);

    //      TileBase tile = alphaTilemap.GetTile(gridPosition);


    //    Debug.Log("The alpha tile in " + gridPosition + " is " + tile);
    //  if(tile == null)
    //{
    //  Debug.Log("your code sucks you dumb person");
    //}

    //return null;
    //}
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

    public bool isSecret(Vector3Int position)
    {
        for (int i = 0; i < secretNum1+1; i++)
        {
            if (interactableMap.GetTile(position) == secretTiles[i])
            {
                Debug.Log("This is a secret tile");
                if (Input.GetKey(KeyCode.RightShift))
                {
                    secretNum1++;
                    Debug.Log("secretNum is " + secretNum1);
                }
             
                
                return true;
            }
        }

        return false;
    }

    public void winSecret()
    {
        Debug.Log("You found the secret");
    }

    public void curse()
    {
        Debug.Log("You've been cursed");
    }

    public void plantType(Vector3Int position, string type)
    {
        Debug.Log("the type is " + type);
        if (type == "Strawberry")
        {
            //set tile type to strawberry
            interactableMap.SetTile(position, Strawberry);
        }
        if (type == "Melon")
        {
            interactableMap.SetTile(position, Melon);
        }
        if (type == "Turnip")
        {
            interactableMap.SetTile(position, Turnip);
        }

    }


}
