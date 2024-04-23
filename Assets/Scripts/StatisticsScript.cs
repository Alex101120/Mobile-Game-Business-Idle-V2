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
    public long Networth;
    public float totalTimeInGame = 0f;
   

    void Start()
    {
        totalTimeInGame = PlayerPrefs.GetFloat("TimeInGame");
        InvokeRepeating("GetStat", 0f, 30f);

    }
   

    void GetStat()
    {
        
        Networth = ClickScript.moneyStatClick;
        ClickStatText.text="Clicks :" + ClickScript.ClickStat.ToString();
        MoneyStatText.text="Moneys earned from clicking:" + FormatMoney(ClickScript.moneyStatClick) +"$";
        NetworthText.text = "Networth:" + FormatMoney(Networth);
        TimeSpend.text="Total Time " + FormatTime(totalTimeInGame);
        PlayerPrefs.SetFloat("TimeInGame", totalTimeInGame);
       
        Debug.Log("Statistics Updated! ");
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

    void Update()
    {
        totalTimeInGame += Time.deltaTime;
    }
}
