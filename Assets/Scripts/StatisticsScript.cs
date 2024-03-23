using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsScript : MonoBehaviour
{
    public TMP_Text NetworthText;
    public TMP_Text ClickStatText;
    public TMP_Text MoneyStatText;
    public ClickScript ClickScript;
    public int ClickStat;
    public long MoneyStat;
    public long Networth;
    void Start()
    {
        
        
    }
    public void GetStat()
    {
        MoneyStat = ClickScript.moneyStatClick;
        ClickStat = ClickScript.ClickStat;
        Networth = MoneyStat;
        ClickStatText.text="Clicks :" + ClickStat.ToString();
        MoneyStatText.text="Moneys earned from clicking:" + FormatMoney(MoneyStat) +"$";
        NetworthText.text = "Networth:" + FormatMoney(Networth);
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
    private void SaveGameStat()
    {
        
        PlayerPrefs.SetString("NetWorth",Networth.ToString());
    }
    void Update()
    {
        
    }
}
