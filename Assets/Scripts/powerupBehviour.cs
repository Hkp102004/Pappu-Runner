using UnityEngine;

public class powerupBehviour : MonoBehaviour
{
    playerBehaviour player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();

        if(player==null)
        {
            Debug.LogError("player script is missing from the powerupscript");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.powerup();
            Destroy(gameObject);
        }
    }
}
