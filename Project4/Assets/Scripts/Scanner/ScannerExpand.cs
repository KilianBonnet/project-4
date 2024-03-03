using UnityEngine;

public class ScannerExpand : MonoBehaviour
{
    [ColorUsage(showAlpha: true, hdr: true)]
    public Color ScannerColor;

    public float ScannerDistance = 1;

    private const float EXPAND_SPEED = 2.0f;
    private Material scannerMaterial;
    private float currentDistance;


    private void Start()
    {
        scannerMaterial = GetComponent<MeshRenderer>().material;
        scannerMaterial.color = ScannerColor;

        currentDistance = 0;
        transform.localScale = Vector3.zero;
        UpdateScannerAlpha(1);
    }

    private void Update()
    {
        currentDistance += Time.deltaTime * EXPAND_SPEED;
        if (currentDistance > ScannerDistance)
        {
            Destroy(gameObject);
            return;
        }

        transform.localScale = Vector3.one * currentDistance;
        float alpha = 1 - (currentDistance / ScannerDistance);
        UpdateScannerAlpha(alpha);
    }


    public void UpdateScannerAlpha(float alpha)
    {
        Color scannerColor = scannerMaterial.color;
        scannerColor.a = alpha;
        scannerMaterial.color = scannerColor;
    }
}
