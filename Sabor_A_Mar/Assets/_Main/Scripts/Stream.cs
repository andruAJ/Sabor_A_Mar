using System.Collections;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticles;

    private Coroutine pourRoutine = null;
    private Vector3 targetPosition = Vector3.zero;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticles = GetComponentInChildren<ParticleSystem>();
    }
    private void Start() 
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin() 
    {
        StartCoroutine(BeginPour());
        pourRoutine = StartCoroutine(UpdateParticle());
    }
    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }
        yield return null;
    }
    public void End() 
    {
        if (pourRoutine != null)
        {
            StopCoroutine(pourRoutine);
            pourRoutine = StartCoroutine(EndPour());
        }
    }

    private IEnumerator EndPour() 
    {
        while (!HasReachedPosition(0, targetPosition)) 
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
        
        Destroy(gameObject);
    }
    private Vector3 FindEndPoint() 
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 5f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(5f);
        return endPoint;
    }
    private void MoveToPosition(int index, Vector3 targetPosition) 
    {
        lineRenderer.SetPosition(index, targetPosition);
    }
    private void AnimateToPosition(int index, Vector3 targetPosition) 
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f); //en caso de querer cambiar la velocidad, le cambió el 1.75 por otro valor
        lineRenderer.SetPosition(index, newPosition);
    }
    private bool HasReachedPosition(int index, Vector3 targetPosition) 
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);

        return currentPosition == targetPosition;
    }
    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticles.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticles.gameObject.SetActive(isHitting);
            yield return null;
        }
    }
}
