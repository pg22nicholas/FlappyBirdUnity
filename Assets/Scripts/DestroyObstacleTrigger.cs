using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacleTrigger : MonoBehaviour
{
    // Destroy obstacles that leave this trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ObstacleSet>())
            ObstacleManager.PropertyInstance.DestroyLastObstacle();
    }
}