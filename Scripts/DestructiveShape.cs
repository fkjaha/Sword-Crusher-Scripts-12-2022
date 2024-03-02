using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructiveShape : MonoBehaviour
{
    public List<Pixel> GetPixels => pixels;
    public int GetDestructionProgress => (int)(100 - (float)pixels.Count/_originalPixelsCount * 100);

    public event UnityAction OnDamaged;
    
    [SerializeField] private List<Pixel> pixels;

    private int _originalPixelsCount;

    public void Initialize(List<Pixel> newPixels)
    {
        pixels = newPixels;
        _originalPixelsCount = pixels.Count;
    }

    public void ReleasePixel(Pixel pixel)
    {
        pixels.Remove(pixel);
        OnDamaged?.Invoke();
    }

    public void ResetShape()
    {
        foreach (Pixel pixel in pixels)
        {
            DestroyImmediate(pixel.gameObject);
        }
        pixels = new List<Pixel>();
    }
}
