using UnityEditor.Rendering.Universal;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

    [Header("Y Dead Zone")]
    [SerializeField] private float yDeadZone = 1.5f; // how far player can move before cam follows on Y

    private float targetY;

    void Start()
    {
        targetY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Update target Y only if player moves outside the dead zone
        float yDiff = player.position.y - targetY;
        if (Mathf.Abs(yDiff) > yDeadZone)
        {
            targetY = player.position.y - Mathf.Sign(yDiff) * yDeadZone;
        }

        Vector3 desiredPosition = new Vector3(
            player.position.x + offset.x,
            targetY + offset.y,
            offset.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        if (player.position.x >= 345)
        {
            offset = new Vector3(0, 1, -10.66f);
        }
    }
}