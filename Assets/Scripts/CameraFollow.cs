using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // minimal border - x, maximal - y
    [SerializeField]
    private Vector2 xBorder, yBorder, zBorder;
    [SerializeField]
    private float heightMovingSpeed = 5000f;

    // need to set X and Z borders according to yBorder (can be set only in inspector window)
    //public Vector2 Xborder { get => Xborder;set=>{ } }

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
            UpdatePosition(transform.position);
        }
    }

    // updating only X and Z coords!
    public void UpdatePosition(Vector3 newPosition)
    {
        transform.position = new Vector3(
            newPosition.x,
            Mathf.Clamp(transform.position.y, yBorder.x, yBorder.y),
            newPosition.z
            );
    }
}
