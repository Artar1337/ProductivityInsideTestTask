using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // minimal border - x, maximal - y
    [SerializeField]
    // yBorder - heigth border, physical - outer X, Z walls border for camera (for max yBorder)
    private Vector2 yBorder, physicalBorder;
    private Vector2 xBorder, zBorder;
    private new Camera camera;
    private float resolutionProportion;

    [SerializeField]
    private float heightMovingSpeed = 5000f;

    // need to set X and Z borders according to yBorder (can be set only in inspector window)
    // and physical field border, represented by physicalBorder
    //public Vector2 Xborder { get => Xborder;set=>{ } }

    private void Start()
    {
        camera = GetComponent<Camera>();
        resolutionProportion = physicalBorder.x / physicalBorder.y;
        UpdateBorders();
        UpdatePosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // only mouse scroll can update Y coord
        float input = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(input) > 0.001f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + input * heightMovingSpeed * Time.deltaTime,
                transform.position.z);
            UpdateBorders();
            UpdatePosition(transform.position);
        }
    }

    private void UpdateBorders()
    {
        // y coord does not matter for our viewport
        // center of viewport
        Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0.5f));
        // left down of viewport (max x and z, centered to a actual world center)
        Vector3 maxCoords = camera.ViewportToWorldPoint(new Vector3(1f, 0, 1f)) - center;
        float x = Mathf.Abs(physicalBorder.x - Mathf.Abs(maxCoords.x * transform.position.y));
        // z depends on y (16 by 9 dy default)
        float z = x / resolutionProportion;
        xBorder = new Vector2(-x, x);
        zBorder = new Vector2(-z, z);
    }

    // updating only X and Z coords!
    public void UpdatePosition(Vector3 newPosition)
    {
        transform.position = new Vector3(
            Mathf.Clamp(newPosition.x, xBorder.x, xBorder.y),
            Mathf.Clamp(transform.position.y, yBorder.x, yBorder.y),
            Mathf.Clamp(newPosition.z, zBorder.x, zBorder.y)
            );
    }
}
