using UnityEngine;

public class cloudSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform player;
    private float pivot = 23;
    [SerializeField] private float time=0;
    private float rate = 9;
    [SerializeField] private GameObject cloud;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(player==null)
        {
            Debug.LogError("player position is missing from cloud spawner");
        }
    }

    // Update is called once per frame
    void Update()
    {
        position(); //this is for the movement
        spawning(); //this is for the spawning of clouds
    }

    void position()
    {
        transform.position = new Vector3(player.position.x + pivot, 14, transform.position.z);
    }

    void spawning()
    {

        if(time>=rate)
        {
            Instantiate(cloud, new Vector3(transform.position.x, UnityEngine.Random.Range(13,15) , transform.position.z), Quaternion.identity);
            time=0;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
