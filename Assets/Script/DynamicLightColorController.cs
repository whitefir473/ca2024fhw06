using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossColorChange : MonoBehaviour
{
    public Material[] beamMaterials; // �C���O������
    public Color[] colors; // �C��ƲաA�Ω��e�ܴ�
    public float colorChangeInterval = 1f; // �C���ܴ����ɶ����j

    private float timer;
    private int[] currentColorIndexes; // �ΨӰO���C�ӥ�������e�C�����

    void Start()
    {
        if (beamMaterials.Length == 0 || colors.Length == 0)
        {
            Debug.LogError("�нT�O�w�g����������M�C��Ʋճ]�m�ȡI");
            return;
        }

        // ��l���C�����
        currentColorIndexes = new int[beamMaterials.Length];
        for (int i = 0; i < beamMaterials.Length; i++)
        {
            currentColorIndexes[i] = i % colors.Length; // �C�ӥ�����l�Ƭ����P�C��
            beamMaterials[i] = new Material(beamMaterials[i]); // �J������A�קK�@�ΰ��D
            beamMaterials[i].SetColor("_Color", colors[currentColorIndexes[i]]);
        }
    }

    void Update()
    {
        // �p�ɱ����C���ܤ�
        timer += Time.deltaTime;
        if (timer >= colorChangeInterval)
        {
            ChangeBeamColors();
            timer = 0f;
        }
    }

    void ChangeBeamColors()
    {
        // �C���C�������A�����ܤ��P�C��
        for (int i = 0; i < beamMaterials.Length; i++)
        {
            // �p���e�C����ޡ]��e�ܴ��޿�^
            int nextColorIndex = (currentColorIndexes[i] + 1) % colors.Length;
            currentColorIndexes[i] = nextColorIndex;  // ��s�C�����

            // ���ƹL���C��
            StartCoroutine(LerpColor(beamMaterials[i], colors[nextColorIndex]));
        }
    }

    IEnumerator LerpColor(Material material, Color targetColor)
    {
        Color initialColor = material.GetColor("_Color");
        float duration = colorChangeInterval;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            material.SetColor("_Color", Color.Lerp(initialColor, targetColor, elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.SetColor("_Color", targetColor); // �T�O�̫�]�w���ؼ��C��
    }
}


