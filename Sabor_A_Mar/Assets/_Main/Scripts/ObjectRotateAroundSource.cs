using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.Animations;

public class ObjectRotateAroundSource : MonoBehaviour
{
    public Transform source;
    private float delta;
   
    void Start()
    {
        transform.rotation.x = source.rotation.x;
    }

    void Update()
    {
        if (transform.rotation.x != source.rotation.x) 
        {
            delta = source.rotation.x - transform.rotation.x;
            Rotate(delta);
        }
    }
    public void Rotate(float delta) 
    {
        this.transform.RotateAround(source.position, Vector3.up, delta);
    }

}
