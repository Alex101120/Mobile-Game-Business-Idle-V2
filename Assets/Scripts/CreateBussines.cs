using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBussines : MonoBehaviour
{
    public GameObject CloneLocation;
    public GameObject originalObject;
    public int idTaxi;


    private void Start()
    {
        if(PlayerPrefs.HasKey("TaxiCompanyId"))
        {
           idTaxi = PlayerPrefs.GetInt("TaxiCompanyId", idTaxi);
        }
        else
        {
            idTaxi = 0;
        }
        
       for (int i = 0; i < idTaxi; i++) 
        {
            GameObject clonedObject = Instantiate(originalObject);
            clonedObject.transform.SetParent(CloneLocation.transform, false);
            clonedObject.SetActive(true);
            clonedObject.name = "TaxiCompany" + i;
        }
        
    }
    public void CloneTaxiBusiness()
    {
        // Verific?m dac? avem un obiect UI original
        if (originalObject != null)
        {
            // Cre?m o nou? instan?? a obiectului original
            GameObject clonedObject = Instantiate(originalObject);

            // Set?m obiectul clonat ca fiind copil al obiectului p?rinte specificat
            clonedObject.transform.SetParent(CloneLocation.transform, false);
            clonedObject.name = "TaxiCompany" + idTaxi;
            clonedObject.SetActive(true);
            idTaxi += 1;

            PlayerPrefs.SetInt("TaxiCompanyId", idTaxi);

        }
        else
        {
            Debug.LogError("Nu ai setat un obiect original pentru clonare!");
        }
    }



}
