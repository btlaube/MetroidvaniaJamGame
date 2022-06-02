using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderTest : MonoBehaviour
{

    public float length;
    public EdgeCollider2D edgeCollider;
    public EdgeCollider2D edgeTrigger;
    public Vector2[] points;

    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        edgeTrigger.isTrigger = true;
        edgeCollider.points = this.points;
        edgeTrigger.points = this.points;
        lineRenderer.positionCount = this.points.Length;
        for(int i = 0; i < this.points.Length; i++) {
            lineRenderer.SetPosition(i, this.points[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        edgeCollider.points = this.points;
        edgeTrigger.points = this.points;
        lineRenderer.positionCount = this.points.Length;
        for(int i = 0; i < this.points.Length; i++) {
            lineRenderer.SetPosition(i, this.points[i]);
        }
    }
}
