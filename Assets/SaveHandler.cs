using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveHandler : MonoBehaviour
{
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start()
    {
        initTilemaps();
    }

    private void initTilemaps()
    {
        //get all tilemaps
        //write to dictionary
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach(var map in maps)
        {
            tilemaps.Add(map.name, map);
        }

    }

    public void OnSave()
    {

    }

    public void OnLoad()
    {

    }

}
