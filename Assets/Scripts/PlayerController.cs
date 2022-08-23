using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject glock;
    public Transform camTransform;
    private float timea;
    private bool starta;
    private float timeb;
    private bool startb;
    private int inMag=20;
    private int maxMag=20;
    private int simulationFrame;
    public Camera cam1;
    public GameObject camRot;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    private void Update()
    {
        cam1 = FindObjectOfType<Camera>();
        camTransform = cam1.gameObject.transform;
        camRot.transform.rotation = cam1.gameObject.transform.rotation;
        // SendInputToServer();
        ++simulationFrame;
        if (Input.GetKeyDown(KeyCode.Mouse0)&&inMag>0)
        {
            glock.GetComponent<Animator>().SetBool("Shoot", true);
            inMag -= 1;
            starta = true;
            Debug.Log("shoot");
            ClientSend.PlayerShoot(camTransform.forward);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            startb=true;
            glock.GetComponent<Animator>().SetBool("Reload", true);
        }
        if (startb)
        {
            timeb += Time.deltaTime;
            //here
        }
        if (timeb > 1f)
        {
            startb = false;
            glock.GetComponent<Animator>().SetBool("Reload", false);
            inMag = maxMag;
            timeb = 0;
        }

        if (starta)
        {
            timea += Time.deltaTime;
        }
        if (timea>0.05f)
        {
            starta = false;
            glock.GetComponent<Animator>().SetBool("Shoot", false);
            timea= 0;
        }
         
    }
    private void FixedUpdate()
    {
        
       // SendInputToServer();
       // ++simulationFrame;    
       //sending here
    }

    /// <summary>Sends player input to the server.</summary>
    public void SendInputToServer()
    {
        
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(_inputs);
        
    }
}
