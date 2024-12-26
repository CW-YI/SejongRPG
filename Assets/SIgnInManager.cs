using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SignInManager : MonoBehaviour
{
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;

    private string backendUrl = "http://your-backend-url.com/api/user"; // �鿣�� ������ ��������Ʈ URL

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLoginButtonClick()
    {
        string username = idInputField.text;
        string password = passwordInputField.text;

        User user = new User
        {
            username = username,
            password = password,
            name = "Temporary Name", // �ӽ� ��
            year = 1, // �ӽ� ��
            creditsEarned = 0, // �ӽ� ��
            creditsRequired = 120 // �ӽ� ��
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

        UnityWebRequest request = new UnityWebRequest(backendUrl, "POST");
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
            // �߰����� ó�� ������ ���⿡ �ۼ�
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}
