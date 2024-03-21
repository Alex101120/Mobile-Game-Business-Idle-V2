using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBussines : MonoBehaviour
{
    public GameObject CloneLocation;
    public GameObject originalObject;
    
    public void CloneTaxiBusiness()
    {
        // Verific?m dac? avem un obiect UI original
        if (originalObject != null)
        {
            // Cre?m o nou? instan?? a obiectului original
            GameObject clonedObject = Instantiate(originalObject);

            // Set?m obiectul clonat ca fiind copil al obiectului p?rinte specificat
            clonedObject.transform.SetParent(CloneLocation.transform, false);
            clonedObject.name = "TaxiCompany";
            clonedObject.SetActive(true);


        }
        else
        {
            Debug.LogError("Nu ai setat un obiect original pentru clonare!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
