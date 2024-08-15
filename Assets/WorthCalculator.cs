using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorthCalculator : MonoBehaviour
{
    public TMP_Text PortofolioWorth;
    public TMP_Text DividentYield;
    public GameObject Stocks; // Assign this in the Inspector

    void Start()
    {
        // Optional: Initialize the UI text fields
        PortofolioWorth.text = "Portofolio Worth: 0";
        DividentYield.text = "Divident Yield: 0";
        CalculateWorthAndDividends();
        // Call CalculateWorthAndDividends immediately and then every 30 seconds
        InvokeRepeating("CalculateWorthAndDividends", 0f, 30f);
    }

    void CalculateWorthAndDividends()
    {
        long totalNetworth = 0;
        float totalDivident = 0;

        foreach (Transform child in Stocks.transform)
        {
            RNGScript rngScript = child.GetComponent<RNGScript>();
            if (rngScript != null)
            {
                totalNetworth += rngScript.NetworthStock; // Ensure this variable name matches your script
                totalDivident += rngScript.MoneyDivident;  // Ensure this variable name matches your script
            }
        }

        PortofolioWorth.text = "Portofolio Worth: " + totalNetworth;
        DividentYield.text = "Divident Yield: " + totalDivident.ToString("F2");
    }
}
