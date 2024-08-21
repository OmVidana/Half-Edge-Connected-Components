using UnityEngine;

/// <summary>
/// Controls the movement and rotation of the player's camera.
/// </summary>
public class Movement : MonoBehaviour
{
    [Range(1f, 64f)] public float movementSpeed;
    [Range(1f, 32f)] public float movementBoost;
    [Range(1f, 48f)] public float upSpeed;
    [Range(1f, 48f)] public float downSpeed;
    [Range(1f, 24f)] public float horizontalLook;
    [Range(1f, 24f)] public float verticalLook;
    
    public Vector3 minBounds;
    public Vector3 maxBounds;
    
    public CCEvents ccEvents;

    private Vector3 _currentPos;
    private float _currentSpeed;
    private float _rotationX;
    private float _rotationY;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ccEvents.IsUIActive = !ccEvents.IsUIActive;
            if (ccEvents.IsUIActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (ccEvents.IsUIActive) return;
        
        _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed + movementBoost : movementSpeed;

        _currentPos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            _currentPos += (transform.forward * _currentSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _currentPos -= (transform.forward * _currentSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _currentPos -= (transform.right * _currentSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _currentPos += (transform.right * _currentSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _currentPos += (transform.up * upSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentPos -= (transform.up * downSpeed) * Time.deltaTime;
        }

        _currentPos.x = Mathf.Clamp(_currentPos.x, minBounds.x, maxBounds.x);
        _currentPos.y = Mathf.Clamp(_currentPos.y, minBounds.y, maxBounds.y);
        _currentPos.z = Mathf.Clamp(_currentPos.z, minBounds.z, maxBounds.z);

        transform.position = _currentPos;
        
        float mouseX = Input.GetAxis("Mouse X") * horizontalLook;
        float mouseY = -Input.GetAxis("Mouse Y") * verticalLook;
        
        _rotationX += mouseX;
        _rotationY += mouseY;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        
        transform.localEulerAngles = new Vector3(_rotationY, _rotationX, 0);
    }
}
