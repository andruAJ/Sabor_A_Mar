using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourTreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    private void Update()
    {
        if (origin == null || streamPrefab == null)
            return;

        bool pourDetected = CalculatePourAngle() < pourTreshold;

        if (isPouring != pourDetected)
        {
            isPouring = pourDetected;
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }    
    }
    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }
    private void EndPour()
    {
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
