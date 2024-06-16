using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Target_object : MonoBehaviour
{

    public Transform target;
    private GameObject cube;
    public float box_scale = 10;

    public float Speed = 1.4f;


    void Start()
    {
        // target의 전면 위치 계산
        Vector3 frontPosition = target.position + target.forward * (target.localScale.z / 2 + 1);  // 큐브의 크기와 위치를 고려하여 앞쪽으로 이동

        // 큐브 생성 및 크기 설정
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(target.localScale.x, target.localScale.y, target.localScale.z + box_scale);

        // 큐브의 위치를 target의 전면 위치로 설정
        cube.transform.position = frontPosition;
        
        // 큐브를 반투명하게 설정  
        Material transparentMaterial = new Material(Shader.Find("Standard"));
        transparentMaterial.color = new UnityEngine.Color(0, 0, 0, 0.2f);  // 
        transparentMaterial.SetFloat("_Mode", 3); 
        transparentMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        transparentMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        transparentMaterial.SetInt("_ZWrite", 0);
        transparentMaterial.DisableKeyword("_ALPHATEST_ON");
        transparentMaterial.EnableKeyword("_ALPHABLEND_ON");
        transparentMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        transparentMaterial.renderQueue = 3000;

        // 큐브에 위에 작성한 설정 적용하기
        cube.GetComponent<Renderer>().material = transparentMaterial;
    }

    
    void Update()
    {
        // 이동 속도 설정
        Vector3 movement = transform.forward * Speed * Time.deltaTime;
        transform.position += movement;

        // Target의 움직임에 맞게 큐브도 움직이게 만듦
        Vector3 frontPosition = target.position + target.forward * (target.localScale.z / 2) + target.forward * box_scale / 2;
        cube.transform.position = frontPosition;
        cube.transform.rotation = target.rotation;
    }
}