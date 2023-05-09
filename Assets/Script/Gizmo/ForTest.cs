using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTest : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public GameObject startPoint, endPoint;
    public GameObject circleOrigin;
    public bool drawLook, drawCircle, drawCircleRadius, drawTrail, drawPolarCoordinateTest, drawGrid;
    public float radius;
    public float rotation;
    private Vector3 circleEndpoint, cStartPoint;

    // numSegments = number of segments to draw
    // angleStep = angle between adjacent lines (in degrees)
    // angle = current angle
    // angleRad = current angle in radians
    public int numSegments; 
    private float angleStep, angle = 0f, angleRad = 0f; 
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if(drawLook) DrawLook();
        if(drawCircle && radius != 0) DrawCircle();
        if(drawCircleRadius && radius != 0) DrawCircleRadius();    
        if(drawTrail) DrawTrail();
        if(drawPolarCoordinateTest) DrawPolarCoordinateTest();
        if(drawGrid) DrawGrid();
    
    }

    private void DrawLook()
    {
        Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
        Gizmos.DrawLine(startPoint.transform.position, new Vector3(startPoint.transform.position.x, endPoint.transform.position.y, 0));
        Gizmos.DrawLine(endPoint.transform.position, new Vector3(startPoint.transform.position.x, endPoint.transform.position.y, 0));  
    }
    private void DrawCircle()
    {
        angleStep = 360f / numSegments;
        for (float i = numSegments; i > 0; i--)
        {
            angle = (i) * angleStep + rotation;
            angleRad = angle * Mathf.Deg2Rad;
            circleEndpoint = new Vector3(circleOrigin.transform.position.x + radius * Mathf.Cos(angleRad),
                                        circleOrigin.transform.position.y + radius * Mathf.Sin(angleRad),
                                        0f);

            angle = (i - 1) * angleStep + rotation;
            angleRad = angle * Mathf.Deg2Rad;
            cStartPoint = new Vector3(circleOrigin.transform.position.x + radius * Mathf.Cos(angleRad),
                                        circleOrigin.transform.position.y + radius * Mathf.Sin(angleRad),
                                        0f);

            Gizmos.DrawLine(cStartPoint, circleEndpoint);
        }
    }
    private void DrawCircleRadius()
    {
        angleStep = 360f / numSegments;
        for (float i = 0; i < numSegments; i++)
        {   
            angle = i * angleStep + rotation; // 240
            angleRad = angle * Mathf.Deg2Rad; // 240 * pi/180
            circleEndpoint = new Vector3(circleOrigin.transform.position.x + radius * Mathf.Cos(angleRad),
                                        circleOrigin.transform.position.y + radius * Mathf.Sin(angleRad),
                                        0f);
            Gizmos.DrawLine(circleOrigin.transform.position, circleEndpoint);
            //Gizmos.DrawLine(circleOrigin, -circleEndpoint);
        }
    }

    #region DrawTrail
    [Header("Draw Trail")]
    public GameObject trailOrigin;
    public float trailLength;
    private Vector3 trailTop, trailBottom;
    private void DrawTrail()
    {
        trailTop = trailOrigin.transform.position + (trailOrigin.transform.up * (trailOrigin.transform.localScale.y/2f));
        trailBottom = trailOrigin.transform.position - (trailOrigin.transform.up * (trailOrigin.transform.localScale.y/2f));

        Gizmos.DrawLine(trailTop, trailTop - (trailOrigin.transform.right * trailLength)); //top
        Gizmos.DrawLine(trailOrigin.transform.position, trailOrigin.transform.position - (trailOrigin.transform.right * trailLength)); //center
        Gizmos.DrawLine(trailBottom, trailBottom - (trailOrigin.transform.right * trailLength)); //bottom
    }
    #endregion

    #region DrawPolarCoordinateTest
    [Header("Draw Polar Coordinate Test")]
    public GameObject centerPoint;
    public List<float> theta;
    public float polarCoordinateRadius;
    private void DrawPolarCoordinateTest()
    {
        //Debug.Log($"Theta: {theta} -- CenterZ: {centerPoint.transform.localEulerAngles.z} -- new Theta: {theta + centerPoint.transform.localEulerAngles.z}");
        
        foreach(float t in theta)
        {
            Vector3 polarEndPoint = new Vector3 (polarCoordinateRadius * Mathf.Cos(((t + centerPoint.transform.localEulerAngles.z) * Mathf.Deg2Rad)) + centerPoint.transform.position.x,
                                    polarCoordinateRadius * Mathf.Sin(((t + centerPoint.transform.localEulerAngles.z) * Mathf.Deg2Rad)) + centerPoint.transform.position.y, 0);
            Gizmos.DrawLine(centerPoint.transform.position, polarEndPoint);
        }
    }
    #endregion

    #region DrawGrid
    [Header("Draw Grid")]
    public float gridSize, gridGap, numberOfLines;
    private float sPoint;
    private void DrawGrid()
    {
        sPoint = 0;
        for(int i = 0; i < numberOfLines; i++)
        {
            Gizmos.DrawLine(new Vector3(sPoint,0,0), new Vector3(sPoint, gridSize, 0));
            Gizmos.DrawLine(new Vector3(sPoint,0,0), new Vector3(sPoint, -gridSize, 0));
            Gizmos.DrawLine(new Vector3(0,sPoint,0), new Vector3(gridSize, sPoint, 0));
            Gizmos.DrawLine(new Vector3(0,sPoint,0), new Vector3(-gridSize, sPoint, 0));

            Gizmos.DrawLine(new Vector3(-sPoint,0,0), new Vector3(-sPoint, gridSize, 0));
            Gizmos.DrawLine(new Vector3(-sPoint,0,0), new Vector3(-sPoint, -gridSize, 0));
            Gizmos.DrawLine(new Vector3(0,-sPoint,0), new Vector3(gridSize, -sPoint, 0));
            Gizmos.DrawLine(new Vector3(0,-sPoint,0), new Vector3(-gridSize, -sPoint, 0));

            sPoint += (gridGap);
        }
    }
    #endregion
}
