using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform handParent;
    [SerializeField] private Image handImage;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private UnityEvent onPressed;

    private void Update()
    {
        handParent.position = Input.mousePosition;
        
        if(Input.GetKeyDown(KeyCode.Mouse0)) onPressed.Invoke();
        handImage.sprite = Input.GetKey(KeyCode.Mouse0) ? pressedSprite : idleSprite;
    }
}
