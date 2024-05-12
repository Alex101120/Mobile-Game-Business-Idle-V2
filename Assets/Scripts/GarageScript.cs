using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageScript : MonoBehaviour
{
    public string SaveGameCars;
    public string parentName;
    public int UpgradeGarageIndex;
    public int maxCars = 5; // Num?rul maxim de ma?ini pe care garajul le poate stoca
    public TMP_Text GarageSpaceText;
    public IncomeCalculator IncomeCalculator;
    public List<GameObject> cars = new List<GameObject>(); // Lista de ma?ini din garaj



    public void Start()
    {
        string Save = IncomeCalculator.CompanyNameId;
        if (PlayerPrefs.HasKey("UpgradeGarageIndex"))
        {
            UpgradeGarageIndex = PlayerPrefs.GetInt("UpgradeGarageIndex");
        }
        else
        {
            UpgradeGarageIndex = 0;
        }
       
    }
    // Metoda pentru a verifica dac? mai putem ad?uga o ma?in? în garaj
    public bool CanAddCar()
    {
        return cars.Count < maxCars;
    }

    // Metoda pentru ad?ugarea unei ma?ini în garaj
    public void AddCar(GameObject car)
    {
        
        cars.Add(car);
        GarageSpaceText.text = "Space Available: " + cars.Count + "/5";
        SaveGame();

    }

    public void UpgradeGarage()
    {
        switch (UpgradeGarageIndex)
        {
            case 0:
                maxCars = 7;
                PlayerPrefs.SetInt("UpgradeGarageIndex", UpgradeGarageIndex);
                break;
                
            case 1:
                maxCars = 10;
                PlayerPrefs.SetInt("UpgradeGarageIndex", UpgradeGarageIndex);
                break;
            case 2:
                maxCars = 15;
                PlayerPrefs.SetInt("UpgradeGarageIndex", UpgradeGarageIndex);
                break;
            case 3:
                maxCars = 20;
                PlayerPrefs.SetInt("UpgradeGarageIndex", UpgradeGarageIndex);
                break;
        }
    }
   void SaveGame()
    {

        SaveGameCars = "";

        string Save = IncomeCalculator.CompanyNameId;
        foreach (GameObject car in cars)
        {
            
            if (car.name.Contains("Dacia"))
            {
                SaveGameCars += "d";
            }
        }
        PlayerPrefs.SetString(Save, SaveGameCars);
    }
}
