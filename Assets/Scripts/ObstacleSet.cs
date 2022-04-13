using UnityEngine;

public class ObstacleSet : MonoBehaviour
{
    [SerializeField] private Obstacle m_TopObstaclePrefab;
    [SerializeField] private Obstacle m_BottomObstaclePrefab;
    [SerializeField] private float m_MoveSpeed = 5f;

    [SerializeField] public float m_scaleWidth = 1.5f;
    [SerializeField] public float m_scaleGap = 2f;
     
    private void Awake()
    {
        // Set y scale of center of obstacle box collider
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, m_scaleGap);

        // Create top and bottom obstacles
        var topObstacle = Instantiate(m_TopObstaclePrefab, new Vector2(0, 0), Quaternion.identity);
        topObstacle.InitiateObstacle(m_scaleWidth, m_scaleGap / 2 + topObstacle.GetComponent<Renderer>().bounds.extents.y, transform);
        var bottomObstacle = Instantiate(m_BottomObstaclePrefab, new Vector2(0, 0), Quaternion.identity);
        bottomObstacle.InitiateObstacle(m_scaleWidth, -m_scaleGap / 2 - bottomObstacle.GetComponent<Renderer>().bounds.extents.y, gameObject.transform);

        // Push this object off screen so the children are spawned off screen
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1, .5f, 10));
        gameObject.transform.Translate(new Vector2(bottomObstacle.GetComponent<Renderer>().bounds.extents.x, 0));

        // set x scale of center of box collider to equal children
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(bottomObstacle.transform.localScale.x, gameObject.GetComponent<BoxCollider2D>().size.y);
    }

    public void ApplyInitialYPos(float yPos)
    {
        gameObject.transform.position = (new Vector2(transform.position.x, yPos));
    }

    void Update()
    {
        if (!ObstacleManager.PropertyInstance.GetPlayerLost())
        {
            Vector2 moveVector = new Vector2(m_MoveSpeed * Time.deltaTime * -1, 0);
            gameObject.transform.Translate(moveVector);
        }
        
    }
}
