using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffSet;

    [SerializeField]
    Vector2 posOffset;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This is the camera's current position
        Vector3 startPos = transform.position;
        //This is the player's current position
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //transform.position = Vector3.Lerp(startPos, endPos, timeOffSet * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);

    }
}
