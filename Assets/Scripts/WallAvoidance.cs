using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WallAvoidance : MonoBehaviour
{
    List<GameObject> walls = new List<GameObject>();
    GameObject boid;
    public bool drawRays = false;
    public float forceMultiplier = 1f;

    void Start()
    {
        boid = gameObject.transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        boid.GetComponent<Rigidbody>().AddForce(GetAvoidanceForce());
    }

    //trigger enter/exits are how I track boids in the neighborhood
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WorldObj")
        {
            if (!walls.Contains(other.gameObject))
            { walls.Add(other.gameObject); }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WorldObj")
        {
            if (walls.Contains(other.gameObject))
            { walls.Remove(other.gameObject); }
        }
    }

    Vector3 GetAvoidanceForce()
    {
        //a force that if neighbor(s) enter the radius, moves the boid away from it/them
        Vector3 diffVector = Vector3.zero;
        Vector3 accVector = Vector3.zero;
        Vector3 avoidingForce = Vector3.zero;
        
        if (walls.Count != 0)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                diffVector = gameObject.transform.position - walls[i].GetComponent<BoxCollider>().ClosestPoint(transform.position);
                accVector = (diffVector.normalized / diffVector.magnitude);
                avoidingForce += accVector;
            }
            avoidingForce *= forceMultiplier;
        }
        if (drawRays)
        { Debug.DrawRay(transform.position, avoidingForce, Color.yellow); }
        return avoidingForce;
    }
}
