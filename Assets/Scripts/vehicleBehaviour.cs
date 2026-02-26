using UnityEngine;

public class vehicleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deathzone = 18f;
    Transform player_position;
    void Start()
    {
        player_position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(player_position == null)
        {
            Debug.LogError("player_position is missing from ", gameObject);
            return;
        }
    }

    void Update()
    {
        
    }

    void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x < player_position.position.x + deathzone)
        {
            Destroy(gameObject);
            Debug.Log("Vehicle is out of bound and destroyed");
        }
    }
}
