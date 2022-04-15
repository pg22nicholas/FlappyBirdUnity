using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseBlink : MonoBehaviour
{

    Image blinkImage;

    void Start()
    {
        blinkImage = GetComponent<Image>();
    }

    public void BlinkWhite()
    {
        StartCoroutine(ColorLerpTo(Color.white, 1));
    }

    // Lerp from white to transparent to simulate a flash of light
    public IEnumerator ColorLerpTo(Color _color, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            blinkImage.color = Color.Lerp(_color, Color.clear, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
