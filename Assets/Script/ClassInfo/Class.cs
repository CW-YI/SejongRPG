using System.Collections.Generic;

[System.Serializable] 
public class Class
{
    public string subjectName;         // 교과명
    public int credit;                 // 학점수
    public bool isCompleted;           // 들었는지 여부
    public string category;            // 구분 (전필, 전선 등)
    public List<string> otherPoints;   // 연결된 다른 수업들 (이름 또는 ID)

    public Class(string subjectName, int credit, bool isCompleted, string category, List<string> otherPoints)
    {
        this.subjectName = subjectName;
        this.credit = credit;
        this.isCompleted = isCompleted;
        this.category = category;
        this.otherPoints = otherPoints;
    }
}
