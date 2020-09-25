using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Food_Type { Carbohydrates, Proteins, Vitamins, Sugars };
public class Food_Script : MonoBehaviour
{
    public int ID;
    Spawner spawner;
    public Food_Type food_type;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.Respawn1(ID, food_type);
        }
        
    }
}
