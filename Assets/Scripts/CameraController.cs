using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerManager player;
    public PlayerLikeServer player1;
    public Transform playerobject;
    public float sensitivity = 100f;
    public float clampAngle = 85f;
    public float smooth = 0.1f;
    public Vector3 offset;

    private float verticalRotation;
    private float horizontalRotation;
    public Quaternion CamRotation;

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player1.transform.eulerAngles.y;
    }

    private void Update()
    {
        player = FindObjectOfType<PlayerManager>();
        player1 = FindObjectOfType<PlayerLikeServer>();
        playerobject = player1.gameObject.transform;
        CamRotation = gameObject.transform.rotation;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorMode();
        }
        if (Cursor.lockState == CursorLockMode.Locked)
        {
           Look(); 
        }
        
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }
    private void LateUpdate()
    {
        Vector3 desiredPos = playerobject.position + offset;
        Vector3 smoothenedPos = Vector3.Lerp(transform.position,desiredPos,smooth);
        transform.position = smoothenedPos;

    }
    private void Look()
    {
        float _mouseVertical = -Input.GetAxis("Mouse Y");
        float _mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += _mouseVertical * sensitivity * Time.deltaTime;
        horizontalRotation += _mouseHorizontal * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        player1.gameObject.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

    }
    private void ToggleCursorMode()
    {
        Cursor.visible=!Cursor.visible;
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
        
}
