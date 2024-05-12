using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCars : MonoBehaviour
{
    public GameObject CloneLocation; // Loca?ia în care se cloneaz? ma?ina
    public GameObject originalObject; // Ma?ina original? care va fi clonat?
    public ClickScript ClickScript;
    public IncomeCalculator IncomeCalculator;
    public GameObject Error;

    private void Start()
    {
        if (PlayerPrefs.HasKey(IncomeCalculator.CompanyNameId))
        {
            string SaveGameCars = PlayerPrefs.GetString(IncomeCalculator.CompanyNameId);

            // Verific?m dac? exist? ma?ini salvate în PlayerPrefs
            if (!string.IsNullOrEmpty(SaveGameCars))
            {
                // Iter?m prin fiecare caracter din ?irul de caractere SaveGameCars
                foreach (char c in SaveGameCars)
                {
                    if (c == 'd')
                    {

                        GarageScript GarageScript = CloneLocation.GetComponent<GarageScript>();
                        GameObject clonedObject = Instantiate(originalObject);
                        clonedObject.transform.SetParent(CloneLocation.transform, false);
                        clonedObject.name = originalObject.name;
                        clonedObject.SetActive(true);
                        GarageScript.AddCar(clonedObject);

                    }
                }
            }
        }
     
    }
    // Metoda pentru clonarea ma?inii
    public void CloneDaciaCar()
    {
        // Verific?m dac? avem suficien?i bani pentru a crea ma?ina
        if (ClickScript.money < 3000)
        {
            Debug.LogWarning("Nu ai suficien?i bani pentru a crea ma?ina!");
            Error.gameObject.SetActive(true);
            Error.SetActive(true); // Activeaz? mesajul de eroare
            return; // Ie?i din metoda CloneDaciaCar() dac? nu ai suficien?i bani
        }

        // Verific?m dac? avem un obiect original ?i o loca?ie de clonare valid?
        if (originalObject != null && CloneLocation != null)
        {
            // Ob?inem referin?a la garaj
            GarageScript GarageScript = CloneLocation.GetComponent<GarageScript>();
            if (GarageScript != null)
            {
                // Reducem suma de bani a juc?torului
                ClickScript.money -= 3000;

                // Verific?m dac? mai avem spa?iu în garaj pentru o alt? ma?in?
                if (GarageScript.CanAddCar())
                {
                    // Cre?m o nou? instan?? a obiectului original
                    GameObject clonedObject = Instantiate(originalObject);

                    // Set?m obiectul clonat ca fiind copil al obiectului p?rinte specificat
                    clonedObject.transform.SetParent(CloneLocation.transform, false);
                    clonedObject.name = originalObject.name;
                    clonedObject.SetActive(true);

                    // Ad?ug?m ma?ina clonat? în lista garajului
                    GarageScript.AddCar(clonedObject);
                }
                else
                {
                    Debug.LogWarning("Garajul este plin! Nu se poate ad?uga o alt? ma?in?.");
                }
            }
            else
            {
                Error.SetActive(true);
                Debug.LogWarning("Nu s-a g?sit un component GarageScript pe obiectul de clonare.");
            }
        }
        else
        {
            Debug.LogError("Nu ai setat un obiect original pentru clonare sau o loca?ie de clonare valid?!");
        }
    }
   
}
