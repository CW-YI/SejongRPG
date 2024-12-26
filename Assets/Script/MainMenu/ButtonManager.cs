using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject CalenderTab;
    [SerializeField] GameObject SkillTab;
    [SerializeField] GameObject InvenctoryTab;
    [SerializeField] Transform cam;

    public bool isDragAvailable = false;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    public void SwitchToSkillTab()
    {
        MainMenu.SetActive(false);
        SkillTab.SetActive(true);
        isDragAvailable = true;
    }
    public void SwitchToCalenderTab()
    {
        MainMenu.SetActive(false);
        CalenderTab.SetActive(true);
    }
    public void SwitchToInvenctoryTab()
    {
        MainMenu.SetActive(false);
        InvenctoryTab.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        CalenderTab.SetActive(false);
        SkillTab.SetActive(false);
        InvenctoryTab.SetActive(false);
        MainMenu.SetActive(true);
        isDragAvailable = false;
        cam.position = new Vector3(0f, 0f, -10f);
    }
}
