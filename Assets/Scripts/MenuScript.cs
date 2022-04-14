using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton; 

    private void Start()
    {
        m_PlayButton.onClick.AddListener(PlayGame);
    }

    void PlayGame() {
        // TODO:
    }
}
