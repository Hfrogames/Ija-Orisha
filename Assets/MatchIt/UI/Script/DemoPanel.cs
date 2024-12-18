using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DemoPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button button;

    private void Start()
    {
        // Fail("null");
    }

    public void Success(string text)
    {
        title.text = text;
        icon.color = Color.green;
        button.enabled = false;
    }

    public void Normal(string text)
    {
        title.text = text;
        icon.color = Color.grey;
        button.enabled = true;
    }

    public void Fail(string text)
    {
        title.text = text;
        icon.color = Color.red;
        button.enabled = true;
    }
}