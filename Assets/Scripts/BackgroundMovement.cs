using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float m_BackgroundMoveSpeed = 10f;

    private Camera _camera;
    private float m_BoundSizeX;
    private float m_StartingX;

    private bool m_IsStopScrolling = false;

    void Start()
    {
        _camera = Camera.main;
        m_BoundSizeX = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x * gameObject.transform.localScale.x;
        m_StartingX = gameObject.GetComponent<SpriteRenderer>().transform.position.x;
    }

    void Update()
    {
        if (!m_IsStopScrolling)
        {
            transform.Translate(new Vector3(-m_BackgroundMoveSpeed * Time.deltaTime, 0, 0), Space.World);
            if (transform.position.x < m_StartingX - m_BoundSizeX)
            {
                transform.Translate(new Vector3(m_BoundSizeX, 0, 0), Space.World);
            }
        }
    }

    public bool IsStopScrolling 
    {
        set { m_IsStopScrolling = value;  }
    }
}
