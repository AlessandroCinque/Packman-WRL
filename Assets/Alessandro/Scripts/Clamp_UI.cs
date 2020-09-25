using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clamp_UI : MonoBehaviour
{
    [SerializeField] PlayerController player;
    public Color error;
    public Color correct;
    public GameObject message;
    float display_time = 0.0f;
    public float max_display = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player.On_forbidden_food += OnForbidden;
        player.On_allowed_food += OnAllowed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 message_pos = Camera.main.WorldToScreenPoint(this.transform.position);
        message.transform.position = message_pos;

        display_time -= Time.deltaTime;
        if (display_time <= 0.0f)
        {
            
            message.SetActive(false);
        }
    }
   private void OnForbidden()
   {
        message.GetComponent<Text>().text = "NO!";
        message.GetComponent<Text>().color = error;
        message.SetActive(true);
        display_time = max_display;
   }
    private void OnAllowed()
    {
        message.GetComponent<Text>().text = "Yum!";
        message.GetComponent<Text>().color = correct;
        message.SetActive(true);
        display_time = max_display;
    }
}
