using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerContrller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameEnded)
        {
            return;
        }

        moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
            {
                moveInput.y = 1f;
            }

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
            {
                moveInput.y = -1f;
            }

            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput.x = -1f;
            }

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput.x = 1f;
            }
        }

        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(
            rb.position +
            moveInput * moveSpeed * Time.fixedDeltaTime
        );
    }
}
