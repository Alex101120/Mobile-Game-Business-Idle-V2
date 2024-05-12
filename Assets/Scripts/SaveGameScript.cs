using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveGameScript : MonoBehaviour
{
    public GameObject gameObjectToSave;

    void Start()
    {
        LoadGame();
        InvokeRepeating("SaveGame", 0f, 10f);
    }

    void SaveGame()
    {
        var writer = QuickSaveWriter.Create("GameData");

        // Save specific properties of the GameObject
        writer.Write("position", gameObjectToSave.transform.position);
        writer.Write("rotation", gameObjectToSave.transform.rotation.eulerAngles);
        writer.Write("name", gameObjectToSave.name);
        // You can add other specific data if needed

        writer.Commit();
        Debug.Log("Game data saved!");
    }

    // This is just to demonstrate how to load the saved data
    void LoadGame()
    {
        var reader = QuickSaveReader.Create("GameData");

        if (reader.Exists("position"))
        {
            Vector3 position = reader.Read<Vector3>("position");
            Vector3 rotation = reader.Read<Vector3>("rotation");
            string name = reader.Read<string>("name");

            // Apply loaded data to the GameObject
            gameObjectToSave.transform.position = position;
            gameObjectToSave.transform.rotation = Quaternion.Euler(rotation);
            gameObjectToSave.name = name;

            Debug.Log("Game data loaded!");
        }
    }
}
