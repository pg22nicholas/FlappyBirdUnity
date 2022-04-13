using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D RigidBody;
    private float m_MaxZRot = 38f;
    private float m_MinZRot = -90f;

    private Animator animator;
    private bool isStopped = false;

    private Rigidbody2D rigidbody;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dont update anything if stopped (at bottom of screen)
        if (isStopped)
        {
            return;
        }

        // Player input if still alive
        if (!ObstacleManager.PropertyInstance.GetPlayerLost())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RigidBody.velocity = Vector2.up * 4.5f;
            }
        } 
        // Otherwise player dead, stop animation
        else
        {
            Debug.Log("stop");
            animator.enabled = false;
        }
        
        // if at bottom of screen
        if (gameObject.transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y ||
            Mathf.Abs(gameObject.transform.position.y - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y) < .01)
        {
            Debug.Log("Stopped");
            Destroy(rigidbody);
            isStopped = true;
        }


        float currEulerAngle = transform.eulerAngles.z;
        // set from -180 to 180
        currEulerAngle = (currEulerAngle > 180) ? currEulerAngle - 360 : currEulerAngle;
        // If going upwards, apply upwards z rotation
        if (RigidBody.velocity.y > 0)
        {
            currEulerAngle += 500 * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currEulerAngle, m_MinZRot, m_MaxZRot));
        } 
        // apply downwards z rotation
        else
        {
            currEulerAngle -= 50 * Time.deltaTime * RigidBody.velocity.y * -1;
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currEulerAngle, m_MinZRot, m_MaxZRot));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>() != null)
        {
            Debug.Log("Trigger obstacle Hit");
            ObstacleManager.PropertyInstance.SetPlayerLost(true);
        }
        else if (collision.GetComponent<ScreenBounds>() != null)
        {
            Debug.Log("Trigger boundary Hit");
            ObstacleManager.PropertyInstance.SetPlayerLost(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Debug.Log("Collision obstacle");
            ObstacleManager.PropertyInstance.SetPlayerLost(true);
        }
        else if (collision.gameObject.GetComponent<ScreenBounds>() != null)
        {
            Debug.Log("Collision boundary");
            ObstacleManager.PropertyInstance.SetPlayerLost(true);
        }
    }
}
