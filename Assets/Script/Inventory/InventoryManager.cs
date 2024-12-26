using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab; // 슬롯 프리팹
    public RectTransform inventoryPanel; // Inventory Panel
    public int slotCount = 20; // 생성할 슬롯 개수
    public int columnCount = 4; // 한 줄에 표시할 슬롯 개수

    void Start()
    {
        GenerateSlots();
    }

    void GenerateSlots()
    {
        // GridLayoutGroup 자동 설정
        GridLayoutGroup grid = inventoryPanel.GetComponent<GridLayoutGroup>();
        float panelWidth = inventoryPanel.rect.width;
        float panelHeight = inventoryPanel.rect.height;

        // 슬롯 크기 계산
        float cellWidth = (panelWidth - (grid.spacing.x * (columnCount - 1))) / columnCount;
        float cellHeight = cellWidth; // 정사각형 슬롯
        grid.cellSize = new Vector2(cellWidth, cellHeight);

        // 슬롯 생성
        for (int i = 0; i < slotCount; i++)
        {
            // 슬롯 프리팹 생성
            GameObject slotObject = Instantiate(slotPrefab, inventoryPanel);
            Slot slot = slotObject.GetComponent<Slot>();

            // Inventory 클래스의 slots 배열에 추가
            if (Inventory.instance != null)
            {
                // 슬롯 리스트를 List<Slot>으로 변경했을 경우
                Inventory.instance.slots.Add(slot);
            }
        }
    }
}
