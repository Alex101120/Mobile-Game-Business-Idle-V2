using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    string SaveGarageIndex;



    public void Start()
    {
        SaveGarageIndex = IncomeCalculator.CompanyNameId+"Garage";
        if (PlayerPrefs.HasKey(SaveGarageIndex))
        {
            UpgradeGarageIndex = PlayerPrefs.GetInt(SaveGarageIndex);
           
        }
        else
        {
            UpgradeGarageIndex = 0;
        }
        UpgradeGarage();
        GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
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
        GarageSpaceText.text = "Space Available: " + cars.Count +"/" + maxCars;
        SaveGame();

    }
    public void CheckSpace()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].name == "Destroy")
            {
                Destroy(cars[i]);
                cars.RemoveAt(i);
                break; // Ie?im din bucl? dup? ce am g?sit ?i ?ters ma?ina
            }
        }
        GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
    }
        public void UpgradeGarage()
    {
        switch (UpgradeGarageIndex)
        {
            case 0:
                maxCars = 7;
                PlayerPrefs.SetInt(SaveGarageIndex, UpgradeGarageIndex);
                UpgradeGarageIndex++;
                GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
                break;
                
            case 1:
                maxCars = 10;
                PlayerPrefs.SetInt(SaveGarageIndex, UpgradeGarageIndex);
                UpgradeGarageIndex++;
                GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
                break;
            case 2:
                maxCars = 15;
                PlayerPrefs.SetInt(SaveGarageIndex, UpgradeGarageIndex);
                UpgradeGarageIndex++;
                GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
                break;
            case 3:
                maxCars = 20;
                PlayerPrefs.SetInt(SaveGarageIndex, UpgradeGarageIndex);
                UpgradeGarageIndex++;
                GarageSpaceText.text = "Space Available: " + cars.Count + "/" + maxCars;
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
