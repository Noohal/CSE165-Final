using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicLineRenderer : MonoBehaviour
{
    public Transform camera;
    
    public Transform startPoint;
    public Vector3 endPoint = new Vector3(10, 0, 0);
    
    public int numPoints = 50;
    public float height = 5f;

    private LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawParabola();
    }

    void DrawParabola()
    {
        Vector3 start = startPoint.position;
        Vector3 end = start + camera.forward * 2f;
        
        Vector3[] points = new Vector3[numPoints];
        
        float step = 1.0f / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            float t = i * step;
            points[i] = CalculateParabolaPoint(start, end, t);
        }

        line.positionCount = numPoints;
        line.SetPositions(points);
    }

    Vector3 CalculateParabolaPoint(Vector3 start, Vector3 end, float t)
    {
        float parabola = 4 * height * t * (1 - t);

        Vector3 mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, mid.y + parabola, mid.z);
    }
}
