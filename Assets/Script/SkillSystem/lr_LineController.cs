using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Vector3 start, Vector3 end)
    {
        lr.positionCount = 2; // 선은 두 점으로 구성
        lr.SetPosition(0, start); // 시작점
        lr.SetPosition(1, end);   // 끝점
    }
}
