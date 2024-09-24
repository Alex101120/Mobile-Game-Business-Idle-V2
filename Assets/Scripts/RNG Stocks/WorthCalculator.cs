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
    private string FormatMoney(long money)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Q", "Qi", "S", "Sp", "O", "N", "D" };
        int suffixIndex = 0;

        while (money >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            money /= 1000;
            suffixIndex++;
        }

        return money.ToString("F1") + suffixes[suffixIndex];
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

        PortofolioWorth.text = "Portofolio Worth: " + FormatMoney(totalNetworth);
        DividentYield.text = "Divident Yield: " + totalDivident.ToString("F2");
    }
}
