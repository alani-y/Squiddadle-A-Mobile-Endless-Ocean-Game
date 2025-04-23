using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System.Threading.Tasks;

public class shopManager : MonoBehaviour
{
    public GameObject shopObject;
    public gameManager gameManager;

    async void Awake()
    {
        await InitializeUGS();
    }

    async Task InitializeUGS()
    {
        try
        {
            var options = new InitializationOptions();
            // options.SetEnvironmentName("production"); // Optional

            await UnityServices.InitializeAsync(options);
            Debug.Log("Unity Gaming Services initialized.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("UGS failed to initialize: " + e.Message);
        }
    }

    void Start()
    {
        shopObject.SetActive(false);
    }

    public void revealShop(){
        shopObject.SetActive(true);
    }

    public void hideShop(){
        shopObject.SetActive(false);
    }

    public void OnPurchaseComplete(){

        if (CodelessIAPStoreListener.Instance != null)
        {
            CodelessIAPStoreListener.Instance.InitiatePurchase("alt_squid");
            gameManager.addAlternateSquid();
        }
        Debug.Log("Successful purchase!");
    }

    public void OnProductFetched(Product purchase){
        Debug.Log(purchase.definition.payout.subtype);
        Debug.Log("Enjoy your product!");
    }

    public void test(){
        Debug.Log("testing");
    }
}
