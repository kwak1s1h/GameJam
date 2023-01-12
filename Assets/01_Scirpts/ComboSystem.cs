using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem Instance;

    public int count = -1;

    [SerializeField] private TextMeshProUGUI comboTxt;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void AddCombo()
    {
        count++;
        if(count >= 1) comboTxt.gameObject.SetActive(true);
        comboTxt.text = $"{count} combo";
        if(count != 0)
            GameManager.Instance.Player.speed += 7.5f;
        Debug.Log(count);
    }

    public void ResetCombo()
    {
        count = 0;
        comboTxt.gameObject.SetActive(false);
        GameManager.Instance.Player.speed = 150f;
    }
}
