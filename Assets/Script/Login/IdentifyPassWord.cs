using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IdentifyPassWord : MonoBehaviour
{
    public InputField passwordInputField;
    public InputField passwordIdentifyInputField;
    public Text mismatchText;

    // Start is called before the first frame update
    void Start()
    {
        mismatchText.gameObject.SetActive(false);
        passwordIdentifyInputField.onEndEdit.AddListener(delegate { CheckPasswordMatch(); });
    }

    private void CheckPasswordMatch()
    {
        if (passwordInputField.text != passwordIdentifyInputField.text)
        {
            mismatchText.gameObject.SetActive(true);
        }
        else
        {
            mismatchText.gameObject.SetActive(false);
        }
    }

    public bool IsPasswordMatching()
    {
        return passwordInputField.text == passwordIdentifyInputField.text;
    }
}
