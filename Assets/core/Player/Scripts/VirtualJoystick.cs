using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform _joystickBackground;
    private RectTransform _joystickHandle;
    private Vector2 _inputVector;

    private void Start()
    {
        _joystickBackground = GetComponent<RectTransform>();
        _joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_joystickBackground, eventData.position, eventData.pressEventCamera, out position);
        
        position.x = position.x / _joystickBackground.sizeDelta.x;
        position.y = position.y / _joystickBackground.sizeDelta.y;
        
        _inputVector = new Vector2(position.x * 2, position.y * 2);
        _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
        
        _joystickHandle.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.sizeDelta.y / 2));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return _inputVector.x;
    }

    public float Vertical()
    {
        return _inputVector.y;
    }
}