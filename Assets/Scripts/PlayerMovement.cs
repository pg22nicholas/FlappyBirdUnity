using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D RigidBody;
    private float m_MaxZRot = 38f;
    private float m_MinZRot = -90f;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RigidBody.velocity = Vector2.up * 4.5f;
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
        // applt downwards z rotation
        else
        {
            currEulerAngle -= 50 * Time.deltaTime * RigidBody.velocity.y * -1;
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(currEulerAngle, m_MinZRot, m_MaxZRot));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            Debug.Log("Bird collided");
            // TODO:
        }
    }
}
