using System;
using DG.Tweening;
using UnityEngine;

public class BladeCompresor : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody relatedRigidbody;
    [SerializeField] private AutoJumpDetector jumpDetector;
    [SerializeField] private Vector3 compressTargetSize;
    [SerializeField] private float compressionTime;
    [SerializeField] private Ease compressEase;

    private bool _compressing;
    private Vector3 _startTargetSize;
    

    private void Start()
    {
        _startTargetSize = targetTransform.localScale;
        jumpDetector.onJumpDetected += Compress;
    }

    private void Update()
    {
        // if (Mathf.Abs(relatedRigidbody.velocity.y) < .1f)
        // {
        //     Compress();
        // }
    }

    private void Compress()
    {
        if(_compressing) return;
        targetTransform.DOComplete(true);
        _compressing = true;
        targetTransform.DOScale(compressTargetSize, compressionTime / 2).SetEase(compressEase).onComplete += () =>
            targetTransform.DOScale(_startTargetSize, compressionTime / 2).SetEase(compressEase).onComplete += () =>
                _compressing = false;
    }
}
