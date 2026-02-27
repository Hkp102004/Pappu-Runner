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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Location();
    }

    void Location()
    {
        
        transform.position = new Vector3(player.position.x + distance, transform.position.y, transform.position.z);  // to make spawnerr move according to player position

        if(transform.position.x >= border)
        {
            transform.position = new Vector3(border,transform.position.y,transform.position.z); //logic to stop the spawner at border
        }
    }
}
