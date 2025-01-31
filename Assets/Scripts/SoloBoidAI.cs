using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoloBoidAI : MonoBehaviour
{
    //public int rayCount = 7;
    public float angleRange = 90;
    public float targetVelocity = 10.0f;
    public float rayRange = 10f;

    public float raycastDistance = 10f;
    public int rayCount = 360; // Number of rays to cast

    private void FixedUpdate()
    {
        //GetAvoidingForces();
        //gameObject.GetComponent<Rigidbody>().AddForce(GetAvoidingForce());
    }

    void OnDrawGizmos() 
    {
        /*
        for (int i = 1; i < rayCount; i++)
        {
            var rotation = transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (float)rayCount) * angleRange * 2 - angleRange, transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(transform.position, direction);
        }

        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                for (int z = -3; z <= 3; z++)
                {
                    Quaternion rotation = Quaternion.Euler((x / 3) * 180, (y / 3) * 180, (z / 3) * 180);
                }
            }
        }

        Gizmos.DrawRay(transform.position, transform.up);
        */
        //Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
        //Quaternion rotation = Quaternion.identity;
        //transform.forward + )
    }

    Vector3 GetAvoidingForce()
    {
        // Avoid nearby obstacles
        Vector3 avoidingForce = Vector3.zero;
        for (int i = 1; i < rayCount; i++)
        {
            var rotation = transform.rotation;

            var rotationMod = Quaternion.AngleAxis((i / (float)rayCount) * angleRange * 2 - angleRange, transform.up);
            
            var direction = rotation * Vector3.forward;

            var ray = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                if (hitInfo.collider.CompareTag("WorldObj"))
                {
                    avoidingForce += -direction * (rayRange - hitInfo.distance); //* (1.0f/rayCount);
                }
            }
        }
        //avoidingForce = Vector3.Normalize(avoidingForce);
        //if (drawRays) { Debug.DrawRay(transform.position, avoidingForce, Color.blue); }
        //Debug.DrawRay(transform.position, avoidingForce, Color.yellow); 
        return avoidingForce;
    }

    Vector3 GetAvoidingForces() 
    {
        Vector3 avoidingForces = Vector3.zero;
        /*
        for (int x = -2; x < 2; x++)
        {
            for (int y = -2; y < 2; y++)
            {
                //for (int z = -4; z <= 4; z++)
                //{
                    Quaternion rotation = Quaternion.Euler(((float)x/2.0f)*180f, ((float)y / 2.0f) *180, 0);
                    var direction = (rotation * Vector3.forward);
                    Debug.DrawRay(transform.position, direction, Color.yellow);
                //}
            }
        }
        */


        return avoidingForces;
    }

    void DrawRays()
    {
        // Iterate through all angles
        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the angle for each ray
            float angle = i * 360f / rayCount;

            // Convert angle to radians
            float radians = angle * Mathf.Deg2Rad;

            // Calculate direction vector based on angle
            Vector3 direction = new Vector3(Mathf.Cos(radians), 0f, Mathf.Sin(radians));

            // Raycast in the calculated direction
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, raycastDistance))
            {
                // Handle the hit object or position
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
            else
            {
                // Draw the ray if no hit
                Debug.DrawRay(transform.position, direction * raycastDistance, Color.green);
            }
        }
    }


}
