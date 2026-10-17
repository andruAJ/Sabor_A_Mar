using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.Animations;

public class ObjectFollowsSource : MonoBehaviour
{
    public Transform leader;
    public Transform follower;
    public float speed = 5f;
    public float distanceThreshold = 2f;
    private Vector3 distance;


    void Update()
    {
        distance = leader.position - follower.position;
        if (distance.magnitude > distanceThreshold)
        {
            follower.transform.position = Vector3.MoveTowards(follower.transform.position, leader.position, Time.deltaTime * speed);
        }
    }


}
