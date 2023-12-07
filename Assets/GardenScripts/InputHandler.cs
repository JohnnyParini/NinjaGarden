using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;


    private void Awake()
    {
        _mainCamera = Camera.main;

    }

    public void onClick(InputAction.CallbackContext context)
    {
        if (!context.started)  return;
       
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) return;

        //we now know that we have clicked and hit something

        Debug.Log(rayHit.collider.gameObject.name);
    }


}
