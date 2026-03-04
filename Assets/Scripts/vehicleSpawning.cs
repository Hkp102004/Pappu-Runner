using System;
using System.Security.Principal;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class vehicleSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] vehicles;
    [SerializeField] private Transform player;
    [SerializeField] private float top;
    [SerializeField] private float bottom;
    [SerializeField] private float spawnRate = 1.3f;
    [SerializeField] private bool active;
    [SerializeField] private float time;
    [SerializeField] private float startDist = 20;
    [SerializeField] private float pastDist = 15;
    [SerializeField] private float vehicleMax = 20;
    [SerializeField] private GameObject container;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        Condition(); //check the condition to spawn the vehicle
        Spawn(); //function to spawn the vehicle
    }


    void Condition()
    {
        if(player.position.x < transform.position.x + pastDist && player.position.x > transform.position.x - startDist)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }
    public void Spawn()
    {   
        if(time >= spawnRate && active)
        {
            float[] positions = {top,bottom};
            float value = positions[UnityEngine.Random.Range(0,2)];
            GameObject newVehicle = Instantiate(vehicles[UnityEngine.Random.Range(0,6)], new Vector3(transform.position.x, value, transform.position.z), Quaternion.identity);
            newVehicle.transform.parent = container.transform; // to keep the vehicle in a container
            vehicleBehaviour vehicleB = newVehicle.GetComponent<vehicleBehaviour>();
            if(vehicleB == null)
            {
                Debug.LogError("the vehicle script is missing in spawner after spawning it");
                return;
            }
            vehicleB.endzone = vehicleMax;
            time=0;
        }
        else
        {
            time+=Time.deltaTime;
        }
    }
}
