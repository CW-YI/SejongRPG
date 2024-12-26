using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable] 
public class Class
{
    public string major;
    public string id;
    public string subjectName;         // 교과명
    public string category;            // 이수 구분 (전필, 전선 등)
    public int grade;                   // 학년
    public int credit;                 // 학점수
    public bool isCompleted;           // 들었는지 여부
    public List<string> otherPoints;   // 연결된 다른 수업들 (이름 또는 ID)

    // 생성자
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
