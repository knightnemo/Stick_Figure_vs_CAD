using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float m = 1.0f;
    public GameObject ply; // 获取玩家的位置信息
    Transform targetTransform;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = ply.transform;
        rigidbody2d.gravityScale = 0;
    }

    void Update()
    {
        Vector2 targetPosition = targetTransform.position;
        if (targetPosition.y < rigidbody2d.position.y)
        {
            Debug.Log(targetPosition.x + "/" + targetPosition.y + "/" + rigidbody2d.position.x);
            if ((targetPosition.x <= rigidbody2d.position.x + m) && (targetPosition.x >= rigidbody2d.position.x - m))
            {
                Debug.Log("Triggered fallingblock");
                rigidbody2d.gravityScale = 1;
            }
        }
    }
}