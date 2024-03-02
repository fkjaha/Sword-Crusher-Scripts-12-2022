using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsController : MonoBehaviour
{
    [SerializeField] private List<MaterialsPreset> presets;

    public void ApplyPreset(int index)
    {
        if(!Application.isPlaying) return;
        MaterialsPreset preset = presets[index % presets.Count];
        foreach (MaterialGroup group in preset.GetMaterialGroups)
        {
            foreach (Renderer groupGetRenderer in group.GetRenderers)
            {
                groupGetRenderer.material = group.GetMaterial;
                // Debug.Log(group.GetMaterial.name);
            }
        }
    }
}

[Serializable]
public class MaterialsPreset
{
    public List<MaterialGroup> GetMaterialGroups => materialGroups;
    
    [SerializeField] private List<MaterialGroup> materialGroups;
}

[Serializable]
public class MaterialGroup
{
    public List<Renderer> GetRenderers => renderers;
    public Material GetMaterial => material;
    public int GetMaterialPositionIndex => materialPositionIndex;
    
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private Material material;
    [SerializeField] private int materialPositionIndex;
}
