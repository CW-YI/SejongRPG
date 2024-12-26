using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTotalInfo : MonoBehaviour
{
    public static ClassTotalInfo instance;
    public List<Class> totalClasses;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }
}
