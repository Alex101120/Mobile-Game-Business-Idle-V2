using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject2 : MonoBehaviour
{
    public IncomeCalculator IncomeCalculator;

    public void Destroy()
    {
        string SaveGameCars = PlayerPrefs.GetString(IncomeCalculator.CompanyNameId);

        if (this.gameObject.name == "Dacia")
        {
            // G?se?te indexul primei apari?ii a literei "d"
            int index = SaveGameCars.IndexOf("d");
            if (index != -1)
            {
                // ?terge doar prima apari?ie a literei "d"
                SaveGameCars = SaveGameCars.Remove(index, 1);

                // Salveaz? ?irul actualizat înapoi în PlayerPrefs
                PlayerPrefs.SetString(IncomeCalculator.CompanyNameId, SaveGameCars);
            }
        }

       gameObject.name = "Destroy";
    }
}
