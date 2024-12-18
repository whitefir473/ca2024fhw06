using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MaterialSwitcher : MonoBehaviour
{
    public Renderer coneRenderer;
    public Material[] materials;

    private int currentMaterialIndex = 0;

    public void ChangeMaterial()
    {
        if (coneRenderer != null && materials.Length > 0)
        {
            coneRenderer.material = materials[currentMaterialIndex];
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
        }
    }
}

