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
        lr.positionCount = 2; // ���� �� ������ ����
        lr.SetPosition(0, start); // ������
        lr.SetPosition(1, end);   // ����
    }
}
