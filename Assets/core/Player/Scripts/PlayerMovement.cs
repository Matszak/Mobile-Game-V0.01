using core.Scripts.enemy_ai;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    
    public float speed = 5f;
    public VirtualJoystick moveJoystick;
    public VirtualJoystick aimJoystick;
    
    private Rigidbody2D _rb;
    private PlayerData _playerData;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerData = GetComponent<PlayerData>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(moveJoystick.Horizontal(), moveJoystick.Vertical());
        
        float deadZone = 0.2f;  // Martwa strefa joysticka :(
        
        if (movement.magnitude > deadZone)
        {
            _rb.velocity = movement * speed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

        Vector2 aimDirection = new Vector2(aimJoystick.Horizontal(), aimJoystick.Vertical());
        if (aimDirection.magnitude > deadZone)
        {
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            _playerData.TryShoot(aimDirection);
        }

    }
}