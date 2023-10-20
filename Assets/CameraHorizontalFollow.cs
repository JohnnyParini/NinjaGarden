using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontalFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public float smoothSpeed = 0.125f;
    public float zOffSet;
    
 
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, zOffSet);
    }
}
