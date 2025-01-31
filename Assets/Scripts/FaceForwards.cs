using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceForwards : MonoBehaviour
{
    float minSpeed = .01f;
    float maxSpeed = 8f;
    void Update()
    {
        //clamp magnitude of boid speed
        Vector3 clampedVector = Vector3.ClampMagnitude(
            gameObject.GetComponent<Rigidbody>().velocity,
            Mathf.Clamp(gameObject.GetComponent<Rigidbody>().velocity.magnitude,
            minSpeed, maxSpeed));
        gameObject.GetComponent<Rigidbody>().velocity = clampedVector;
        
        //make a boid face forward
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity.normalized;
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}
