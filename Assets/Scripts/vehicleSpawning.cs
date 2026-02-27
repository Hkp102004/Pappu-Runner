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
        
        transform.position = new Vector3(player.position.x + distance, transform.position.y, transform.position.z);
    }
}
