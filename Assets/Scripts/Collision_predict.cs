using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collision_predict : MonoBehaviour
{

    private float collisionTime = 0; // 충돌 시간 초기화

    // obj1의 위치 & 속도 & 이동방향
    private GameObject obj1;
    private Vector3 Cluster;
    public float speed1 = 0;
    private float radius1 = 0;
    private Vector3 obj1_vector = Vector3.zero;
    private Vector3 previousPosition1;

    // obj2의 위치 & 속도 
    private GameObject obj2;
    private Vector3 Target;
    public float speed2 = 0;
    private float radius2 = 0;
    private Vector3 obj2_vector = Vector3.zero;
    private Vector3 previousPosition2;

    // 충돌 여부 시각화
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;


    // Start is called before the first frame update
    void Start()
    {
        // Cluster(obj1), Target(obj2) 각각의 값 가져오기
        obj1 = GameObject.Find("Capsule");
        obj2 = GameObject.Find("Target");

        // 초기 위치 설정
        previousPosition1 = obj1.transform.position;
        previousPosition2 = obj2.transform.position;


        // 반지름 측정
        radius1 = obj1.transform.lossyScale.x / 2;
        radius2 = obj2.transform.lossyScale.x / 2;

        // 충돌 여부 초기화
        text1.text = "";
        text2.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // 충돌 시 충돌 여부 시각화
        text1.text = "Collision: O";

        // 충돌 감지 (Boxcast가 동적 객체와 접촉)
        if (Raycast.Collision == true)
        {

            // 시간 간격 설정
            float timeInterval = 0.1f; 

            // 충돌 시간 계산을 위한 반복문
            for (float time = 0; time < 100; time += timeInterval) 
            {
                //Cluster x,z 방향으로의 변화 
                float Cluster_x = Cluster.x + obj1_vector.x * time;
                float Cluster_z = Cluster.z + obj1_vector.z * time;                
                //Target x,z 방향으로의 변화
                float Target_x = Target.x + obj2_vector.x * time;
                float Target_z = Target.z + obj2_vector.z * time;

                // 충돌 여부는 두 원의 중심 간 거리(d)를 이용 Boxcast 가 충돌 감지 시 적용
                float d = Mathf.Sqrt(Mathf.Pow(Cluster_x - Target_x, 2) + Mathf.Pow(Cluster_z - Target_z, 2));
                // 충돌 여부 판별
                if ((d < radius1 + radius2))
                {
                    // 충돌 시간 저장하고 반복문 종료
                    collisionTime = time;
                    break;
                }
                else
                {
                    
                }
                
            }
            
        }

        // 충돌 시간 시각화(소수점 둘째까지)
        text2.text = (collisionTime/10).ToString("F1") + " sec";
    }

    void FixedUpdate()
    {
        // FixedUpdate를 통해 
        Cluster = obj1.transform.position;

        Target = obj2.transform.position;

        obj1_vector = Cluster - previousPosition1;
        obj2_vector = Target - previousPosition2;

        previousPosition1 = Cluster;
        previousPosition2 = Target;

    }
}