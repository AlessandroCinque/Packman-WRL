using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Rules : MonoBehaviour
{
    // The "Food_Type" is declared inside the Food_Script.cs
    Food_Type food_type;
    //======================================================
    [SerializeField] PlayerController player;

    public Text rules_display;
    // Start is called before the first frame update
    void Start()
    {
        player.On_allowed_food += OnAllowed;
        rules_display = GameObject.Find("looking_for").GetComponent<Text>();
        food_type = (Food_Type)Random.Range(0, System.Enum.GetValues(typeof(Food_Type)).Length);
        player.SetFoodIWant(food_type);
    }

    // Update is called once per frame
    void Update()
    {
        rules_display.text = "I would like some... " + food_type.ToString();
    }
    void OnAllowed()
    {
        food_type = (Food_Type)Random.Range(0, System.Enum.GetValues(typeof(Food_Type)).Length);
        player.SetFoodIWant(food_type);
    }
}
