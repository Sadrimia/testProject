using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        try
        {
            Basket.onNumberOfProductsChanged += UpdateQuestText;
        }
        catch
        {
            throw new System.Exception("Basket script don't found.");
        }

    }

    private void OnDisable()
    {
        try
        {
            Basket.onNumberOfProductsChanged -= UpdateQuestText;
        }
        catch
        {
            throw new System.Exception("Basket script don't found.");
        }
    }

    private void UpdateQuestText(int numberOfProducts, string productName)
    {
        _text.text = "Pick up " + numberOfProducts.ToString() + " " + productName;
    }
}
