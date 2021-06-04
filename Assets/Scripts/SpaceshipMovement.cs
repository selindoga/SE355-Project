using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class SpaceshipMovement : MonoBehaviour
{
    public float HorizontalSpeed = 0.2f;
    public float VerticalSpeed = 0.2f;
    public Joystick joystick;
    public Joystick RotationJoystick;
    public float RotationHorizontalMove;
    public float RotationVerticalMove;
    public float rotation_multiplier = 63.6493f; // because = 45 / 0.707
    
    public float HorizontalMove;
    public float VerticalMove;
    private Rigidbody2D rigidb;
    private Touch touch0;
    private Touch touch1;
    private Collider2D coll;
    
    private bool isDashing;
    private float DoubleTapTime;
    private float dashingLocalY;
    private float dashSpeed = 6f;


    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        
    }

    void FixedUpdate()
    {
        HorizontalMove = joystick.Horizontal;
        VerticalMove = joystick.Vertical;
        RotationHorizontalMove = RotationJoystick.Horizontal;
        RotationVerticalMove = RotationJoystick.Vertical;
        Vector2 target_velocity = ( Vector3.right * (HorizontalMove * HorizontalSpeed )) + (Vector3.up * (VerticalMove * VerticalSpeed));
        rigidb.AddForce(target_velocity, ForceMode2D.Impulse);
        RotateLocally();
    }

    void RotateLocally()
    {
        if (((RotationHorizontalMove > 0f && RotationHorizontalMove < 0.707f) && RotationVerticalMove > 0f) || ((RotationHorizontalMove < 0f && RotationHorizontalMove > -0.707f) && RotationVerticalMove > 0f))
        {
            transform.localEulerAngles = new Vector3(0,0,-(RotationHorizontalMove * rotation_multiplier)); // 1 ve 8
        }
        else if (((RotationVerticalMove > 0f && RotationVerticalMove < 0.707f) && RotationHorizontalMove > 0f) || ((RotationVerticalMove < 0f && RotationVerticalMove > -0.707f) && RotationHorizontalMove > 0f))
        {
            transform.localEulerAngles = new Vector3(0,0,(RotationVerticalMove * rotation_multiplier) - 90); // 2 ve 3
        }
        else if (((RotationVerticalMove > 0f && RotationVerticalMove < 0.707f) && RotationHorizontalMove < 0f) || ((RotationVerticalMove < 0f && RotationVerticalMove > -0.707f) && RotationHorizontalMove < 0f))
        {
            transform.localEulerAngles = new Vector3(0,0,-(RotationVerticalMove * rotation_multiplier) + 90); // 6 ve 7
        }
        else if (((RotationHorizontalMove > 0f && RotationHorizontalMove < 0.707f) && RotationVerticalMove < 0f) || ((RotationHorizontalMove < 0f && RotationHorizontalMove > -0.707f) && RotationVerticalMove < 0f))
        {
            transform.localEulerAngles = new Vector3(0,0,(RotationHorizontalMove * rotation_multiplier) - 180); // 4 ve 5
        }
        
    }
    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        return input;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rigidb.velocity = new Vector2(0f, 0f);
        rigidb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
    }

    void Update()
    {
        touch0 = Input.GetTouch(0);
        if (Touching0())
        {
            
            if (Touching0() && (DoubleTapTime > Time.time))
            {
                if (! isDashing)
                {
                    StartCoroutine(Dash());
                }
            }
            else
            {
                DoubleTapTime = Time.time + 0.3f;
            }
        }
    }
    bool Touching0()
    {
        return (((touch0.position.x > Screen.width / 2f) && (touch0.position.y < (Screen.height / 2))) &&
         (touch0.phase == TouchPhase.Began));
    }
}
