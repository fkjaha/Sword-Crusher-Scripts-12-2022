using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PositionRandomizer : MonoBehaviour
{
    public static PositionRandomizer Instance;
    
    [Header("Test")]
    [SerializeField] private Vector3 minDeltaTest;
    [SerializeField] private Vector3 maxDeltaTest;

    private void Awake()
    {
        Instance = this;
    }

#if UNITY_EDITOR
    [ContextMenu("Test_Randomize Selected")]
    public void RandomizeSelectedShape()
    {
        if (Selection.activeTransform.TryGetComponent(out DestructiveShape shape))
        {
            RandomizeObjectsLocalPosition(shape.GetPixels.Select(p => p.transform).ToList(), minDeltaTest, maxDeltaTest);
        }
    }
#endif
    
    public void RandomizeShapePixels(DestructiveShape shape)
    {
        RandomizeObjectsLocalPosition(shape.GetPixels.Select(p => p.transform).ToList(), minDeltaTest, maxDeltaTest);
    }
    
    public static void RandomizeObjectLocalPosition(Transform objectTransform, Vector3 minDelta, Vector3 maxDelta)
    {
        objectTransform.localPosition += new Vector3(
            Random.Range(minDelta.x, maxDelta.x),
            Random.Range(minDelta.y, maxDelta.y),
            Random.Range(minDelta.z, maxDelta.z));
    }

    public static void RandomizeObjectsLocalPosition(List<Transform> objectTransforms, Vector3 minDelta, Vector3 maxDelta)
    {
        foreach (Transform objectTransform in objectTransforms)
        {
            RandomizeObjectLocalPosition(objectTransform, minDelta, maxDelta);
        }
    }
}
