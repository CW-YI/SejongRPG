using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public InputField idInputField;
    public InputField passwordInputField;
    public Button signInButton;
    public Button signUpButton;
    public Text passwordMismatchText; // ��й�ȣ ����ġ �ؽ�Ʈ

    private string loginUrl = "http://your-backend-url.com/api/login"; // �鿣�� ������ �α��� ��������Ʈ URL

    // Start is called before the first frame update
    void Start()
    {
        signInButton.onClick.AddListener(OnSignInButtonClick);
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        passwordMismatchText.gameObject.SetActive(false); // ��й�ȣ ����ġ �ؽ�Ʈ �ʱ�ȭ
    }

    private void OnSignInButtonClick()
    {
        int id = int.Parse(idInputField.text);
        string password = passwordInputField.text;

        User user = new User
        {
            ID = id,
            password = password
        };

        // ��й�ȣ ����ġ üũ
        if (!IsPasswordMatching(user))
        {
            passwordMismatchText.gameObject.SetActive(true);
            return;
        }

        SendUserData(user);
    }

    private void OnSignUpButtonClick()
    {
        SceneManager.LoadScene("SignUp");
    }

    public void SendUserData(User user)
    {
        StartCoroutine(SendUserDataCoroutine(user));
    }

    private IEnumerator SendUserDataCoroutine(User user)
    {
        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
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
            // �α��� ���� �� ��ų �ý��� ������ ��ȯ
            passwordMismatchText.gameObject.SetActive(false);
            SceneManager.LoadScene("SkillSystem");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }

    private bool IsPasswordMatching(User user)
    {
        // �������� ��й�ȣ�� Ȯ���ϴ� ������ �߰��ؾ� �մϴ�.
        // ���⼭�� ���÷� ��й�ȣ�� "password"�� ��ġ�ϴ��� Ȯ���մϴ�.
        return user.password == "password";
    }
}

