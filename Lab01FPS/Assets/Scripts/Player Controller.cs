using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Header("Movement");
    public float speed;
    public float sensitivity;
    public float sprintspeed;

    private float moveFB;
    private float moveLR;
    private float rotX;
    private float rotY;
    private CharacterController cc;
    private Camera _cam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = gameObject.GetComponent<CharacterController>();
        _cam = gameObject.transform.GetChild(0).transform.gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float movementSpeed = speed;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintspeed;
        }
        if !(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = speed;
        }

        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, 0, moveFB);

        transform.Rotate(0, rotX, 0);

        _cam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        cc.Move(movement * Time.deltaTime);
    }
}
