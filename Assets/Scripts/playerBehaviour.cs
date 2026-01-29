using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 10f;
    [SerializeField] private float height = 400f;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float horiInput = Input.GetAxis("Horizontal"); //key maps for fonrizontal inputs

        Vector3 direction = new Vector3(horiInput,0,0);
        transform.Translate(direction * speed * Time.deltaTime);

        if(transform.position.x <= -9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.up * height * Time.deltaTime);
        }
    }
}
