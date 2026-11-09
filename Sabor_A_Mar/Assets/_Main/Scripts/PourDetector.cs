using UnityEngine;

public class PourDetector : MonoBehaviour
{
    //public int pourTreshold = 45;

    [SerializeField] private float startPourThreshold = 45f;
    [SerializeField] private float stopPourThreshold = 179f;

    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;

    private bool isEnding = false;
    private Stream currentStream = null;

    private void Update()
    {
        if (origin == null || streamPrefab == null)
            return;

        bool pourDetected = CalculatePourAngle() < startPourThreshold;

        if (isPouring != pourDetected)
        {
            isPouring = pourDetected;
            if (isPouring)
            {
                Debug.LogWarning("Inicio de vertido");
                StartPour();
            }
            else if (CalculatePourAngle() > stopPourThreshold)
            {
                Debug.LogWarning("Fin de vertido");
                isPouring = false;
                EndPour();
            }
        }    
    }
    private void StartPour()
    {
        if (currentStream != null || isEnding)
            return;

        currentStream = CreateStream();
        currentStream.Begin();
    }
    private void EndPour()
    {
        if (currentStream == null)
            return;

        isEnding = true;
        currentStream.End();
        currentStream = null;
    }
    private float CalculatePourAngle()
    {
        if (origin == null)
            return 0f;
        return Vector3.Angle(origin.up, Vector3.down); //revisar que el ángulo sí este bueno
    }
    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        Stream stream = streamObject.GetComponent<Stream>();
        return stream;
    }
}
