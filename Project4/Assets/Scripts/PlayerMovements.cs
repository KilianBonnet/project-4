using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private const float GROUND_CHECK_MARGIN = .1f;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 16f;

    [Header("Noise")]
    [ColorUsage(showAlpha: true, hdr: true)]
    [SerializeField] private Color scannerColor;

    [SerializeField] private float noiseDelta = 2f;
    [SerializeField] private GameObject scannerPrefab;
    [SerializeField] private float noiseDistance = 3f;

    private float groundCheckDistance;
    private float lastNoise;
    private Rigidbody rb;

    private void Start()
    {
        groundCheckDistance = GetComponent<CapsuleCollider>().height / 2 + GROUND_CHECK_MARGIN;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0f);

        if (CheckGrounded() && Input.GetButtonDown("Jump"))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        lastNoise += Time.deltaTime;
        if (lastNoise > noiseDelta)
        {
            lastNoise = 0;

            Vector3 footPosition = transform.position + Vector3.down * groundCheckDistance;
            GameObject scannerGameObject = Instantiate(
                scannerPrefab,
                footPosition,
                transform.rotation
            );

            Scanner scanner = scannerGameObject.GetComponent<Scanner>();
            scanner.ScannerColor = scannerColor;
            scanner.ScannerDistance = noiseDistance;
        }
    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance)
         && hit.collider.CompareTag("Ground");
    }
}
