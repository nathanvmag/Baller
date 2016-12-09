using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class IAPmanager : MonoBehaviour, IStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        public static IAPmanager Instance { set; get; }
        [SerializeField]
        GameObject errorDisplay, sucessDisplay;
        public static string Product_25diamond = "diamond25";
        public static string Product_100diamond = "diamond100";
        public static string Product_250diamond = "diamond250";
        public static string Product_500diamond = "diamond500";
        public static string Product_750diamond = "diamond750";
        public static string Product_1000diamond = "diamond1000";
        public static string Product_2500diamond = "diamond2500";
        GameManager gm;
        public List<string> products = new List<string>();
        void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            gm = Camera.main.GetComponent<GameManager>();
            products = new List<string>();
            products.Add(Product_25diamond);
            products.Add(Product_100diamond);
            products.Add(Product_250diamond);
            products.Add(Product_500diamond);
            products.Add(Product_750diamond);
            products.Add(Product_1000diamond);
            products.Add(Product_2500diamond);
           
            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());           
            builder.AddProduct(Product_25diamond, ProductType.Consumable);
            builder.AddProduct(Product_100diamond, ProductType.Consumable);
            builder.AddProduct(Product_250diamond, ProductType.Consumable);
            builder.AddProduct(Product_500diamond, ProductType.Consumable);
            builder.AddProduct(Product_750diamond, ProductType.Consumable);  
            builder.AddProduct(Product_1000diamond, ProductType.Consumable);
            builder.AddProduct(Product_2500diamond, ProductType.Consumable);  
       
         

            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void BuyConsumable(string s)
        {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(s);
        }
       
       private void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                   // Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    errorDisplay.SetActive(true);
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
                errorDisplay.SetActive(true);
            }
        }              

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
            errorDisplay.SetActive(true);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            // A consumable product has been purchased by this user.
           if (checkall(args)){
               sucessDisplay.SetActive(true);
               sucessDisplay.transform.FindChild("Diamonds").GetComponent<Text>().text = "You have: " + gm.SetDiamonds.ToString();
           }           
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                errorDisplay.SetActive(true);
            }
            
           

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }

      
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            errorDisplay.SetActive(true);
        }
        private bool checkall(PurchaseEventArgs args )
        {
            foreach (string s in products)
            {
                if (String.Equals(args.purchasedProduct.definition.id, s, StringComparison.Ordinal))
                {
                    // Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    
                    // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                    if (s.Equals(products[0]))
                    {
                        Debug.Log("25diamonds good");
                        gm.SetDiamonds += 25;
                    }
                    if (s.Equals(products[1]))
                    {
                        Debug.Log("100diamonds good");
                        gm.SetDiamonds += 100;
                    }
                    if (s.Equals(products[2]))
                    {
                        Debug.Log("250diamonds good");
                        gm.SetDiamonds += 250;
                    }
                    if (s.Equals(products[3]))
                    {
                        Debug.Log("500diamonds good");
                        gm.SetDiamonds += 500;

                    }
                    if (s.Equals(products[4]))
                    {
                        Debug.Log("750diamonds good");
                        gm.SetDiamonds += 750;
                    }
                    if (s.Equals(products[5]))
                    {
                        Debug.Log("1000diamonds good");
                        gm.SetDiamonds += 1000;
                    }
                    if (s.Equals(products[6]))
                    {
                        Debug.Log("2500diamonds good");
                        gm.SetDiamonds += 2500;
                    }
                    return true;
                }
            }
            return false;
        }
        public void buyBt (int id)
        {
            string buyid = products[id];
            IAPmanager.Instance.BuyProductID(buyid);

        }
        public void Close(GameObject eu)
        {
            eu.SetActive(false);
        }
      

      
    }
