using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField yearInputField;
    public TMP_InputField creditsRequiredInputField;
    public Button signUpButton;

    private string signUpUrl = "http://your-backend-url.com/api/signup"; // 백엔드 서버의 회원가입 엔드포인트 URL

    // Start is called before the first frame update
    void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
    }

    private void OnSignUpButtonClick()
    {
        string username = idInputField.text;
        string password = passwordInputField.text;
        string name = nameInputField.text;
        int year = int.Parse(yearInputField.text);
        int creditsRequired = int.Parse(creditsRequiredInputField.text);

        User user = new User
        {
            username = username,
            password = password,
            name = name,
            year = year,
            creditsRequired = creditsRequired,
            creditsEarned = 0 // 회원가입 시 이수 학점은 0으로 초기화
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
            Debug.Log("Username: " + user.username);
            Debug.Log("Name: " + user.name);
            Debug.Log("Year: " + user.year);
            Debug.Log("Credits Earned: " + user.creditsEarned);
            Debug.Log("Credits Required: " + user.creditsRequired);
            // 추가적인 처리 로직을 여기에 작성
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}
