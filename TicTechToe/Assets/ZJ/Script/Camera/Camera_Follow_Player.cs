using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour
{
    public Transform player;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;

    Vector3 cameraPos;
 
    void LateUpdate()
    {
        if(transform.position != player.position)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
       
    }
}
