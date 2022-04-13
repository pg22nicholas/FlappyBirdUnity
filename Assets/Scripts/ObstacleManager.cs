using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager s_PropertyInstance;

    [SerializeField] private ObstacleSet m_ObstacleSetPrefab;
    [SerializeField] private Camera camera;
    [SerializeField] private LoseBlink loseBlink;
    [SerializeField] private Text ScoreText;

    List<ObstacleSet> m_ObstacleSetList = new List<ObstacleSet>();

    private int m_score = 0;
    private bool m_IsPlayerLost = false;

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
        m_ObstacleSetList.Add(set);

        // Apply an offset only if there has been a previously spawned obstacle
        if (m_ObstacleSetList.Count != 0)
        {
            float yPosPrevObstacle = m_ObstacleSetList[m_ObstacleSetList.Count - 1].gameObject.transform.position.y;
            float yScreenPos = Camera.main.WorldToViewportPoint(new Vector3(0, yPosPrevObstacle, 0)).y;
            float newYScreenPos = Mathf.Clamp(Random.Range(yScreenPos - .15f, yScreenPos + .15f), .1f, .9f);
            float newYScreenPosWorld = Camera.main.ViewportToWorldPoint(new Vector3(0, newYScreenPos, 0)).y;
            set.ApplyInitialYPos(newYScreenPosWorld);
        }
    }

    public void DestroyLastObstacle()
    {
        ObstacleSet setToRemove = m_ObstacleSetList[0];
        m_ObstacleSetList.RemoveAt(0);
        Destroy(setToRemove.gameObject);
    }

    public void SetPlayerLost(bool isPlayerLost)
    {
        // Set screen blink only when player set from not dead to dead
        if (!m_IsPlayerLost && isPlayerLost)
            loseBlink.BlinkWhite();

        m_IsPlayerLost = isPlayerLost;
    }

    public bool GetPlayerLost()
    {
        return m_IsPlayerLost;
    }

    public void OnClearedGap()
    {
        if (!m_IsPlayerLost)
            ScoreText.text = (++m_score).ToString();
    }
}
