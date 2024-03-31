using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Android;
using Unity.VisualScripting;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System;
using System.Collections;

public class ClickScript : MonoBehaviour
{
    private float previousTotalIncome;
    public long money;
    public long moneyStatClick;
    public int ClickStat;
    public int ratio = 0;
    public float totalIncomePerSecond;
    public float totalIncome;
    public long totalIncomelong;
    private bool[] upgrades = new bool[4]; // Array to store upgrade statuses
    public TMP_Text MoneyText;
    public TMP_Text MoneyText2;
    public Image Error;
    public Button[] UpgradeButtons = new Button[4]; // Array to store upgrade buttons
    public CreateBussines CreateBussines;
    public IncomeCalculator IncomeCalculator;
    public bool Changed;
    public List<GameObject> Bussinesses = new List<GameObject>();
    public GameObject BussinesTab;

    private void Start()


    {
        Application.targetFrameRate = 60;
        string MoneyString;
        string moneyStatClickString;
        ratio = PlayerPrefs.GetInt("ratio");
        MoneyString = PlayerPrefs.GetString("Money");
        money = long.Parse(MoneyString);
        ClickStat = PlayerPrefs.GetInt("StatClick");
        moneyStatClickString = PlayerPrefs.GetString("MoneyStat");
        moneyStatClick = long.Parse(moneyStatClickString);
        CheckUpgrades();
        StartCoroutine(AddMoneyPerSecond());
    }

    private void Update()
    {
        UpdateUI();
       
        AutoSaveGame();

    }

    private void CheckUpgrades()
    {
        if (ratio >= 1) upgrades[0] = true;
        if (ratio >= 3) upgrades[1] = true;
        if (ratio >= 7) upgrades[2] = true;
        if (ratio >= 15) upgrades[3] = true;
    }

    private void UpdateUI()
    {
        MoneyText.text = "$: " + FormatMoney(money);
        MoneyText2.text = "$: " + FormatMoney(money);


        for (int i = 0; i < UpgradeButtons.Length; i++)
        {
            if (money > GetUpgradeCost(i) && !upgrades[i])
            {
                UpgradeButtons[i].gameObject.SetActive(true);

            }
            else
                UpgradeButtons[i].gameObject.SetActive(false);
        }

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
    private int GetUpgradeCost(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case 0: return 50;
            case 1: return 200;
            case 2: return 750;
            case 3: return 1400;
            default: return 0;
        }
    }

    public void OnClick()
    {

        money += 1 + ratio;
        ClickStat += 1;
        moneyStatClick += 1 + ratio;
    }

    public void UpgradeTier(int tierIndex)
    {
        ratio = GetRatioByTier(tierIndex);
        money -= GetUpgradeCost(tierIndex);
        upgrades[tierIndex] = true;
    }


    private int GetRatioByTier(int tierIndex)
    {
        switch (tierIndex)
        {
            case 0: return 1;
            case 1: return 3;
            case 2: return 7;
            case 3: return 15;
            default: return 0;
        }
    }
    public void BuyTaxiCompany()

    {
        if (money >= 30000)
        {
            money = money - 30000;
            CreateBussines.CloneTaxiBusiness();

        }
        else
        {
            Error.gameObject.SetActive(true);
        }

    }
    public void BuyTaxiCar()
    {
        if (money >= 3000)
        {
            money = money - 3000;
        }
        else
        {
            Error.gameObject.SetActive(true);
        }
    }
    public void CalculateTotalIncome()
    {
        totalIncome = 0;
        foreach (Transform child in BussinesTab.transform)
        {

            IncomeCalculator[] incomeCalculators = child.GetComponentsInChildren<IncomeCalculator>();


            foreach (IncomeCalculator incomeCalculator in incomeCalculators)
            {

                if (incomeCalculator != null)
                {
                    totalIncome += incomeCalculator.GetTotalIncome();
                    totalIncomePerSecond = (totalIncome / 3600);
                    totalIncomelong = Convert.ToInt64(totalIncomePerSecond);
                }
            }
        }
        Debug.Log("Venit total: " + totalIncome);
    }
    private IEnumerator AddMoneyPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // A?tepta?i 1 secund?

            // Incrementa?i contorul cu 1
            money += totalIncomelong;

            Debug.Log("Count: " + money.ToString());
        }
    }



    public void AutoSaveGame()
    {
    PlayerPrefs.SetString("Money", money.ToString());
    PlayerPrefs.SetInt("ratio", ratio);
    PlayerPrefs.SetInt("StatClick", ClickStat);
    PlayerPrefs.SetString("MoneyStat", moneyStatClick.ToString());
    }

    
}
