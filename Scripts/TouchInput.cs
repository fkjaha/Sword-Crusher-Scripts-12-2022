using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public static TouchInput Instance;

    public Vector2 GetInput => _input;
    public TouchPhase GetLastTouchPhase => _lastTouchPhase;

    private Vector2 _input;
    private TouchPhase _lastTouchPhase;
    
    private Vector2 _lastInputPosition;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _lastInputPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    _input = touch.position - _lastInputPosition;
                    _lastInputPosition = touch.position;
                    break;
                case TouchPhase.Ended: case TouchPhase.Canceled: case TouchPhase.Stationary:
                    _input = Vector2.zero;
                    break;
            }

            _lastTouchPhase = touch.phase;
        }
        #if UNITY_EDITOR
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _lastInputPosition = Input.mousePosition;
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                _input = (Vector2)Input.mousePosition - _lastInputPosition;
                _lastInputPosition = Input.mousePosition;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _input = Vector2.zero;
            }
        }
        #endif
    }
}
