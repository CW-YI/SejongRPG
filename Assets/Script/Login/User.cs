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
//int ID(�й�)
//string Password
//int userGrade(�г�)
//int creditSum(�̼��� ����)
//bool militaryServiceNeeded
//bool militaryServed
//bool majorTransfer