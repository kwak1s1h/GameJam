using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem Instance;

    public int count = -1;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void AddCombo()
    {
        count++;
        if(count != 0)
            GameManager.Instance.Player.speed += 5f;
        Debug.Log(count);
    }

    public void ResetCombo()
    {
        count = 0;
        GameManager.Instance.Player.speed = 150f;
    }
}
