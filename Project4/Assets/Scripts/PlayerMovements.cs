using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private const float GROUND_CHECK_MARGIN = .1f;

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 16f;

    private float groundCheckDistance;
    private Rigidbody rb;

    private bool isJumping;
    private float horizontal;

    private void Start()
    {
        groundCheckDistance = GetComponent<CapsuleCollider>().height / 2 + GROUND_CHECK_MARGIN;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, 0f);

        if (CheckGrounded() && Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance)
         && hit.collider.CompareTag("Ground");
    }
}
