using UnityEngine;

public class background : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform cameraPosition;
    void Start()
    {
        if(cameraPosition == null)
        {
            Debug.LogError("Camera position is missing in the background script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraPosition.position.x + offset.x, cameraPosition.position.y + offset.y, offset.z);
    }

}
