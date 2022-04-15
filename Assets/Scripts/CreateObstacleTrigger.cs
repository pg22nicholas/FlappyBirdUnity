using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacleTrigger : MonoBehaviour
{
    // Create new obstacle when obstacle leaves this trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        ObstacleSet set = collision.GetComponent<ObstacleSet>();
        if (set != null)
        {
            ObstacleManager.PropertyInstance.CreateNewObstacle();
        }
    }
}
