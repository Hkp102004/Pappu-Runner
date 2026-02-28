using System;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    [SerializeField] private float SpawnRate=2;
    [SerializeField] private float time;
    [SerializeField] private float activeDistance;
    [SerializeField] private float MaxDistance;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        Location(); //to track the position and move according to the player
        Check(); //function to check player position to start and stop spawner
        if(active)
        {
            Spawn();
        }
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

    void Spawn()
    {   
        //1.10
        //1.11
        if(time >= SpawnRate)
        {
            Instantiate(vehicles[UnityEngine.Random.Range(0,6)], new Vector3(transform.position.x, UnityEngine.Random.Range(top,bottom), transform.position.z), Quaternion.identity);
            time=0;
        }
        else
        {
            time+=Time.deltaTime;
        }
    }
}
