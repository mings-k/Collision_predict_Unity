using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Vector3 past_position = Vector3.zero;
    private Vector3 now_position = Vector3.zero;
    public float Speed = 1.4f;

    // 충돌 확인 변수
    public static bool Collision = false;

    void OnDrawGizmos()
    {

        float maxDistance = 100f;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit,
            transform.rotation, maxDistance);

        if (isHit)
        {
            // Ray 시각화 
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
            Collision = true;
           
        }

        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            Collision = false;
        }
    }

    void Update()
    {
        // 초당 1.4미터의 속도로 이동하도록 설정
        Vector3 movement = transform.forward* Speed * Time.deltaTime;
        transform.position += movement;

        now_position = transform.position;

        float speed = Vector3.Distance(now_position, past_position) / Time.deltaTime;


        past_position = now_position;
    }
}
