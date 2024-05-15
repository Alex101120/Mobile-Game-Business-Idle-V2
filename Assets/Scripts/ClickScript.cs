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
    public TMP_Text OfflineBannerText;
    public TMP_Text CurrentRatio;
    public GameObject OfflineBanner;
    public Button[] UpgradeButtons = new Button[4]; // Array to store upgrade buttons
    public CreateBussines CreateBussines;
    public IncomeCalculator IncomeCalculator;
    public GameObject Error;
    public bool Changed;
    public List<GameObject> Bussinesses = new List<GameObject>();
    public GameObject BussinesTab;
    private DateTime lastExitTime;
    private float limitTime = 28800;

    private void Start()


    {
        string MoneyString;
        if (PlayerPrefs.HasKey("Money"))
        {
            MoneyString = PlayerPrefs.GetString("Money");
        }
        else
        {
            MoneyString = "1";
        }
        string moneyStatClickString ;
        if (PlayerPrefs.HasKey("MoneyStat"))
        {
            moneyStatClickString = PlayerPrefs.GetString("MoneyStat");
        }
        else
        {
            moneyStatClickString = "1";
        }
        Application.targetFrameRate = 60;
        ratio = PlayerPrefs.GetInt("ratio");
        money = long.Parse(MoneyString);
        ClickStat = PlayerPrefs.GetInt("StatClick");
        moneyStatClick = long.Parse(moneyStatClickString);
        totalIncomePerSecond = PlayerPrefs.GetFloat("TotalIncomePerScond");
        CurrentRatio.text = "X" + (1+ratio).ToString();   
        CheckUpgrades();
        StartCoroutine(AddMoneyPerSecond());
        InvokeRepeating("SaveGame", 0f, 30f);
     

        if (PlayerPrefs.HasKey("LastExitTime"))
        {
            // Dac? da, îl recuper?m
            string lastExitTimeString = PlayerPrefs.GetString("LastExitTime");
            lastExitTime = DateTime.Parse(lastExitTimeString);
           
            // Calcul?m diferen?a de timp în secunde
            TimeSpan timeDifference = DateTime.Now - lastExitTime;
            float secondsDifference = (float)timeDifference.TotalSeconds;

            if ( secondsDifference > limitTime)
            {
                secondsDifference = limitTime;
            }
            else
            {
                secondsDifference = secondsDifference;
            }


            long IncomeOffline = (long)secondsDifference * (long)totalIncomePerSecond ;
            money = money + IncomeOffline;
            OfflineBanner.SetActive(true);
            OfflineBannerText.text = "Timpul scurs de la ultima ie?ire: " + secondsDifference + " secunde si a generat" + FormatMoney(IncomeOffline);

            // Afis?m diferen?a în consol?
            Debug.Log("Timpul scurs de la ultima ie?ire: " + FormatTime(secondsDifference) + " secunde si a generat" + IncomeOffline );
        }
        else
        {
            Debug.Log("Prima rulare a aplica?iei.");
            OfflineBanner.SetActive(false);
        }
    }
    public void Add1m()
    {
        money = money + 100000000;
    }
    private void Update()
    {
        UpdateUI();
       
        

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
    private string FormatTime(float time)
    {
        string[] suffixes = { "S", "M", "H", "D" };
        int suffixIndex = 0;

        while (time >= 60 && suffixIndex < suffixes.Length - 1)
        {
            time /= 60;
            suffixIndex++;
        }

        if (suffixIndex == suffixes.Length - 1 && time >= 24)
        {
            time /= 24;
        }

        return time.ToString("F1") + suffixes[suffixIndex];
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
        CurrentRatio.text = "X" + (1+ratio).ToString();
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

            
        }
    }



    void SaveGame()
    {
    PlayerPrefs.SetString("Money", money.ToString());
    PlayerPrefs.SetInt("ratio", ratio);
    PlayerPrefs.SetInt("StatClick", ClickStat);
    PlayerPrefs.SetString("MoneyStat", moneyStatClick.ToString());
    PlayerPrefs.SetFloat("TotalIncomePerScond", totalIncomePerSecond);
    PlayerPrefs.SetString("LastExitTime", DateTime.Now.ToString());
    PlayerPrefs.Save();
    Debug.Log("GameSaved!" );
    }
   


}
