using UnityEngine;
using TMPro;
using System;

public class RNGScript : MonoBehaviour
{
    public const float maxStocks = 500000f;
    private float currentstocks;
    public float startPrice = 100f;
    public float maxChange = 5f;
    public float minChange = -5f;
    public const float divident = 0.2f;
    public float MoneyDivident;
    private float currentPrice;
    public float Stocks1Owned;
    private float averagePrice;
    public long NetworthStock;
    private string stockname;
    public TMP_Text stockPriceText;
    public TMP_Text stockOwnedText;
    public TMP_Text ProfitText;
    public TMP_Text AveragePriceText;
    public TMP_Text StocksAvailable;
    public TMP_Text StocksCanBuyText;
    public TMP_InputField inputField;
    public ClickScript clickScript;

    void Start()
    {
        stockname = gameObject.name; // Use the GameObject's name
        currentstocks = maxStocks;
        currentPrice = PlayerPrefs.GetFloat(stockname + "Price", 100f);
        Stocks1Owned = PlayerPrefs.GetFloat(stockname + "Owned", 0f);
        averagePrice = PlayerPrefs.GetFloat(stockname + "AveragePrice", 0f);

        currentstocks -= Stocks1Owned;

        UpdateUI();

        InvokeRepeating("UpdatePrice", 0f, 30f);
    }

    void UpdatePrice()
    {
        AutoSave();
        float change = UnityEngine.Random.Range(minChange, maxChange);
        currentPrice = Mathf.Max(0, startPrice + change); // Ensure price doesn't go negative

        UpdateProfit();
        UpdateUI();
    }

    void UpdateProfit()
    {
        float profit = (currentPrice - averagePrice) * Stocks1Owned;
        ProfitText.text = $"Profit: {profit:F2}";
        ProfitText.color = profit > 0 ? Color.green : (profit < 0 ? Color.red : Color.white);

        NetworthStock = Convert.ToInt64(currentPrice * Stocks1Owned);
    }

    void UpdateUI()
    {
        stockPriceText.text = $"Stock Price: {currentPrice:F2}";
        stockOwnedText.text = $"Stocks Owned: {Stocks1Owned}";
        AveragePriceText.text = $"Average Price: {averagePrice:F2}";
        StocksAvailable.text = $"Stocks Available: {currentstocks}";
        // Update how many stocks you can buy
        float stocksCanBuy = clickScript.money / currentPrice;
        if(stocksCanBuy > currentstocks)
        {
            stocksCanBuy = currentstocks;
        }
        StocksCanBuyText.text = $"You can buy: {stocksCanBuy:F0} Stocks";
    }

    void CalculateDividents()
    {
        MoneyDivident = Stocks1Owned * divident;
    }

    public void BuyStocks()
    {
        if (!float.TryParse(inputField.text, out float stockCount) || stockCount <= 0)
        {
            Debug.LogError("Invalid input for stock count!");
            return;
        }

        float totalPrice = stockCount * currentPrice;

        if (totalPrice > clickScript.money)
        {
            Debug.LogError("Insufficient funds!");
            return;
        }

        if (stockCount > currentstocks)
        {
            Debug.LogError("Not enough stocks available!");
            return;
        }

        clickScript.money -= (long)totalPrice;
        float previousTotalStocks = Stocks1Owned;
        Stocks1Owned += stockCount;
        currentstocks -= stockCount;
        averagePrice = (averagePrice * previousTotalStocks + currentPrice * stockCount) / Stocks1Owned;

        CalculateDividents();
        UpdateUI();
    }

    public void SellStocks()
    {
        if (!float.TryParse(inputField.text, out float stockCount) || stockCount <= 0 || stockCount > Stocks1Owned)
        {
            Debug.LogError("Invalid stock count to sell!");
            return;
        }

        float sellPrice = stockCount * currentPrice;
        clickScript.money += (long)sellPrice;
        Stocks1Owned -= stockCount;
        currentstocks += stockCount;

        if (Stocks1Owned > 0)
        {
            // Recalculate the average price correctly
            averagePrice = ((averagePrice * (Stocks1Owned + stockCount)) - (currentPrice * stockCount)) / Stocks1Owned;
        }
        else
        {
            averagePrice = 0; // If no stocks are owned, reset the average price
        }

        CalculateDividents();
        UpdateUI();
    }

    void AutoSave()
    {
        PlayerPrefs.SetFloat(stockname + "Price", currentPrice);
        PlayerPrefs.SetFloat(stockname + "Owned", Stocks1Owned);
        PlayerPrefs.SetFloat(stockname + "AveragePrice", averagePrice);
        PlayerPrefs.SetFloat(stockname + "DivYield", MoneyDivident);
    }
}
