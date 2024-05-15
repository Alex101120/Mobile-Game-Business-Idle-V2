using UnityEngine;
using TMPro;
using System;

public class RNGScript : MonoBehaviour
{
    private float maxStocks = 500000;
    public float startPrice;
    public float maxChange = 5;
    public float minChange = -5;
    private float currentPrice;
    public float stocksOwned;
    private float averagePrice;
    public long NetworthStock1;
    public TMP_Text stockPriceText;
    public TMP_Text stockOwnedText;
    public TMP_Text ProfitText;
    public TMP_Text AveragePriceText;
    public TMP_Text StocksAvailable;
    public TMP_Text StocksPercentageText; // New TMP_Text for displaying the percentage of total stocks owned
    public TMP_Text StocksCanBuyText; // New TMP_Text for displaying how many stocks you can buy
    public TMP_InputField inputFieldStockBuy;
    public TMP_InputField inputFieldStockSell;
    public ClickScript clickScript;

    void Start()
    {
        if (PlayerPrefs.HasKey("Stock1Price"))
        {
            currentPrice = PlayerPrefs.GetFloat("Stock1Price");
        }
        else
        {
            currentPrice = 100;
        }

        if (PlayerPrefs.HasKey("StocksOwned"))
        {
            stocksOwned = PlayerPrefs.GetFloat("StocksOwned");
            maxStocks = maxStocks - stocksOwned;
        }
        else
        {
            stocksOwned = 0;
        }

        if (PlayerPrefs.HasKey("Stock1AveragePrice"))
        {
            averagePrice = PlayerPrefs.GetFloat("Stock1AveragePrice");
        }
        else
        {
            averagePrice = 0;
        }
        stockPriceText.text = "Stock Price: " + currentPrice.ToString("F2");
        stockOwnedText.text = "Stocks Owned: " + stocksOwned.ToString();
        AveragePriceText.text = "AveragePrice: " + averagePrice.ToString();
        StocksAvailable.text = "Stocks Available: " + maxStocks.ToString(); // New line to display available stocks
        InvokeRepeating("UpdatePrice", 0f, 30f);
    }

    void UpdatePrice()
    {
        AutoSave();
        float change = UnityEngine.Random.Range(minChange, maxChange);
        startPrice += change;
        currentPrice = startPrice;

        float profit = (currentPrice - averagePrice) * stocksOwned;
        stockPriceText.text = "Stock Price: " + currentPrice.ToString("F2");
        ProfitText.text = "Profit: " + profit.ToString("F2");

        if (profit > 0)
            ProfitText.color = Color.green;
        else if (profit < 0)
            ProfitText.color = Color.red;
        else
            ProfitText.color = Color.white; // or any other color for breakeven

        NetworthStock1 = Convert.ToInt64(currentPrice) * Convert.ToInt64(stocksOwned);
        Debug.Log("Current stock price: " + currentPrice);

        // Update the percentage of total stocks owned
        float stocksPercentage = (stocksOwned / maxStocks) * 100;
        StocksPercentageText.text = "Stocks Owned: " + stocksPercentage.ToString("F2") + "%";

        // Update how many stocks you can buy
        float stocksCanBuy = clickScript.money / currentPrice;
        StocksCanBuyText.text = "Stocks Can Buy: " + stocksCanBuy.ToString("F0");

        
    }

    public void BuyStocks()
    {
        if (!float.TryParse(inputFieldStockBuy.text, out float stockCount))
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

        // Update maxStocks to represent the maximum number of stocks that can be bought
        maxStocks += stockCount;

        clickScript.money -= (long)totalPrice;
        float totalStocksBought = stockCount + stocksOwned;
        averagePrice = (averagePrice * stocksOwned + currentPrice * stockCount) / totalStocksBought;

        stocksOwned = totalStocksBought;
        stockOwnedText.text = "Stocks Owned: " + stocksOwned.ToString();
        AveragePriceText.text = "AveragePrice: " + averagePrice.ToString("F2");
        StocksAvailable.text = "Stocks Available: " + maxStocks.ToString(); // Update available stocks text
    }

    public void SellStocks()
    {
        if (!float.TryParse(inputFieldStockSell.text, out float stockCount))
        {
            Debug.LogError("Invalid input for stock count!");
            return;
        }

        if (stockCount <= 0 || stockCount > stocksOwned)
        {
            Debug.LogError("Invalid stock count to sell!");
            return;
        }

        float sellPrice = stockCount * currentPrice;
        clickScript.money += (long)sellPrice;
        stocksOwned -= stockCount;
        maxStocks -= stockCount; // Update maxStocks after selling stocks

        // Recalculate average price after selling
        if (stocksOwned > 0)
        {
            averagePrice = ((averagePrice * stocksOwned) - sellPrice) / (stocksOwned - stockCount);
        }
        else
        {
            averagePrice = 0; // Dac? nu mai de?ine?i ac?iuni, pre?ul mediu devine 0
        }

        stockOwnedText.text = "Stocks Owned: " + stocksOwned.ToString();
        AveragePriceText.text = "AveragePrice: " + averagePrice.ToString("F2");
        StocksAvailable.text = "Stocks Available: " + maxStocks.ToString(); // Update available stocks text
    }

    void AutoSave()
    {
        PlayerPrefs.SetFloat("Stock1Price", currentPrice);
        PlayerPrefs.SetFloat("StocksOwned", stocksOwned);
        PlayerPrefs.SetFloat("Stock1AveragePrice", averagePrice);
    }
}
