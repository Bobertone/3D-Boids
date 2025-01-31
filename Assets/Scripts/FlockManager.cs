using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class FlockManager : MonoBehaviour
{
    [SerializeField] List<GameObject> flock = new List<GameObject>();
    [SerializeField] int ruleNum;
    GameObject boid;
    public bool drawRays = true;
    public float forceMultiplier = 1f;

    void Start()
    {
        boid = gameObject.transform.parent.gameObject;
    }

    void FixedUpdate()
    {
        switch (ruleNum)
        {
            case 1: //Cohesion
                boid.GetComponent<Rigidbody>().AddForce(GetCohesionForce());
                break;
            case 2: //Separation
                boid.GetComponent<Rigidbody>().AddForce(GetSeparationForce());
                break;
            case 3: //Alignment
                boid.GetComponent<Rigidbody>().AddForce(GetAlignmentForce());
                break;
            default:
                break;
        }
    }

    //trigger enter/exits are how I track boids in the neighborhood
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish") 
        {
            if (!flock.Contains(other.gameObject)) 
            { flock.Add(other.gameObject); }
        }
    }
    private void OnTriggerExit(Collider other)
    { 
        if (other.gameObject.tag == "Fish")
        {
            if (flock.Contains(other.gameObject))
            { flock.Remove(other.gameObject); }
        }
    }

    Vector3 GetCohesionForce() 
    { // force that brings boids together towards average position
        Vector3 cohesionForce = Vector3.zero;
        if (flock.Count != 0)
        {
            Vector3 avgPos = Vector3.zero;

            for (int i = 0; i < flock.Count; i++)
            {
                avgPos += flock[i].transform.position;
            }
            // find center of mass
            avgPos = avgPos / flock.Count;
            Vector3 directionVector = avgPos - gameObject.transform.position;
            cohesionForce = Vector3.Normalize(directionVector) * forceMultiplier;
            if (drawRays)
            { Debug.DrawRay(transform.position, cohesionForce, Color.green); }
        }
        return cohesionForce;
    }

    Vector3 GetSeparationForce()
    { //a force that if neighbor(s) enter the radius, moves the boid away from it/them
        Vector3 diffVector = Vector3.zero;
        Vector3 accVector = Vector3.zero;
        Vector3 separatingForce = Vector3.zero;
        
        if (flock.Count != 0)
        {
            for (int i = 0; i < flock.Count; i++)
            {
                diffVector = gameObject.transform.position - flock[i].transform.position;
                accVector = (diffVector.normalized / diffVector.magnitude);
                separatingForce += accVector;
            }
            separatingForce *= forceMultiplier;
        }
        if (drawRays)
        { Debug.DrawRay(transform.position, separatingForce, Color.red); }
        return separatingForce;
    }

    Vector3 GetAlignmentForce()
    { // Try to match the heading of neighbors = Average velocity
        Vector3 alignmentForce = Vector3.zero;
        Vector3 averageVelocity = Vector3.zero;
        if (flock.Count != 0)
        {
            for (int i = 0; i < flock.Count; i++)
            {
                averageVelocity += flock[i].GetComponent<Rigidbody>().velocity;
            }
            averageVelocity = averageVelocity / flock.Count;
        }
        alignmentForce =  Vector3.Normalize(averageVelocity) * forceMultiplier;
        if (drawRays) { Debug.DrawRay(transform.position, alignmentForce, Color.blue); }
        return alignmentForce;
    }

    
}
