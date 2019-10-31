using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button TargetButton;
    public bool IsMenuOpen;
    public float OpenCloseTime = 0.4f;

    //public UnityEvent OpenMenuEvent;
    //public UnityEvent CloseMenuEvent;
    [Tooltip("The panel to open when the menu button is clicked")]
    public OpenClose ToOpenOnMenuClick;
    [Tooltip("The panel to close when the menu button is clicked")]
    public OpenClose ToCloseOnMenuClick;
    [Tooltip("The overlay panel to open when opening the menu")]
    public OpenClose OverlayOnMenuOpen;

    public Sprite OpenSprite;
    public Sprite CloseSprite;

    private void Start()
    {
        //To work properly the 3 open close elements should have the same duration

        ToOpenOnMenuClick.OpenCloseTime = OpenCloseTime;
        ToCloseOnMenuClick.OpenCloseTime = OpenCloseTime;
        OverlayOnMenuOpen.OpenCloseTime = OpenCloseTime;

        ToOpenOnMenuClick.EventOnOpenStart.AddListener(() =>
        {
            OverlayOnMenuOpen.Open();
            TargetButton.interactable = false;
            TargetButton.image.sprite = OpenSprite;
            //Debug.Log("EventOnOpenStart");
        });

        ToOpenOnMenuClick.EventOnOpenEnd.AddListener(() =>
        {
            TargetButton.interactable = true;
        });

        ToOpenOnMenuClick.EventOnCloseStart.AddListener(() =>
        {
            TargetButton.interactable = false;
            OverlayOnMenuOpen.Close();
        });

        ToOpenOnMenuClick.EventOnCloseEnd.AddListener(() =>
        {

            TargetButton.image.sprite = CloseSprite;
            TargetButton.interactable = true;
            // Debug.Log("EventOnCloseEnd");
        });

        /*ToCloseOnMenuClick.EventOnOpenStart.AddListener(() =>
        {
            
        });

        ToCloseOnMenuClick.EventOnOpenEnd.AddListener(() =>
        {

        });

        ToCloseOnMenuClick.EventOnCloseStart.AddListener(() =>
        {

        });

        ToCloseOnMenuClick.EventOnCloseEnd.AddListener(() =>
        {

        });
        */

        TargetButton.onClick.AddListener(() =>
        {
            OpenCloseMenu();
        });

        /*
        TargetButton.onClick.AddListener(() =>
        {            
            if (IsMenuOpen)
            {
                //Debug.Log("closing menu!");
                CloseMenuEvent.Invoke();
                TargetButton.image.sprite = CloseSprite;
            }
            else
            {
                //Debug.Log("opening menu!");
                OpenMenuEvent.Invoke();
                TargetButton.image.sprite = OpenSprite;
            }

            IsMenuOpen = !IsMenuOpen;
        });
        */


    }
    public void OpenCloseMenu()
    {
        if (IsMenuOpen)
        {
            ToCloseOnMenuClick.Open();
            ToOpenOnMenuClick.Close();
        }
        else
        {
            ToCloseOnMenuClick.Close();
            ToOpenOnMenuClick.Open();
        }

        IsMenuOpen = !IsMenuOpen;
    }
}
