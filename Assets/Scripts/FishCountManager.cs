using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class FishCountManager : MonoBehaviour
{
    int boidStartCount = 100;
    int boidCount = 0;
    [SerializeField] GameObject fishPrefab;
    [SerializeField] int spawnRange = 10;
    List<GameObject> school = new List<GameObject>();
    public Slider boidSlider;
    public TextMeshProUGUI boidCountValue;

    [Header("Strengths")]
    public float cohesionStrength = 1f;
    public float separationStrength = 1f;
    public float alignmentStrength = 1f;
    public float avoidanceStrength = 1f;
    [Header("Radii")]
    public float cohesionRadius = 15f;
    public float separationRadius = 5f;
    public float alignmentRadius = 12f;
    public float avoidanceRadius = 15f;
    [Header("Strength Texts")]
    public TextMeshProUGUI cohStrValue;
    public TextMeshProUGUI sepStrValue;
    public TextMeshProUGUI aliStrValue;
    public TextMeshProUGUI avoStrValue;
    [Header("Strength Sliders")]
    public Slider cohStrSlider;
    public Slider sepStrSlider;
    public Slider aliStrSlider;
    public Slider avoStrSlider;
    [Header("Radius Texts")]
    public TextMeshProUGUI cohRadValue;
    public TextMeshProUGUI sepRadValue;
    public TextMeshProUGUI aliRadValue;
    public TextMeshProUGUI avoRadValue;
    [Header("Radius Sliders")]
    public Slider cohRadSlider;
    public Slider sepRadSlider;
    public Slider aliRadSlider;
    public Slider avoRadSlider;

    void Update()
    {
        //set up UI
        cohesionStrength = cohStrSlider.value;
        separationStrength = sepStrSlider.value;
        alignmentStrength = aliStrSlider.value;
        avoidanceStrength = avoStrSlider.value;
        
        cohStrValue.text = cohesionStrength.ToString("0.0");
        sepStrValue.text = separationStrength.ToString("0.0");
        aliStrValue.text = alignmentStrength.ToString("0.0");
        avoStrValue.text = avoidanceStrength.ToString("0.0");

        cohesionRadius = cohRadSlider.value;
        separationRadius = sepRadSlider.value;
        alignmentRadius = aliRadSlider.value;
        avoidanceRadius = avoRadSlider.value;

        cohRadValue.text = cohesionRadius.ToString("0.0");
        sepRadValue.text = separationRadius.ToString("0.0");
        aliRadValue.text = alignmentRadius.ToString("0.0");
        avoRadValue.text = avoidanceRadius.ToString("0.0");

        for (int i = 0; i < school.Count; i++) 
        {
            //more UI setup
            school[i].transform.GetChild(1).gameObject.GetComponent<FlockManager>().forceMultiplier = cohesionStrength;
            school[i].transform.GetChild(2).gameObject.GetComponent<FlockManager>().forceMultiplier = separationStrength;
            school[i].transform.GetChild(3).gameObject.GetComponent<FlockManager>().forceMultiplier = alignmentStrength;
            school[i].transform.GetChild(4).gameObject.GetComponent<WallAvoidance>().forceMultiplier = avoidanceStrength;

            school[i].transform.GetChild(1).gameObject.transform.localScale = new Vector3(cohesionRadius, cohesionRadius, cohesionRadius);
            school[i].transform.GetChild(2).gameObject.transform.localScale = new Vector3(separationRadius, separationRadius, separationRadius);
            school[i].transform.GetChild(3).gameObject.transform.localScale = new Vector3(alignmentRadius, alignmentRadius, alignmentRadius);
            school[i].transform.GetChild(4).gameObject.transform.localScale = new Vector3(avoidanceRadius, avoidanceRadius, avoidanceRadius);
        }
        //scrapped feature for adding/removing boids
        while(school.Count < boidSlider.value) { SpawnFish(); }
        while(school.Count > boidSlider.value) { KillFish(); }
        boidCountValue.text = school.Count.ToString();

    }

    void Start()
    {
        for(int i = 0; i < boidStartCount; i++) 
        {
            SpawnFish();
        }
    }

    public void SpawnFish() 
    { //spawn in a boid with a random color and position
        Vector3 position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        GameObject fish = Instantiate(fishPrefab, position, Quaternion.Euler(90, 0, 0));
        Renderer renderer = fish.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        Material material = renderer.material;
        TrailRenderer trail = fish.GetComponent<TrailRenderer>();

        Color randColor = new Color(Random.value, Random.value, Random.value);
        Color randColorClear = new Color(randColor.r, randColor.g, randColor.b, 20f);
        material.color = randColor;
        trail.material.color = randColorClear;
        school.Add(fish);
    }
    public void KillFish()
    { //scrapped function for removing boids
        GameObject dead = school[0];
        school.RemoveAt(0);
        Destroy(dead);
    }

    public void ToggleBehavior(int behavior) 
    { //UI button - toggles the colliders for different behaviors
        for (int i = 0; i < school.Count; i++) 
        {
            GameObject child = school[i].transform.GetChild(behavior).gameObject;
            child.SetActive(!child.activeSelf);
        }
    }

    public void ToggleRays(int behavior)
    { //UI button - toggles the raycast debugs for different behaviors
        for (int i = 0; i < school.Count; i++)
        {
            GameObject child = school[i].transform.GetChild(behavior).gameObject;
            if (child.activeSelf)
            {
                if (behavior == 4) {
                    child.GetComponent<WallAvoidance>().drawRays = !child.GetComponent<WallAvoidance>().drawRays;
                    Debug.Log("yellow toggled " + child.GetComponent<WallAvoidance>().drawRays);
                } else {
                    child.GetComponent<FlockManager>().drawRays = !child.GetComponent<FlockManager>().drawRays;
                }
            }
        }
    }

    public void ToggleSpheres(int behavior)
    { //UI button - toggles the radius visuals for different behaviors
        for (int i = 0; i < school.Count; i++)
        {
            GameObject child = school[i].transform.GetChild(behavior).gameObject;
            if (child.activeSelf) 
            {
                child.GetComponent<Renderer>().enabled = !child.GetComponent<Renderer>().enabled;
            }
        }
    }

}
