using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour
{
    public Dropdown languageDropdown;
    public Text textIndonesia;
    public Text textManado;

    private void Start()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        if (languageDropdown.value == 0)
        {
            textIndonesia.gameObject.SetActive(true);
            textManado.gameObject.SetActive(false);
        }
        else if (languageDropdown.value == 1)
        {
            textIndonesia.gameObject.SetActive(false);
            textManado.gameObject.SetActive(true);
        }
    }
}
