using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab; // ���� ������
    public RectTransform inventoryPanel; // Inventory Panel
    public int slotCount = 20; // ������ ���� ����
    public int columnCount = 4; // �� �ٿ� ǥ���� ���� ����

    void Start()
    {
        GenerateSlots();
    }

    void GenerateSlots()
    {
        // GridLayoutGroup �ڵ� ����
        GridLayoutGroup grid = inventoryPanel.GetComponent<GridLayoutGroup>();
        float panelWidth = inventoryPanel.rect.width;
        float panelHeight = inventoryPanel.rect.height;

        // ���� ũ�� ���
        float cellWidth = (panelWidth - (grid.spacing.x * (columnCount - 1))) / columnCount;
        float cellHeight = cellWidth; // ���簢�� ����
        grid.cellSize = new Vector2(cellWidth, cellHeight);

        // ���� ����
        for (int i = 0; i < slotCount; i++)
        {
            // ���� ������ ����
            GameObject slotObject = Instantiate(slotPrefab, inventoryPanel);
            Slot slot = slotObject.GetComponent<Slot>();

            // Inventory Ŭ������ slots �迭�� �߰�
            if (Inventory.instance != null)
            {
                // ���� ����Ʈ�� List<Slot>���� �������� ���
                Inventory.instance.slots.Add(slot);
            }
        }
    }
}
