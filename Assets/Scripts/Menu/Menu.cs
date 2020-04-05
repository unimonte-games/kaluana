﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image menuArrowImage;
    public GameObject menuPanel;
    public Sprite upArrowSprite;
    public Sprite downArrowSprite;
    public Sprite yesSprite;
    public Sprite noSprite;
    public Image soundImage;
    public Image musicImage;
    public Image effectsImage;
    public GameObject mapPanel;
    public GameObject profilePanel;
    public GameObject inventoryPanel;
    public GameObject inventoryContentPanel;
    public GameObject settingsPanel;
    public GameObject descriptionPanel;
    public Text descriptionText;
    public Text descriptionStatusText;
    public Text equipButtonText;
    public Button equipButton;
    public Font pixelFont;


    private bool menuIsOpen = false;

    private bool mapPanelIsOpen = false;
    private bool profilePanelIsOpen = false;
    private bool inventoryPanelIsOpen = false;
    private bool settingsPanelIsOpen = false;

    private bool soundIsOn = true;
    private bool musicIsOn = true;
    private bool effectsIsOn = true;

    public delegate void OpeningInventory();
    public static event OpeningInventory OnOpeningInventory;
    public void OpenCloseMenu()
    {
        if (!menuIsOpen)
        {
            menuIsOpen = true;
            menuArrowImage.sprite = upArrowSprite;
            LeanTween.move(menuPanel, menuPanel.transform.position + new Vector3(0, -90, 0), 0.1f);
        }
        else
        {
            menuIsOpen = false;
            menuArrowImage.sprite = downArrowSprite;
            LeanTween.move(menuPanel, menuPanel.transform.position + new Vector3(0, 90, 0), 0.1f);
            CloseOpenedPanels();
        }
    }
    public void CloseOpenedPanels()
    {
        if(mapPanelIsOpen)
            LeanTween.scale(mapPanel, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.1f);
        if(profilePanelIsOpen)
            LeanTween.scale(profilePanel, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.1f);
        if (inventoryPanelIsOpen)
        {
            LeanTween.scale(inventoryPanel, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.1f);
            LeanTween.scale(descriptionPanel, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.1f);
        }
        if(settingsPanelIsOpen)
            LeanTween.scale(settingsPanel, new Vector3(0.0001f, 0.0001f, 0.0001f), 0.1f);

    }

    public void OpenMap()
    {
        CloseOpenedPanels();
        mapPanelIsOpen = true;
        LeanTween.scale(mapPanel, new Vector3(1, 1, 1), 0.1f);
    }

    public void OpenProfile()
    {
        CloseOpenedPanels();
        profilePanelIsOpen = true;
        LeanTween.scale(profilePanel, new Vector3(1, 1, 1), 0.1f);
    }

    public void OpenInventory()
    {
        CloseOpenedPanels();
        inventoryPanelIsOpen = true;
        Debug.Log(inventoryContentPanel.transform.childCount);
        int count = inventoryContentPanel.transform.childCount;
        if (count != 0)
        {
            for (int a = 0; a < count; a++)
            {
               Destroy(inventoryContentPanel.transform.GetChild(a).gameObject);
            }
        }
        Debug.Log(inventoryContentPanel.transform.childCount);

        foreach (Item i in Inventory.instance.items)
        {
            GameObject newItem = new GameObject(i.name);
            var itemText = newItem.AddComponent<Text>();
            var newButton = newItem.AddComponent<Button>();

            itemText.text = $"{i.name} x{i.quantity}";
            itemText.font = pixelFont;
            itemText.color = Color.black;
            itemText.rectTransform.sizeDelta = new Vector2(170, 35);
            itemText.transform.SetParent(newButton.transform);
            newButton.transform.SetParent(inventoryContentPanel.transform);
            newButton.onClick.AddListener(() => ClickOnItem(i));
            
            LeanTween.scale(newItem, new Vector3(1, 1, 1), 0.1f);
        }
        LeanTween.scale(inventoryPanel, new Vector3(1, 1, 1), 0.1f);
        OnOpeningInventory?.Invoke();
    }

    public void ClickOnItem(Item item)
    {
        LeanTween.scale(descriptionPanel, new Vector3(1, 1, 1), 0.1f);
        descriptionText.text = $"{item.name} \n {item.description}";
        if(item is Equip)
        {
            Equip equip = item as Equip;
            descriptionStatusText.text = $"Status \nVida: {equip.health} \nAtaque: {equip.damage} \nDefesa: {equip.defense} \nVelocidade: {equip.speed}";
            equipButton.enabled = true;
            equipButtonText.text = "Equipar";
        }
        else
        {
            descriptionStatusText.text = $"Status \nVida: -- \nAtaque: -- \nDefesa: -- \nVelocidade: --";
            equipButton.enabled = false;
            equipButtonText.text = "";
        }
        
    }


    public void OpenSettings()
    {
        CloseOpenedPanels();
        settingsPanelIsOpen = true;
        LeanTween.scale(settingsPanel, new Vector3(1, 1, 1), 0.1f);
    }

    public void Map_()
    {

    }

    public void Profile_()
    {

    }

    public void Inventory_()
    {

    }

    public void Settings_sound()
    {
        if (soundIsOn)
        {
            soundIsOn = false;
            soundImage.sprite = noSprite;
        }
        else
        {
            soundIsOn = true;
            soundImage.sprite = yesSprite;
        }

    }
    public void Settings_music()
    {
        if (musicIsOn)
        {
            musicIsOn = false;
            musicImage.sprite = noSprite;
        }
        else
        {
            musicIsOn = true;
            musicImage.sprite = yesSprite;
        }
    }
    public void Settings_effects()
    {
        if (effectsIsOn)
        {
            effectsIsOn = false;
            effectsImage.sprite = noSprite;
        }
        else
        {
            effectsIsOn = true;
            effectsImage.sprite = yesSprite;
        }
    }
}
