using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Variables")]
    public int moveSpeed;
    public RigidBody2D rb;
    public Vector2 moveInput;

    [Header("Interact Variables")]
    public bool interactInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if(interactInput)
        {
            interactInput = false;
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            interactInput = true;
        }
    }

    public void TryInteract()
    {
        Debug.Log("Pressed interact button");
    }

}
