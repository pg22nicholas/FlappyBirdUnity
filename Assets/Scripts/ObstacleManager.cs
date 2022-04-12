using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager s_PropertyInstance;

    [SerializeField] private ObstacleSet m_ObstacleSetPrefab;
    [SerializeField] private Camera camera;

    List<ObstacleSet> obstacleSetList = new List<ObstacleSet>();

    public static ObstacleManager PropertyInstance { 
        get { return s_PropertyInstance; }
    }

    private void Awake()
    {
        // make sure there is only ever one instance of the singleton
        if (s_PropertyInstance != null && s_PropertyInstance != this)
        {
            Destroy(this);
        }
        else
        {
            s_PropertyInstance = this;
        }

        CreateNewObstacle();
    }

    public void CreateNewObstacle()
    {
        ObstacleSet set = Instantiate(m_ObstacleSetPrefab);
        obstacleSetList.Add(set);

        // Apply an offset only if there has been a previously spawned obstacle
        if (obstacleSetList.Count != 0)
        {
            float yPosPrevObstacle = obstacleSetList[obstacleSetList.Count - 1].gameObject.transform.position.y;
            float yScreenPos = Camera.main.WorldToViewportPoint(new Vector3(0, yPosPrevObstacle, 0)).y;
            float newYScreenPos = Mathf.Clamp(Random.Range(yScreenPos - .15f, yScreenPos + .15f), .1f, .9f);
            float newYScreenPosWorld = Camera.main.ViewportToWorldPoint(new Vector3(0, newYScreenPos, 0)).y;
            set.ApplyYPos(newYScreenPosWorld);
        }

    }
}
