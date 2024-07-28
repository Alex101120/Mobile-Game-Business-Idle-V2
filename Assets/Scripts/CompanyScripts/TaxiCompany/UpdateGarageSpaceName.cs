using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateData : MonoBehaviour
{
    public TMP_Text GarageSpaceText;
    public IncomeCalculator IncomeCalculator;
    public GarageScript GarageScript;
    string SaveGarageIndex;
    public int maxCars = 5;
    public int UpgradeGarageIndex;
    // Start is called before the first frame update
    void Start()
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

        GarageSpaceText.text = "Space Available: " + GarageScript.cars.Count + "/" + maxCars;
        UpgradeGarage();
    }
    public void UpgradeGarage()
    {
        switch (UpgradeGarageIndex)
        {
            case 0:
                maxCars = 5;
                GarageSpaceText.text = "Space Available: " + GarageScript.cars.Count + "/" + maxCars;
                break;

            case 1:
                maxCars = 10;
                GarageSpaceText.text = "Space Available: " + GarageScript.cars.Count + "/" + maxCars;
                break;
            case 2:
                maxCars = 15;
                GarageSpaceText.text = "Space Available: " + GarageScript.cars.Count + "/" + maxCars;
                break;
            case 3:
                maxCars = 20;
                GarageSpaceText.text = "Space Available: " + GarageScript.cars.Count + "/" + maxCars;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
