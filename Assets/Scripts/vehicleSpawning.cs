using System;
using System.Security.Principal;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class vehicleSpawning : MonoBehaviour
{
    [SerializeField] private GameObject[] vehicles;
    [SerializeField] private Transform player;
    [SerializeField] private float top;
    [SerializeField] private float bottom;
    [SerializeField] private float border;
    [SerializeField] private float distance;
    [SerializeField] private bool active;
    [SerializeField] private float time;
    [SerializeField] private float activeDistance;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        Location(); //to track the position and move according to the player
        Check(); //function to check player position to start and stop spawner
    }

    void Location()
    {
        
        transform.position = new Vector3(player.position.x + distance, transform.position.y, transform.position.z);  // to make spawnerr move according to player position

        if(transform.position.x >= border)
        {
            transform.position = new Vector3(border,transform.position.y,transform.position.z); //logic to stop the spawner at border
        }
    }

    void Check()
    {
        if(player.position.x - activeDistance >= transform.position.x)
        {
            active = false;
        }
        else
        {
            active = true;
        }
    }

    public void Spawn(float SpawnRate)
    {   
        if(time >= SpawnRate && active)
        {
            float[] positions = {top,bottom};
            float value = positions[UnityEngine.Random.Range(0,2)];
            Instantiate(vehicles[UnityEngine.Random.Range(0,6)], new Vector3(transform.position.x, value, transform.position.z), Quaternion.identity);
            time=0;
        }
        else
        {
            time+=Time.deltaTime;
        }
    }
}
