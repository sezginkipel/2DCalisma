using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    public float runSpeedMultiplier = 1.5f; // Koşma hızı çarpanı

    private Rigidbody2D _rb;
    private PlayerStats _playerStats;
    private Vector2 _moveInput;

    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // --- Input --- 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector2(moveX, moveY).normalized;

        IsWalking = _moveInput.magnitude > 0.1f;
        IsRunning = Input.GetKey(KeyCode.LeftShift);

        // --- Sprite Flipping ---
        if (moveX > 0.1f)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        else if (moveX < -0.1f)
        {
            transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        }
    }

    private void FixedUpdate()
    {
        // --- Movement ---
        // Hızı PlayerStats'tan al
        float baseSpeed = _playerStats.MoveSpeed;
        float currentSpeed = IsRunning ? baseSpeed * runSpeedMultiplier : baseSpeed;
        _rb.linearVelocity = _moveInput * currentSpeed;
    }
}
