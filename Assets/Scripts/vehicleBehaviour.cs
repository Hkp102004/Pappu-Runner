using UnityEngine;

public class vehicleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float deathzone = 18f;
    public float endzone = 40;
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
        Movement();   //working on this
    }

    public void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x < player_position.position.x - deathzone || transform.position.x < endzone)
        {
            Destroy(gameObject);
            Debug.Log("Vehicle is out of bound and destroyed");
        }
    }
}
