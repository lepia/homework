using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public struct SignUpForm
{
    public string username;
    public string password;
    public string nickname;
    public int score;
}

public class UiManager : MonoBehaviour {

    public Image signupPannel;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public InputField nicknameInputField;

    public GameObject uiPannel; 
    
	// Use this for initialization
	void Start ()
    {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void OnClickSignUpButton()
    {
        //signupPannel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        uiPannel.gameObject.SetActive(true);

    }
    
    public void OnClickConfirmButton() 
    {
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string username = usernameInputField.text;
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)
            || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(nickname))
        {
            return;
        }
        if ( password.Equals(confirmPassword))
        {
            //TODO: 서버에 회원가입 정보 전송
            SignUpForm signupForm = new SignUpForm();
            signupForm.username = username;
            signupForm.password = password;
            signupForm.nickname = nickname;

            StartCoroutine(SignUp(signupForm));
            SceneManager.LoadScene("LogInScene");
        }

    }

    // 취소 버튼 이벤트
    public void OnClickCancel()
    {
        //signupPannel.GetComponent<RectTransform>().anchoredPosition = new Vector2(900, 0);
        uiPannel.gameObject.SetActive(false);
    }

    IEnumerator SignUp(SignUpForm form)
    {
        string postData = JsonUtility.ToJson(form);
        byte[] sendData = Encoding.UTF8.GetBytes(postData);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/users/add", sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
