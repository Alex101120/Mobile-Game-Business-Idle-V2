using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCars : MonoBehaviour
{
    public GameObject CloneLocation; // Loca?ia în care se cloneaz? ma?ina
    public GameObject originalObject; // Ma?ina original? care va fi clonat?

    // Metoda pentru clonarea ma?inii
    public void CloneDaciaCar()
    {
        // Verific?m dac? avem un obiect UI original ?i o loca?ie de clonare valid?
        if (originalObject != null && CloneLocation != null)
        {
            // Ob?inem referin?a la garaj
            GarageScript GarageScript = CloneLocation.GetComponent<GarageScript>();
            if (GarageScript != null)
            {
                // Verific?m dac? mai avem spa?iu în garaj pentru o alt? ma?in?
                if (GarageScript.CanAddCar())
                {
                    // Cre?m o nou? instan?? a obiectului original
                    GameObject clonedObject = Instantiate(originalObject);

                    // Set?m obiectul clonat ca fiind copil al obiectului p?rinte specificat
                    clonedObject.transform.SetParent(CloneLocation.transform, false);
                    clonedObject.name = "Dacia";
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
                Debug.LogWarning("Nu s-a g?sit un component GarageScript pe obiectul de clonare.");
            }
        }
        else
        {
            Debug.LogError("Nu ai setat un obiect original pentru clonare sau o loca?ie de clonare valid?!");
        }
    }
}
