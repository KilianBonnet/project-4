using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScannerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject scannerPrefab;
    [SerializeField] private List<Transform> scannersAnchors;

    [SerializeField]
    [Range(0, 100)]
    private float noiseFrequency = .1f;

    private void Update()
    {
        if (scannersAnchors.Count == 0)
            return;

        if (Random.Range(0, 101) >= noiseFrequency)
            return;

        Transform anchorToTrigger = scannersAnchors[Random.Range(0, scannersAnchors.Count)];
        GenerateTreeScanner(anchorToTrigger);
    }

    private void GenerateTreeScanner(Transform anchor)
    {
        GameObject scannerGameObject = Instantiate(
            scannerPrefab,
            anchor.position,
            anchor.rotation,
            anchor
        );

        Scanner scanner = scannerGameObject.GetComponent<Scanner>();
        scanner.ScannerColor = Color.green;
        scanner.ScannerDistance = Random.Range(2f, 4f);
    }
}
