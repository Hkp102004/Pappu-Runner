using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject powerup;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if(p!=null)
        {
            player = p.GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("player gameobject is missing in powerSpawner script");
            return;
        }

        if(powerup == null)
        {
            Debug.LogError("The powerup is missing form the powerup spawner");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        position();
    }

    void position()
    {
        transform.position = new Vector3(player.position.x+ offset.x, player.position.y+offset.y, transform.position.z);
    }


    public void spawn()
    {
        Instantiate(powerup, transform.position, Quaternion.identity);
    }
}
