using UnityEngine;

public class vehicleBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
