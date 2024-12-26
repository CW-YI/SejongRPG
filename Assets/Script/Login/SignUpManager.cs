using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField passwordIdentifyInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField userGradeInputField;
    public TMP_InputField creditSumInputField;
    public TMP_Dropdown majorDropdown;
    public Toggle majorTransferToggle;
    public Button nextButton;
    public Button signUpButton;
    public Button backButton;
    public TextMeshProUGUI mismatchText;
    public TextMeshProUGUI passwordMismatchText;

    private string signUpUrl = "http://sejongrpg.duckdns.org:3000/users";

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
        signUpButton.onClick.AddListener(OnSignUpButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
        mismatchText.gameObject.SetActive(false);
        passwordMismatchText.gameObject.SetActive(false);
        SetStepOneActive(true);
    }

    private void SetStepOneActive(bool isActive)
    {
        idInputField.gameObject.SetActive(isActive);
        passwordInputField.gameObject.SetActive(isActive);
        passwordIdentifyInputField.gameObject.SetActive(isActive);
        nextButton.gameObject.SetActive(isActive);

        nameInputField.gameObject.SetActive(!isActive);
        userGradeInputField.gameObject.SetActive(!isActive);
        creditSumInputField.gameObject.SetActive(!isActive);
        majorDropdown.gameObject.SetActive(!isActive);
        majorTransferToggle.gameObject.SetActive(!isActive);
        signUpButton.gameObject.SetActive(!isActive);
    }

    private void OnNextButtonClick()
    {
        if (passwordInputField.text != passwordIdentifyInputField.text)
        {
            passwordMismatchText.text = "비밀번호가 일치하지 않습니다.\n비밀번호를 수정하세요.";
            passwordMismatchText.gameObject.SetActive(true);
            return;
        }
        passwordMismatchText.gameObject.SetActive(false);
        SetStepOneActive(false);
        signUpButton.GetComponentInChildren<TextMeshProUGUI>().text = "회원가입";
    }

    private void OnSignUpButtonClick()
    {
        string id = idInputField.text;
        string password = passwordInputField.text;
        string name = nameInputField.text;
        int userGrade = int.Parse(userGradeInputField.text);
        int creditSum = int.Parse(creditSumInputField.text);
        string major = majorDropdown.options[majorDropdown.value].text;
        bool majorTransfer = majorTransferToggle.isOn;

        User user = new User
        {
            name = name,
            id = id,
            passwd = password,
            userGrade = userGrade,
            creditSum = creditSum,
            major = major,
            majorTransfer = majorTransfer
        };

        SendUserData(user);
    }

    public void SendUserData(User user)
    {
        StartCoroutine(SendUserDataCoroutine(user));
    }

    private void OnBackButtonClick()
    {
        SceneManager.LoadScene("Login");
    }

    private IEnumerator SendUserDataCoroutine(User user)
    {
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log("JSON Data: " + jsonData);

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
            Debug.Log("ID: " + user.id);
            Debug.Log("User Grade: " + user.userGrade);
            Debug.Log("Credit Sum: " + user.creditSum);
            Debug.Log("Major: " + user.major);
            Debug.Log("Major Transfer: " + user.majorTransfer);
            SceneManager.LoadScene("Login");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("JSON Parsing Error: " + ex.Message);
        }
    }
}
