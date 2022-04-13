using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float upwardsForce = 4.5f;

    
    private float m_MaxZRot = 38f;
    private float m_MinZRot = -90f;
    private bool isStopped = false;

    private Rigidbody2D RigidBody;
    private Animator Animator;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dont update anything if stopped (at bottom of screen)
        if (isStopped)
        {
            RigidBody.Sleep();
            return;
        }

        // Player input if still alive
        if (!ObstacleManager.PropertyInstance.GetPlayerLost())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RigidBody.velocity = Vector2.up * upwardsForce;
            }
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
            KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            KillPlayer();
            isStopped = true;
        } 
        else if (collision.collider.tag == "Ceiling")
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        ObstacleManager.PropertyInstance.SetPlayerLost(true);
        Animator.enabled = false;
    }

}
