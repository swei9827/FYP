using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using Photon.Pun;
using System.Collections.Generic;   

public class LoginSystem : MonoBehaviour
{
    public InputField user;
    public InputField email;
    public InputField pass;

    public bool isSignup = false;

    private string playFabPlayerIdCache;

    void Start()
    {
        //developer use login
        if (!isSignup)
        {
            email.text = "labtest0@unity.com";
            pass.text = "labtest0";
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {        
        Debug.Log("User : " + email.text + " logged in.");

        LoginToPlayFab();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {            
        Debug.Log("User : " + email.text + " succesfully registered.");

        LoginToPlayFab();     
    }

    private void RequestPhotonToken(LoginResult obj)
    {
        Debug.Log("PlayFab authenticated. Requesting photon token...");
        
        playFabPlayerIdCache = obj.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        }, AuthenticateWithPhoton, OnPlayFabError);
    }

    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        Debug.Log("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        Photon.Realtime.AuthenticationValues authValue = new Photon.Realtime.AuthenticationValues();
        authValue.AuthType = Photon.Realtime.CustomAuthenticationType.Custom;
        authValue.AddAuthParameter("username", playFabPlayerIdCache);
        authValue.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        PhotonNetwork.AuthValues = authValue;

        PhotonNetwork.ConnectUsingSettings();
    }

    private void LoginToPlayFab()
    {
        PlayerPrefs.SetString("Email", email.text);
        PlayerPrefs.SetString("Pass", pass.text);

        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
        {
            Email = email.text,
            Password = pass.text
        }, RequestPhotonToken, OnPlayFabError);

        //SceneManager.LoadScene("Lab_Lobby");
        SceneManager.LoadScene("New Lobby");
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        Debug.Log(obj.GenerateErrorReport());
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = email.text, Password = pass.text, Username = user.text };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = email.text, Password = pass.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }    
}

//private static void StartCloudHelloWorld()
//{
//    PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
//    {
//        FunctionName = "RoomCreated",
//        GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
//    }, OnCloudHelloWorld, OnErrorShared);
//}

//private static void OnCloudHelloWorld(ExecuteCloudScriptResult result)
//{
//    // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
//    PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
//    object messageValue;
//    jsonResult.TryGetValue("messageValue", out messageValue);
//}

//private static void OnErrorShared(PlayFabError error)
//{
//    Debug.Log(error.GenerateErrorReport());
//}