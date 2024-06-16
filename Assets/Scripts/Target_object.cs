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
        // target�� ���� ��ġ ���
        Vector3 frontPosition = target.position + target.forward * (target.localScale.z / 2 + 1);  // ť���� ũ��� ��ġ�� ����Ͽ� �������� �̵�

        // ť�� ���� �� ũ�� ����
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(target.localScale.x, target.localScale.y, target.localScale.z + box_scale);

        // ť���� ��ġ�� target�� ���� ��ġ�� ����
        cube.transform.position = frontPosition;
        
        // ť�긦 �������ϰ� ����  
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

        // ť�꿡 ���� �ۼ��� ���� �����ϱ�
        cube.GetComponent<Renderer>().material = transparentMaterial;
    }

    
    void Update()
    {
        // �̵� �ӵ� ����
        Vector3 movement = transform.forward * Speed * Time.deltaTime;
        transform.position += movement;

        // Target�� �����ӿ� �°� ť�굵 �����̰� ����
        Vector3 frontPosition = target.position + target.forward * (target.localScale.z / 2) + target.forward * box_scale / 2;
        cube.transform.position = frontPosition;
        cube.transform.rotation = target.rotation;
    }
}