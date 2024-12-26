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
    public Text passwordMismatchText; // 비밀번호 불일치 텍스트

    private string loginUrl = "http://sejongrpg.duckdns.org:3000/users"; // 백엔드 서버의 로그인 엔드포인트 URL

    // Start is called before the first frame update
    void Start()
    {
        signInButton.onClick.AddListener(OnSignInButtonClick);
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        passwordMismatchText.gameObject.SetActive(false); // 비밀번호 불일치 텍스트 초기화
    }

    private void OnSignInButtonClick()
    {
        string id = idInputField.text;
        string password = passwordInputField.text;

        User user = new User
        {
            id = id,
            passwd = password
        };

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

        UnityWebRequest request = new UnityWebRequest(loginUrl, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("id", user.id);
        request.SetRequestHeader("passwd", user.passwd);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Result: " + request.result);
            passwordMismatchText.gameObject.SetActive(true);
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
            Debug.Log("ID: " + user.id);
            Debug.Log("User Grade: " + user.userGrade);
            Debug.Log("Credit Sum: " + user.creditSum);
            Debug.Log("Major: " + user.major);
            Debug.Log("Major Transfer: " + user.majorTransfer);
            // 로그인 성공 시 스킬 시스템 씬으로 전환
            passwordMismatchText.gameObject.SetActive(false);
            SceneManager.LoadScene("SkillSystem");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}

