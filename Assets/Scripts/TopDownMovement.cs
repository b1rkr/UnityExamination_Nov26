using UnityEngine;
using UnityEngine.InputSystem;

/////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D and a CapsuleCollider2D component in the inspector.
// The following components are needed: Player Input

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class TopDownMovement : MonoBehaviour
{
    public float maxSpeed = 7;
    [SerializeField] GameObject playerSpriteParentObject;
    [SerializeField] Animator animator;

    public bool controlEnabled { get; set; } = true;    // You can edit this variable from Unity Events

    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set gravity scale to 0 so player won't "fall" 
        rb.gravityScale = 0;

    }

    private void Update()
    {
        if (rb.linearVelocityX < 0)
            playerSpriteParentObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        if (rb.linearVelocityX > 0)
        {
            playerSpriteParentObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        animator.SetFloat("VelocityX", rb.linearVelocityX);
        animator.SetFloat("VelocityY", rb.linearVelocityY);
    }

    private void FixedUpdate()
    {
        // Set velocity based on direction of input and maxSpeed
        if (controlEnabled)
        {
            rb.linearVelocity = moveInput.normalized * maxSpeed;
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Write code for walking animation here. (Suggestion: send your current velocity into the Animator for both the x- and y-axis.)
    }

    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack");
        }
    }
}