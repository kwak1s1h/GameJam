using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FirstScreenUI : MonoBehaviour
{
    private TextField nameTxt;
    private Button applyBtn;

    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }
    
    private void OnEnable()
    {
        VisualElement root = document.rootVisualElement;
        nameTxt = root.Q<TextField>("text_field");
        applyBtn = root.Q<Button>("apply_btn");
        // applyBtn. = ;
        Debug.Log(nameTxt);
        
        nameTxt.RegisterCallback<ChangeEvent<string>>(e => {
            if(e.newValue == null || e.newValue == "")
            {
                applyBtn.parent.RemoveFromClassList("show");
            }
            else
            {
                applyBtn.parent.AddToClassList("show");
                // applyBtn.clickable = true;
            }
        });
    }
}
