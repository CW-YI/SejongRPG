using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    public InputField nameInputField;
    public InputField idInputField;
    public InputField passwordInputField;
    public InputField passwordIdentifyInputField;
    public InputField userGradeInputField;
    public InputField creditSumInputField;
    public Toggle militaryServiceNeededToggle;
    public Toggle militaryServedToggle;
    public Toggle majorTransferToggle;
    public Button signUpButton;
    public Text mismatchText;
    public Text passwordMismatchText; // ���ο� �ؽ�Ʈ

    private string signUpUrl = "http://your-backend-url.com/api/signup"; // �鿣�� ������ ȸ������ ��������Ʈ URL

    // Start is called before the first frame update
    void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        mismatchText.gameObject.SetActive(false);
        passwordMismatchText.gameObject.SetActive(false); // ���ο� �ؽ�Ʈ �ʱ�ȭ
    }

    private void OnSignUpButtonClick()
    {
        if (passwordInputField.text != passwordIdentifyInputField.text)
        {
            passwordMismatchText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.\n��й�ȣ�� �����ϼ���.";
            passwordMismatchText.gameObject.SetActive(true);
            return;
        }
        passwordMismatchText.gameObject.SetActive(false);

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
            // ȸ�������� ���������� �Ϸ�Ǹ� �α��� ������ ��ȯ
            SceneManager.LoadScene("Login");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}

