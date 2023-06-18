using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("Throw property")]
    [SerializeField] [Range(0.1f, 1.5f)] private float throwPower = 1f;
    [SerializeField] private Vector2 maxDistance;
    private Vector2 distanceThrow;

    [Header("Change the curve of the line")]
    [SerializeField] private float gravityMutiple = 3;

    [Header("Change the velocity throw")]
    [SerializeField] private float velocityDiv = 1.75f;

    public FixedJoint2D magnetJoin;
    private Rigidbody2D magnetBody;
    private LineRenderer lineShow;
    Vector2 DragStartPos;

    private void Start()
    {
        magnetBody = GetComponent<Rigidbody2D>();
        lineShow = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //Khi nhan chuot luu lai vi tri dau tien cua chuot
        if (Input.GetMouseButtonDown(0))
        {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distanceThrow = DragEndPos - DragStartPos;
            if (distanceThrow.x > maxDistance.x)
            {
                distanceThrow.x = maxDistance.x;
            }
            if (distanceThrow.x < -maxDistance.x)
            {
                distanceThrow.x = -maxDistance.x;
            }
            if (distanceThrow.y > maxDistance.y)
            {
                distanceThrow.y = maxDistance.y;
            }
            Vector2 velocityThrow = (distanceThrow) * throwPower;

            Vector2[] trajectory = PlotPoint(magnetBody, (Vector2)transform.position, velocityThrow, 300);
            lineShow.positionCount = trajectory.Length;
            Vector3[] position = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                position[i] = trajectory[i];
            }
            lineShow.SetPositions(position);
        }

        //Khi tha chuot, luu lai vi tri cuoi va tinh toan vector van toc
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocityThrow = (distanceThrow) * throwPower / velocityDiv;

            //Loai bo join cua Magnet va khoa trajectory 
            magnetJoin.enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            gameObject.GetComponent<Rigidbody2D>().mass = 3;
            magnetBody.velocity = velocityThrow;
            gameObject.GetComponent<TrajectoryLine>().enabled = false;
            gameObject.GetComponent<LineRenderer>().positionCount = 0;
        }
    }

    // Tinh toan duong quy dao nem voi tham so : Rigid cua nam cham, vi tri ban dau, vector nem, so buoc nhay
    public Vector2[] PlotPoint(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep * gravityMutiple;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for(int i = 0;i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }
}
