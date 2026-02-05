using Unity.VisualScripting;
using UnityEngine;

public class rock : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float deathzone = 18f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(player == null)
        {
            Debug.LogError("Tranform of player is missing in rock script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x <= player.position.x - deathzone)
        {
            Destroy(gameObject);
        }
    }
}
