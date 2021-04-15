using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FloorTrail : MonoBehaviour
{
    public Vector3 offset;
    public float minDelta;
    public LineRenderer lineRenderer;
    public bool drawOnStart = false;
    private Vector3 _prevPos;
    private bool _drawing = false;
    
    
    
    void Awake() {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        _prevPos = transform.position;
        Draw(false);
        Clear();
        
    }

    void Start() {
        Draw(drawOnStart);
    }
    
    
    // Update is called once per frame
    void Update() {
        if (_drawing && (_prevPos - transform.position).sqrMagnitude > (minDelta * minDelta)) {
            _prevPos = transform.position;
            lineRenderer.SetPosition(lineRenderer.positionCount++, _prevPos + offset);
        }
    }

    /// <summary>
    /// clear trail
    /// </summary>
    public void Clear() {
        lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// start or stop drawing
    /// </summary>
    /// <param name="startDrawing"></param>
    public void Draw(bool startDrawing) {
        if (lineRenderer != null) {
            _drawing = startDrawing;
        }
    }
    
}
