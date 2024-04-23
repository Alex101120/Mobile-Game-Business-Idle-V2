using UnityEngine;
using TMPro;

public class RNGScript : MonoBehaviour
{
    public float startPrice = 100;
    public float maxChange = 5;
    public float minChange = -5;
    private float currentPrice;
    private float stocksOwned;
    private float averagePrice;
    public TMP_Text stockPriceText;
    public TMP_Text stockOwnedText;
    public TMP_Text ProfitText;
    public TMP_Text AveragePriceText;
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
            currentPrice = startPrice;
        }

        if (PlayerPrefs.HasKey("StocksOwned"))
        {
            stocksOwned = PlayerPrefs.GetFloat("StocksOwned");
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

        InvokeRepeating("UpdatePrice", 0f, 30f);
    }

    void UpdatePrice()
    {
        AutoSave();
        float change = Random.Range(minChange, maxChange);
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

        

        Debug.Log("Current stock price: " + currentPrice);
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

        clickScript.money -= (long)totalPrice;
        float totalStocksBought = stockCount + stocksOwned;
        averagePrice = (averagePrice * stocksOwned + currentPrice * stockCount) / totalStocksBought;

        stocksOwned = totalStocksBought;
        stockOwnedText.text = "Stocks Owned: " + stocksOwned.ToString();
        AveragePriceText.text = "AveragePrice" + averagePrice.ToString();
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

        // Update UI text for stocks owned
        stockOwnedText.text = "Stocks Owned: " + stocksOwned.ToString();
    }
    void AutoSave()
    {
        PlayerPrefs.SetFloat("Stock1Price", currentPrice);
        PlayerPrefs.SetFloat("StocksOwned", stocksOwned);
        PlayerPrefs.SetFloat("Stock1AveragePrice", averagePrice);
    }
}