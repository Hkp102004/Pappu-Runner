using System.Numerics;
using UnityEngine;

public class boulder : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
