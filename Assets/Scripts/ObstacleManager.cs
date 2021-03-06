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
    [SerializeField] private BackgroundMovement backgroundMovement;
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private PlayerMovement playerMovement;

    List<ObstacleSet> m_ObstacleSetList = new List<ObstacleSet>();

    private int m_score = 0;
    private bool m_IsPlayerLost = false;
    private bool m_IsGameStarted = false;

    public static ObstacleManager PropertyInstance { 
        get { return s_PropertyInstance; }
    }

    private void Awake()
    {
        EndGameUI.SetActive(false);
        // make sure there is only ever one instance of the singleton
        if (s_PropertyInstance != null && s_PropertyInstance != this)
        {
            Destroy(this);
        }
        else
        {
            s_PropertyInstance = this;
        }
    }

    // Spawn new obstacle just off the screen
    public void CreateNewObstacle()
    {
        ObstacleSet set = Instantiate(m_ObstacleSetPrefab);
        m_ObstacleSetList.Add(set);

        // Apply a gap offset only if there has been a previously spawned obstacle
        if (m_ObstacleSetList.Count != 0)
        {
            float yPosPrevObstacle = m_ObstacleSetList[m_ObstacleSetList.Count - 1].gameObject.transform.position.y;
            float yScreenPos = Camera.main.WorldToViewportPoint(new Vector3(0, yPosPrevObstacle, 0)).y;
            float newYScreenPos = Mathf.Clamp(Random.Range(yScreenPos - .15f, yScreenPos + .15f), .1f, .9f);
            float newYScreenPosWorld = Camera.main.ViewportToWorldPoint(new Vector3(0, newYScreenPos, 0)).y;
            set.ApplyInitialYPos(newYScreenPosWorld);
        }
    }

    // Destroy the oldest obstacle in the scene
    public void DestroyLastObstacle()
    {
        // revent deleting from empty list, or when player is dead
        if (m_ObstacleSetList.Count > 0)
        {
            // remove from front of list
            ObstacleSet setToRemove = m_ObstacleSetList[0];
            m_ObstacleSetList.RemoveAt(0);
            Destroy(setToRemove.gameObject);
        }
    }

    // increment score whenever obstacle gap is cleared
    public void OnClearedGap()
    {
        if (!m_IsPlayerLost)
            ScoreText.text = (++m_score).ToString();
    }

    public bool IsPlayerLost
    {
        get { return m_IsPlayerLost;  }
        set
        {
            // Set screen blink only when player set from not dead to dead
            if (!m_IsPlayerLost && value)
            {
                loseBlink.BlinkWhite();
                backgroundMovement.IsStopScrolling = true;
                EndGameUI.SetActive(true);
            }
            m_IsPlayerLost = value;
        }
    }

    public bool IsGameStarted
    {
        get { return m_IsGameStarted; }
        set { 
            // If swapping to game being started, then spawn first obstacle
            if (value && !m_IsGameStarted)
                CreateNewObstacle();
            m_IsGameStarted = value;
        }
    }

    public void StartNewGame()
    {
        ScoreText.text = "0";
        playerMovement.ResetPlayer();

        while (m_ObstacleSetList.Count > 0)
            DestroyLastObstacle();
        m_score = 0;
        m_IsPlayerLost = false;
        m_IsGameStarted = false;
        EndGameUI.SetActive(false);
        backgroundMovement.IsStopScrolling = false;
    }
}
