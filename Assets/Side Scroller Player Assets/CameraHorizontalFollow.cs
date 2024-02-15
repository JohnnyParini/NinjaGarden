using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontalFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public float smoothSpeed = 0.125f;
    public float zOffSet;
    public float xOffset;


    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Debug.Log(player);
        transform.position = new Vector3(player.transform.position.x+xOffset, transform.position.y, zOffSet);
    }
}
