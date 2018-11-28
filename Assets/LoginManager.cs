using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text; 

public struct LoginData
{
    public string id, pw; 
}

public class LoginManager : MonoBehaviour {


    public void ButtonClicked()
    {
        StartCoroutine(GetText());
    }
    public void PostButtonClicked()
    {
        StartCoroutine(PostText());
    }

    IEnumerator PostText()
    {
        LoginData data = new LoginData { id = "g   oya", pw = "1234" }; 
        
        string postData = JsonUtility.ToJson(data); 
        byte[] sendData = Encoding.UTF8.GetBytes(postData); 
       
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/test/login", sendData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");//바이트 형식이 서버에 제이슨임을 헤더에 명시
            yield return www.Send();

            Debug.Log(www.downloadHandler.text);
        }
    }
    
    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/test/2139439213"))
        
        {
            yield return www.Send();
            Debug.Log(www.downloadHandler.text); 
            
        }
    }
}
