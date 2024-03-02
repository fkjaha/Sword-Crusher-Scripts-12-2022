using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DestructiveShapeGenerator : MonoBehaviour
{
    [SerializeField] private Pixel pixelPrefab;
    [SerializeField] private Material materialPrefab;
    [SerializeField] private Texture2D sourceSprite;
    [SerializeField] private DestructiveShape blankShape;
    [Tooltip("Directory starting from any Resources folder!")]
    [SerializeField] private string materialsStoreDirectory;
    [SerializeField] private Vector2 pixelsSpacing;

    private List<Material> _availableMaterials;

    [ContextMenu("Generate")]
    public void GenerateDestructiveShapeFromSelected()
    {
        GetMaterialsFromDirectory(materialsStoreDirectory);
        GenerateDestructiveShape(blankShape, sourceSprite);
    }

    public void GenerateDestructiveShape(DestructiveShape targetShape, Texture2D referenceSprite, UnityAction<float> action = null)
    {
        GetMaterialsFromDirectory(materialsStoreDirectory);
        
        Transform shapeTransform = targetShape.transform;
        Vector3 startScale = shapeTransform.localScale;
        shapeTransform.localScale = Vector3.one;
        targetShape.ResetShape();
        
        Color[] pixelColors = referenceSprite.GetPixels();
        List<Pixel> createdPixels = new();
        int width = referenceSprite.width;
        int height = referenceSprite.height;
        
        for (int i = 0; i < pixelColors.Length; i++)
        {
            if(pixelColors[i].a == 0) continue;
            
            createdPixels.Add(GeneratePixel(pixelColors[i], i, width, height, targetShape));
            action?.Invoke((float)(i+1)/pixelColors.Length);
            
        }
        
        targetShape.Initialize(createdPixels);
        shapeTransform.localScale = startScale;
    }

    private Pixel GeneratePixel(Color color, int position, int imageWidth, int imageHeight, DestructiveShape shape)
    {
        Vector2 gridPosition = new Vector2(position % imageWidth - imageWidth/2f + .5f, position / imageWidth - imageHeight/2f + .5f);
        // Debug.Log(gridPosition);
        var parentTransform = shape.transform;
#if UNITY_EDITOR
        Pixel pixel = PrefabUtility.InstantiatePrefab(pixelPrefab) as Pixel;
#endif
#if !UNITY_EDITOR
        Pixel pixel = Instantiate(pixelPrefab);
#endif
        var pixelTransform = pixel.transform;
        pixelTransform.position = parentTransform.position + (Vector3) (gridPosition * pixelsSpacing);
        pixelTransform.parent = parentTransform;
        Material material = GetMaterialFromAvailable(color);
        pixel.Initialize(shape, material);
        
        
        return pixel;
    }

    private Material GetMaterialFromAvailable(Color color)
    {
        Material resultMaterial = _availableMaterials.Find(m => m.color == color);

        if (resultMaterial == null)
        {
            resultMaterial = new Material(materialPrefab)
            {
                color = color
            };
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(resultMaterial, "Assets/Materials/Resources/" + materialsStoreDirectory + "M_"+ _availableMaterials.Count + ".mat");
#endif
            _availableMaterials.Add(resultMaterial);
        }

        return resultMaterial;
    }

    private void GetMaterialsFromDirectory(string directory)
    {
        _availableMaterials = new List<Material>();
        _availableMaterials = Resources.LoadAll<Material>(directory).ToList();
        Debug.Log("Materials loaded: "+ _availableMaterials.Count, this);
    }
}
