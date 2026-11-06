using System.Collections;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private Vector3 targetPosition = Vector3.zero;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start() 
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin() 
    {
        
    }
    private IEnumerator BeginPour()
    {
        yield return null;
    }
    private Vector3 FindEndPoint() 
    {
        return Vector3.zero;
    }
    private void MoveToPosition(int index, Vector3 targetPosition) 
    {
        lineRenderer.SetPosition(index, targetPosition);
    }
}
