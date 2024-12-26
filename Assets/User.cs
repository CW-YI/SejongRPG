using System;

[Serializable]
public class User
{
    public string name { get; set; }
    public int ID { get; set; }
    public string password { get; set; }
    public int userGrade { get; set; }
    public int creditSum { get; set; }
    public bool militaryServiceNeeded { get; set; }
    public bool militaryServed { get; set; }
    public bool majorTransfer { get; set; }
}

//string name
//int ID(학번)
//string Password
//int userGrade(학년)
//int creditSum(이수한 학점)
//bool militaryServiceNeeded
//bool militaryServed
//bool majorTransfer