using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnClick;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.Instance.OnPlay)
        {
            OnClick?.Invoke();
        }
    }
}
