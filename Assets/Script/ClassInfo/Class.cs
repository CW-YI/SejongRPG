using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable] 
public class Class
{
    public string id;
    public string subjectName;         // ������
    public int credit;                 // ������
    public bool isCompleted;           // ������� ����
    public string category;            // ���� (����, ���� ��)
    public List<string> otherPoints;   // ����� �ٸ� ������ (�̸� �Ǵ� ID)

    // ������
    public Class(string id, string subjectName, int credit, bool isCompleted, string category, List<string> otherPoints)
    {
        this.id = id;
        this.subjectName = subjectName;
        this.credit = credit;
        this.isCompleted = isCompleted;
        this.category = category;
        this.otherPoints = otherPoints;
    }
}
