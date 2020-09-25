using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Food_Type that_food;
    public GameObject[] prefab_carbs;
    public GameObject[] prefab_protein;
    public GameObject[] prefab_vitamins;
    public GameObject[] prefab_sugars;
    //========================================================
    public Transform[] spawnerLocations;
    // Start is called before the first frame update
    void Start()
    {
      
        Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < prefab_protein.Length; i++)
        {
            prefab_protein[i] = Instantiate(prefab_protein[i], new Vector3(0f, 20f, -54f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        for (int i = 0; i < prefab_carbs.Length; i++)
        {
            prefab_carbs[i] = Instantiate(prefab_carbs[i], new Vector3(0f, 20f, -54f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        for (int i = 0; i < prefab_vitamins.Length; i++)
        {
            prefab_vitamins[i] = Instantiate(prefab_vitamins[i], new Vector3(0f, 20f, -54f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        for (int i = 0; i < prefab_sugars.Length; i++)
        {
            prefab_sugars[i] = Instantiate(prefab_sugars[i], new Vector3(0f, 20f, -54f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }

        prefab_carbs[Random.Range(0, prefab_carbs.Length)].transform.position = new Vector3(spawnerLocations[3].transform.position.x, spawnerLocations[3].transform.position.y,0.0f);
        prefab_protein[Random.Range(0, prefab_protein.Length)].transform.position = new Vector3(spawnerLocations[4].transform.position.x, spawnerLocations[4].transform.position.y, 0.0f);
        prefab_vitamins[Random.Range(0, prefab_vitamins.Length)].transform.position = new Vector3(spawnerLocations[8].transform.position.x, spawnerLocations[8].transform.position.y, 0.0f);
        prefab_sugars[Random.Range(0, prefab_sugars.Length)].transform.position = new Vector3(spawnerLocations[12].transform.position.x, spawnerLocations[12].transform.position.y, 0.0f);
    }

  

    public void Respawn1(int ID, Food_Type food_Type)
    {
        that_food = food_Type;
        //this is because I don't know why it spawns them at z = -53, even though all the location are at z= 0
        int where_to_spawn = 0;
        Vector3 adj_where_to = Vector3.zero;

        
        switch (that_food)
        {

            case Food_Type.Carbohydrates:
                where_to_spawn = Random.Range(0, 3);
                prefab_carbs[ID].transform.position = new Vector3(0f, 20f, -54f);
                adj_where_to = new Vector3(spawnerLocations[where_to_spawn].transform.position.x, spawnerLocations[where_to_spawn].transform.position.y, 0.0f);
                prefab_carbs[Random.Range(0, prefab_carbs.Length)].transform.position = adj_where_to;
                break;
            case Food_Type.Proteins:
                where_to_spawn = Random.Range(4, 7);
                prefab_protein[ID].transform.position = new Vector3(0f, 20f, -54f);
                adj_where_to = new Vector3(spawnerLocations[where_to_spawn].transform.position.x, spawnerLocations[where_to_spawn].transform.position.y, 0.0f);
                prefab_protein[Random.Range(0, prefab_protein.Length)].transform.position = adj_where_to;
                break;
            case Food_Type.Vitamins:
                where_to_spawn = Random.Range(8, 11);
                prefab_vitamins[ID].transform.position = new Vector3(0f, 20f, -54f);
                adj_where_to = new Vector3(spawnerLocations[where_to_spawn].transform.position.x, spawnerLocations[where_to_spawn].transform.position.y, 0.0f);
                prefab_vitamins[Random.Range(0, prefab_vitamins.Length)].transform.position = adj_where_to;
                break;

            case Food_Type.Sugars:
                where_to_spawn = Random.Range(12, 15);
                prefab_sugars[ID].transform.position = new Vector3(0f, 20f, -54f);
                adj_where_to = new Vector3(spawnerLocations[where_to_spawn].transform.position.x, spawnerLocations[where_to_spawn].transform.position.y, 0.0f);
                prefab_sugars[Random.Range(0, prefab_sugars.Length)].transform.position = adj_where_to;
                break;


        }
    }
}

