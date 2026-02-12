using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Text messageText;
    [SerializeField] private float waitTime;
    void Start()
    {
        messageText.gameObject.SetActive(false);
        if(messageText==null)
        {
            Debug.LogError("message text is missing from uimanager script");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        StartCoroutine(MessageCooldown());
    }

    IEnumerator MessageCooldown()
    {
        yield return new WaitForSeconds(waitTime);
        messageText.gameObject.SetActive(false);
    }
}
