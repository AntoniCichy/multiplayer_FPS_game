using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLikeServer : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    public PlayerController playerController;
    public Transform shootOrigin;
    public float gravity = -9.81f;
    private float moveSpeed = 100f;
    public float jumpSpeed = 5f;
    public float health;
    public float maxHealth = 100f;
    private List<Vector3> positons;
    private bool first;
    private int iteration;

    private bool[] inputs;
    private float yVelocity;







    private float timer;
    private int currentTick;
    private float minTimeBetweenTicks;
    private const float SERVER_TICK_RATE = 30f;
    private const int BUFFER_SIZE = 1024;
    private void Start()
    {
       // gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed = 2;
        //jumpSpeed *= Time.fixedDeltaTime;
        inputs = new bool[5];
        first = true;
        positons = new List<Vector3> { };
        minTimeBetweenTicks = 1f / SERVER_TICK_RATE;
        Debug.Log("MIN TIME"+minTimeBetweenTicks);

    }


    /// <summary>Processes player input and moves the player.</summary>
    public void Update()
    {



        timer += Time.deltaTime;

        while (timer >= minTimeBetweenTicks)
        {
            currentTick = currentTick % BUFFER_SIZE;  
            timer -= minTimeBetweenTicks;
            if (positons.Count == BUFFER_SIZE)
            {
                positons = new List<Vector3> { };
            }
            playerController.GetComponent<PlayerController>().SendInputToServer();
           // Debug.Log(gameObject.transform.position + "----" + currentTick);
            positons.Add(gameObject.transform.position);
            currentTick++;





            bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            //Input.GetKey(KeyCode.Space)
        };
            if (health <= 0f)
            {
                return;
            }
            Vector2 _inputDirection = Vector2.zero;
            if (inputs[0])
            {
                _inputDirection.y += 1;
            }
            if (inputs[1])
            {
                _inputDirection.y -= 1;
            }
            if (inputs[2])
            {
                _inputDirection.x -= 1;
            }
            if (inputs[3])
            {
                _inputDirection.x += 1;
            }

            Move(_inputDirection);
            /*
            if(first==false)
            {
                if (positons[positons.Count-1] != gameObject.transform.position)
                {
                    positons.Add(gameObject.transform.position);
                    iteration++;
                    Debug.Log(positons[iteration]);
                    Debug.Log(positons[iteration].y);
                }
            }
            else
            {
                positons.Add(gameObject.transform.position);
                Debug.Log(positons[iteration]);
                first = false;
            }
            */
           // positons.Add(gameObject.transform.position);
            //Debug.Log(gameObject.transform.position);
        }







    }
    public void Check(Vector3 position,int tick)
    {
        Debug.Log("check"+ positons[tick] +"   "+ position);

            if (positons[tick] == position)
            {
                return;
            }
            else
            {
                Vector3 add = position - positons[tick];
                gameObject.transform.position += add;
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            }
    }
    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;

        _moveDirection *= moveSpeed;
        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                
                yVelocity = jumpSpeed;
            }
        }
        yVelocity += gravity* minTimeBetweenTicks;

        

        _moveDirection.y = yVelocity;
      //  Debug.Log(currentTick+" "+_moveDirection * minTimeBetweenTicks);
        controller.Move(_moveDirection* minTimeBetweenTicks);
        

    }
}
