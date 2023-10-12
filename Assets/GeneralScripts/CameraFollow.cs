using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;

    Vector3 camOffset; //distance we want from the target

    // Start is called before the first frame update
    void Start()
    {
        camOffset = transform.position - target.position; //setperate the camera from the target
    }

    private void FixedUpdate() //player moves, then camera moves
    {
        transform.position = target.position + camOffset;
    }

}
