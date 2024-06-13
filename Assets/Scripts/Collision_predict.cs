using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collision_predict : MonoBehaviour
{

    private float collisionTime = 0; // �浹 �ð� �ʱ�ȭ

    // obj1�� ��ġ & �ӵ� & �̵�����
    private GameObject obj1;
    private Vector3 Cluster;
    public float speed1 = 0;
    private float radius1 = 0;
    private Vector3 obj1_vector = Vector3.zero;
    private Vector3 previousPosition1;

    // obj2�� ��ġ & �ӵ� 
    private GameObject obj2;
    private Vector3 Target;
    public float speed2 = 0;
    private float radius2 = 0;
    private Vector3 obj2_vector = Vector3.zero;
    private Vector3 previousPosition2;

    // �浹 ���� �ð�ȭ
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;


    // Start is called before the first frame update
    void Start()
    {
        // Cluster(obj1), Target(obj2) ������ �� ��������
        obj1 = GameObject.Find("Capsule");
        obj2 = GameObject.Find("Target");

        // �ʱ� ��ġ ����
        previousPosition1 = obj1.transform.position;
        previousPosition2 = obj2.transform.position;


        // ������ ����
        radius1 = obj1.transform.lossyScale.x / 2;
        radius2 = obj2.transform.lossyScale.x / 2;

        // �浹 ���� �ʱ�ȭ
        text1.text = "";
        text2.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // �浹 �� �浹 ���� �ð�ȭ
        text1.text = "Collision: O";

        // �浹 ���� (Boxcast�� ���� ��ü�� ����)
        if (Raycast.Collision == true)
        {

            // �ð� ���� ����
            float timeInterval = 0.1f; 

            // �浹 �ð� ����� ���� �ݺ���
            for (float time = 0; time < 100; time += timeInterval) 
            {
                //Cluster x,z ���������� ��ȭ 
                float Cluster_x = Cluster.x + obj1_vector.x * time;
                float Cluster_z = Cluster.z + obj1_vector.z * time;                
                //Target x,z ���������� ��ȭ
                float Target_x = Target.x + obj2_vector.x * time;
                float Target_z = Target.z + obj2_vector.z * time;

                // �浹 ���δ� �� ���� �߽� �� �Ÿ�(d)�� �̿� Boxcast �� �浹 ���� �� ����
                float d = Mathf.Sqrt(Mathf.Pow(Cluster_x - Target_x, 2) + Mathf.Pow(Cluster_z - Target_z, 2));
                // �浹 ���� �Ǻ�
                if ((d < radius1 + radius2))
                {
                    // �浹 �ð� �����ϰ� �ݺ��� ����
                    collisionTime = time;
                    break;
                }
                else
                {
                    
                }
                
            }
            
        }

        // �浹 �ð� �ð�ȭ(�Ҽ��� ��°����)
        text2.text = (collisionTime/10).ToString("F1") + " sec";
    }

    void FixedUpdate()
    {
        // FixedUpdate�� ���� 
        Cluster = obj1.transform.position;

        Target = obj2.transform.position;

        obj1_vector = Cluster - previousPosition1;
        obj2_vector = Target - previousPosition2;

        previousPosition1 = Cluster;
        previousPosition2 = Target;

    }
}