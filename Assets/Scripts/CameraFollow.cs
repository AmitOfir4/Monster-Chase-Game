using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
    private Vector3 tempPos;
    private string PLAYER_TAG = "Player";

    [SerializeField] private float minX = -45, maxX = 45;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag(PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            return;
        }
        
        tempPos = transform.position;
        tempPos.x = player.position.x;

        if (tempPos.x < minX)
        {
            tempPos.x = minX;
        }
        else if(tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }

        transform.position = tempPos;
    }
}
