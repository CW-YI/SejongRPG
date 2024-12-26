using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField userGradeInputField;
    public TMP_InputField creditSumInputField;
    public Toggle militaryServiceNeededToggle;
    public Toggle militaryServedToggle;
    public Toggle majorTransferToggle;
    public Button signUpButton;

    private string signUpUrl = "http://your-backend-url.com/api/signup"; // 백엔드 서버의 회원가입 엔드포인트 URL

    // Start is called before the first frame update
    void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
    }

    private void OnSignUpButtonClick()
    {
        string name = nameInputField.text;
        int id = int.Parse(idInputField.text);
        string password = passwordInputField.text;
        int userGrade = int.Parse(userGradeInputField.text);
        int creditSum = int.Parse(creditSumInputField.text);
        bool militaryServiceNeeded = militaryServiceNeededToggle.isOn;
        bool militaryServed = militaryServedToggle.isOn;
        bool majorTransfer = majorTransferToggle.isOn;

        User user = new User
        {
            name = name,
            ID = id,
            password = password,
            userGrade = userGrade,
            creditSum = creditSum,
            militaryServiceNeeded = militaryServiceNeeded,
            militaryServed = militaryServed,
            majorTransfer = majorTransfer
        };

        SendUserData(user);
    }

    public void SendUserData(User user)
    {
        StartCoroutine(SendUserDataCoroutine(user));
    }

    private IEnumerator SendUserDataCoroutine(User user)
    {
        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(signUpUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ProcessServerResponse(request.downloadHandler.text);
        }
    }

    private void ProcessServerResponse(string jsonResponse)
    {
        try
        {
            User user = JsonUtility.FromJson<User>(jsonResponse);
            Debug.Log("User data received:");
            Debug.Log("Name: " + user.name);
            Debug.Log("ID: " + user.ID);
            Debug.Log("User Grade: " + user.userGrade);
            Debug.Log("Credit Sum: " + user.creditSum);
            Debug.Log("Military Service Needed: " + user.militaryServiceNeeded);
            Debug.Log("Military Served: " + user.militaryServed);
            Debug.Log("Major Transfer: " + user.majorTransfer);
            // 추가적인 처리 로직을 여기에 작성
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}
