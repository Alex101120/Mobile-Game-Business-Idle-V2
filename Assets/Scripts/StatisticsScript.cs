using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class StatisticsScript : MonoBehaviour
{
    public TMP_Text NetworthText;
    public TMP_Text ClickStatText;
    public TMP_Text MoneyStatText;
    public TMP_Text TimeSpend;
    public ClickScript ClickScript;
    public int ClickStat;
    public long MoneyStat;
    public long Networth;
    public float totalTimeInGame = 0f;
    float totalTime;

    void Start()
    {
        totalTimeInGame = PlayerPrefs.GetFloat("TimeInGame");
       

    }
   

    public void GetStat()
    {
        MoneyStat = ClickScript.moneyStatClick;
        ClickStat = ClickScript.ClickStat;
        Networth = MoneyStat;
        ClickStatText.text="Clicks :" + ClickStat.ToString();
        MoneyStatText.text="Moneys earned from clicking:" + FormatMoney(MoneyStat) +"$";
        NetworthText.text = "Networth:" + FormatMoney(Networth);
        TimeSpend.text="Total Time " + FormatTime(totalTimeInGame);
        PlayerPrefs.SetFloat("TimeInGame", totalTimeInGame);
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


    private void SaveGameStat()
    {
        
        PlayerPrefs.SetString("NetWorth",Networth.ToString());
    }
    void Update()
    {
        totalTimeInGame += Time.deltaTime;
    }
}
