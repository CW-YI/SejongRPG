using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable] 
public class Class
{
    public string major;
    public string id;
    public string subjectName;         // ������
    public string category;            // �̼� ���� (����, ���� ��)
    public int grade;                   // �г�
    public int credit;                 // ������
    public bool isCompleted;           // ������� ����
    public List<string> otherPoints;   // ����� �ٸ� ������ (�̸� �Ǵ� ID)

    // ������
    public Class(string major, string id, string subjectName, string category, int grade, int credit, bool isCompleted, List<string> otherPoints)
    {
        this.major = major;
        this.id = id;
        this.subjectName = subjectName;
        this.credit = credit;
        this.grade = grade;
        this.isCompleted = isCompleted;
        this.category = category;
        this.otherPoints = otherPoints;
    }
}
