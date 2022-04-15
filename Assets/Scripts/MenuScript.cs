using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton; 

    private void Start()
    {
        m_PlayButton.onClick.AddListener(PlayGame);
    }

    // Goto game
    void PlayGame() {
        SceneManager.LoadScene("GameScene");
    }
}
