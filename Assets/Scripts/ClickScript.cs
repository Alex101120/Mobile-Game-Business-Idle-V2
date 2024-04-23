using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Android;

public class ClickScript : MonoBehaviour
{
    public long money;  
    public long moneyStatClick;
    public int ClickStat;
    public int ratio = 0;
    private bool[] upgrades = new bool[4]; // Array to store upgrade statuses
    public TMP_Text MoneyText;
    public Button[] UpgradeButtons = new Button[4]; // Array to store upgrade buttons

    private void Start()


    {
        string MoneyString;
        string moneyStatClickString;
        ratio = PlayerPrefs.GetInt("ratio");
        MoneyString = PlayerPrefs.GetString("Money");
        money = long.Parse(MoneyString);
        ClickStat = PlayerPrefs.GetInt("StatClick");
        moneyStatClickString = PlayerPrefs.GetString("MoneyStat");
        moneyStatClick = long.Parse(moneyStatClickString);
        CheckUpgrades();
<<<<<<< Updated upstream
=======
        StartCoroutine(AddMoneyPerSecond());
        InvokeRepeating("Save", 0f, 30f);

        if (PlayerPrefs.HasKey("LastExitTime"))
        {
            // Dac? da, îl recuper?m
            string lastExitTimeString = PlayerPrefs.GetString("LastExitTime");
            lastExitTime = DateTime.Parse(lastExitTimeString);

            // Calcul?m diferen?a de timp în secunde
            TimeSpan timeDifference = DateTime.Now - lastExitTime;
            float secondsDifference = (float)timeDifference.TotalSeconds;

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
>>>>>>> Stashed changes
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
   
    public void AutoSaveGame()
    {
    PlayerPrefs.SetString("Money", money.ToString());
    PlayerPrefs.SetInt("ratio", ratio);
    PlayerPrefs.SetInt("StatClick", ClickStat);
    PlayerPrefs.SetString("MoneyStat", moneyStatClick.ToString());
<<<<<<< Updated upstream
=======
    PlayerPrefs.SetFloat("TotalIncomePerScond", totalIncomePerSecond);
    }

    void Save()
    {
        PlayerPrefs.SetString("Money", money.ToString());
        PlayerPrefs.SetInt("ratio", ratio);
        PlayerPrefs.SetInt("StatClick", ClickStat);
        PlayerPrefs.SetString("MoneyStat", moneyStatClick.ToString());
        PlayerPrefs.SetFloat("TotalIncomePerScond", totalIncomePerSecond);
        PlayerPrefs.SetString("LastExitTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
>>>>>>> Stashed changes
    }
   

    
}
