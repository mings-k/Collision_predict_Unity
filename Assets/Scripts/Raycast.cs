using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Vector3 past_position = Vector3.zero;
    private Vector3 now_position = Vector3.zero;
    public float Speed = 1.4f;

    // �浹 Ȯ�� ����
    public static bool Collision = false;

    void OnDrawGizmos()
    {

        float maxDistance = 100f;
        RaycastHit hit;

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit,
            transform.rotation, maxDistance);

        if (isHit)
        {
            // Ray �ð�ȭ 
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
        // �ʴ� 1.4������ �ӵ��� �̵��ϵ��� ����
        Vector3 movement = transform.forward* Speed * Time.deltaTime;
        transform.position += movement;

        now_position = transform.position;

        float speed = Vector3.Distance(now_position, past_position) / Time.deltaTime;


        past_position = now_position;
    }
}
