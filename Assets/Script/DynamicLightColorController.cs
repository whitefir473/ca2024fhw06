using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossColorChange : MonoBehaviour
{
    public Material[] beamMaterials; // 每盞燈的材質
    public Color[] colors; // 顏色數組，用於交叉變換
    public float colorChangeInterval = 1f; // 顏色變換的時間間隔

    private float timer;
    private int[] currentColorIndexes; // 用來記錄每個光束的當前顏色索引

    void Start()
    {
        if (beamMaterials.Length == 0 || colors.Length == 0)
        {
            Debug.LogError("請確保已經為光束材質和顏色數組設置值！");
            return;
        }

        // 初始化顏色索引
        currentColorIndexes = new int[beamMaterials.Length];
        for (int i = 0; i < beamMaterials.Length; i++)
        {
            currentColorIndexes[i] = i % colors.Length; // 每個光束初始化為不同顏色
            beamMaterials[i] = new Material(beamMaterials[i]); // 克隆材質，避免共用問題
            beamMaterials[i].SetColor("_Color", colors[currentColorIndexes[i]]);
        }
    }

    void Update()
    {
        // 計時控制顏色變化
        timer += Time.deltaTime;
        if (timer >= colorChangeInterval)
        {
            ChangeBeamColors();
            timer = 0f;
        }
    }

    void ChangeBeamColors()
    {
        // 每次顏色切換後，交替顯示不同顏色
        for (int i = 0; i < beamMaterials.Length; i++)
        {
            // 計算當前顏色索引（交叉變換邏輯）
            int nextColorIndex = (currentColorIndexes[i] + 1) % colors.Length;
            currentColorIndexes[i] = nextColorIndex;  // 更新顏色索引

            // 平滑過渡顏色
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

        material.SetColor("_Color", targetColor); // 確保最後設定為目標顏色
    }
}


