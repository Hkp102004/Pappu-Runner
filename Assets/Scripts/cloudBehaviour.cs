using UnityEngine;

public class cloudBehaviour : MonoBehaviour
{
    private float deathzone = 19;
    private float speed = 1.5f;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(player == null)
        {
            Debug.LogError("the player position is missing from the cloud script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement(); //the movement of the cloud
        deletion(); //the condition check and the deletion of the cloud
    }

    void movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void deletion()
    {
        if(transform.position.x <= player.position.x - deathzone)
        {
            Destroy(gameObject);
        }
    }
}
