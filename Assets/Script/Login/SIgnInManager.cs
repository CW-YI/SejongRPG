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

    private string loginUrl = "http://your-backend-url.com/api/login"; // 백엔드 서버의 로그인 엔드포인트 URL

    // Start is called before the first frame update
    void Start()
    {
        signInButton.onClick.AddListener(OnSignInButtonClick);
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        passwordMismatchText.gameObject.SetActive(false); // 비밀번호 불일치 텍스트 초기화
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

        // 비밀번호 불일치 체크
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
            // 로그인 성공 시 스킬 시스템 씬으로 전환
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
        // 서버에서 비밀번호를 확인하는 로직을 추가해야 합니다.
        // 여기서는 예시로 비밀번호가 "password"와 일치하는지 확인합니다.
        return user.password == "password";
    }
}

