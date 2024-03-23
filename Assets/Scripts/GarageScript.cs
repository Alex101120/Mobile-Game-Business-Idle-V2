using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageScript : MonoBehaviour
{
    public int maxCars = 5; // Num?rul maxim de ma?ini pe care garajul le poate stoca
    public TMP_Text GarageSpaceText;
    private List<GameObject> cars = new List<GameObject>(); // Lista de ma?ini din garaj

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

    }
}
