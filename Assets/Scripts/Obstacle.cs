using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Start()
    {

    }

    public void InitiateObstacle(float widthScale, float yPos, Transform parent)
    {
        gameObject.transform.SetParent(parent);

        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(scale.x * widthScale, scale.y, scale.z);
        gameObject.transform.localPosition = new Vector3(0, yPos);
    }
}
