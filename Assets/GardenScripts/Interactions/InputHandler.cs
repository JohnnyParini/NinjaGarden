using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    public static PlayerInput PlayerInput;

    public static Vector2 MoveInput;

    public static bool wasJumpPressed;
    public static bool isJumpBeingPressed;
    public static bool wasJumpReleased;
    public static bool wasAttackPressed;
    public static bool wasDashPressed;
    public static bool wasInteractPressed;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _dashAction;
    private InputAction _interactAction;


    private void Awake()
    {
        _mainCamera = Camera.main;

        //PlayerInput = GetComponent<PlayerInput>();

        //_moveAction = PlayerInput.actions["Movement"];
        //_jumpAction = PlayerInput.actions["Jump"];
        //_attackAction = PlayerInput.actions["Attack"];
        //_dashAction = PlayerInput.actions["Dash"];
        //_interactAction = PlayerInput.actions["Interact"];

    }


    private void Update()
    {
        //something here is super wrong
        //MoveInput = _moveAction.ReadValue<Vector2>();

        //wasJumpPressed = _jumpAction.WasPressedThisFrame();
        //isJumpBeingPressed = _jumpAction.IsPressed();
        //wasJumpReleased = _jumpAction.WasReleasedThisFrame();
        //wasAttackPressed = _attackAction.WasPressedThisFrame();
        //wasDashPressed = _dashAction.WasPressedThisFrame();
        //wasInteractPressed = _interactAction.WasPressedThisFrame();
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
