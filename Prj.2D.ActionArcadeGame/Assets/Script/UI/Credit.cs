using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    public GameObject creditUI;

    public void CreditShow()
    {
        creditUI.GetComponent<CanvasGroup>().alpha = 1;
        creditUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CreditDisappear()
    {
        creditUI.GetComponent<CanvasGroup>().alpha = 0;
        creditUI.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
