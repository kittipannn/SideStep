using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    float oldPos;
    //FloatingJoystick floatingJoystick;
    public CharacterController controller;
    public float normalSpeed = 6f;
    public float forwardSpeed = 6f;
    public float timeToBoost = 3f;
    public bool isBoost = false;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Animator animator;
    //Vector3 direction;

    private void OnEnable()
    {
        //floatingJoystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<FloatingJoystick>();
        GameManage.GMinstance.playerList.Add(this.gameObject);
        
    }
    private void OnDisable()
    {
        GameManage.GMinstance.playerList.Remove(this.gameObject);
    }
    private void Start()
    {
        animator = this.transform.GetChild(3).GetComponent<Animator>();
        //if (this.gameObject == GameManage.GMinstance.playerList[0])
        //    animator = this.transform.GetChild(2).GetComponent<Animator>();

        //else
        //    animator = this.transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManage.GMinstance.gameStart)
        {
            animator = this.transform.GetChild(3).GetComponent<Animator>();
            if (!GameManage.GMinstance.finishInStageMode)
            {
                animator.SetBool("run",true);
                move();
                //moveMent();
            }
            else if (GameManage.GMinstance.finishInStageMode)
            {
                animator.SetBool("run", false);
            }
        }
        forwardSpeed = GameManage.GMinstance.forwardSpeedPlayer;
        checkBorder();
    }
    //void moveMent() 
    //{
    //    Vector3 direction;
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    velocity.z = forwardSpeed;
        
    //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    //    {
    //        direction = new Vector3 (horizontal, 0f, 0).normalized;
    //    }
    //    else
    //    {
    //        direction = new Vector3(floatingJoystick.Horizontal, 0f, 0f).normalized;
    //    }

    //    direction = direction.normalized;
    //    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle / 2, ref turnSmoothVelocity, turnSmoothTime);
    //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //    controller.Move(direction * normalSpeed * Time.deltaTime);
    //    //if (direction.magnitude >= 0.1f && isBoost)
    //    //{
    //    //    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    //    //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle / 2, ref turnSmoothVelocity, turnSmoothTime);
    //    //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //    //    controller.Move(direction * normalSpeed * Time.deltaTime);
    //    //}
    //    //else
    //    //{
    //    //    float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    //    //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle/2, ref turnSmoothVelocity, turnSmoothTime);
    //    //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //    //    controller.Move(direction * normalSpeed * Time.deltaTime);
    //    //}
    //    velocity.y += gravity * Time.deltaTime;
    //    controller.Move(velocity * Time.deltaTime);
    //}
    void move()
    {
        Vector3 direction = Vector3.zero;
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        velocity.z = forwardSpeed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            direction = new Vector3(horizontal, 0f, 0).normalized;
        }
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {

                case TouchPhase.Began:
                    oldPos = touch.position.x;
                    direction = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                //case TouchPhase.Stationary:
                //    oldPos = touch.position.x;
                //    direction = new Vector3(0.0f, 0.0f, 0.0f);
                //    break;

                case TouchPhase.Moved:
                    if (oldPos > touch.position.x)
                    {
                        direction = new Vector3(-1 , 0.0f, 0.0f);
                    }
                    else
                    {
                        direction = new Vector3(1 , 0.0f, 0.0f);
                    }
                    

                    break;


                case TouchPhase.Ended:
                    oldPos = touch.position.x;
                    direction = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
            }
        }

        if (direction.magnitude >= 0.1f && isBoost)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle / 2, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * normalSpeed * Time.deltaTime);
        }
        else
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle / 2, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * normalSpeed * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void checkBorder() 
    {
        if (this.transform.position.x >= 8)
        {
            this.transform.position = new Vector3(8, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x <= -8)
        {
            this.transform.position = new Vector3(-8, this.transform.position.y, this.transform.position.z);
        }
    }



}
