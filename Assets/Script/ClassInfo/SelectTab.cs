using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTab : MonoBehaviour
{
    [SerializeField] GameObject majorTab;
    [SerializeField] GameObject bsmTab;
   public void MajorTab()
    {
        majorTab.SetActive(true);
        bsmTab.SetActive(false);
    }

    public void BSMTab()
    {
        bsmTab.SetActive(true);
        majorTab.SetActive(false);
    }
}
