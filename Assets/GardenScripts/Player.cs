using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public inventory Inventory;

    private void Awake()
    {
        Inventory = new inventory(21);
    }
}
