using HmsPlugin;
using HuaweiMobileServices.AuthService;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HuaweiMobileServices.Base;
using Facebook.Unity;
using System.Collections.Generic;

public class AuthServiceManager : MonoBehaviour
{
    private HMSAuthServiceManager authServiceManager = null;

    public GameObject loginWithPhone, registerWithPhone;
    public GameObject loginWithEmail, registerWithEmail;
    public Text errorLine;

    private readonly string Tag = "MagicGame";

    private InputField PhoneCountryCode, PhoneNumber, PhonePassword, registerPhoneCountryCode, registerPhoneNumber, registerPhonePassword, registerPhoneVerifyCode;
    private InputField Email, EmailPassword, registerEmail, registerEmailVerifyCode, registerEmailPassword;

    public void Awake()
    {
        Email = loginWithEmail.transform.Find("Email").GetComponent<InputField>();
        EmailPassword = loginWithEmail.transform.Find("EmailPassword").GetComponent<InputField>();
        registerEmail = registerWithEmail.transform.Find("registerEmail").GetComponent<InputField>();
        registerEmailVerifyCode = registerWithEmail.transform.Find("registerEmailVerifyCode").GetComponent<InputField>();
        registerEmailPassword = registerWithEmail.transform.Find("registerEmailPassword").GetComponent<InputField>();
        
        PhoneCountryCode = loginWithPhone.transform.Find("PhoneCountryCode").GetComponent<InputField>();
        PhoneNumber = loginWithPhone.transform.Find("PhoneNumber").GetComponent<InputField>();
        PhonePassword = loginWithPhone.transform.Find("PhonePassword").GetComponent<InputField>();
        registerPhoneCountryCode = registerWithPhone.transform.Find("registerPhoneCountryCode").GetComponent<InputField>();
        registerPhoneNumber = registerWithPhone.transform.Find("registerPhoneNumber").GetComponent<InputField>();
        registerPhonePassword = registerWithPhone.transform.Find("registerPhonePassword").GetComponent<InputField>();
        registerPhoneVerifyCode = registerWithPhone.transform.Find("registerPhoneVerifyCode").GetComponent<InputField>();

        // Facebook
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void Start()
    {
        authServiceManager = HMSAuthServiceManager.Instance;
        authServiceManager.OnSignInSuccess = OnAuthSericeSignInSuccess;
        authServiceManager.OnSignInFailed = OnAuthSericeSignInFailed;
        authServiceManager.OnCreateUserSuccess = OnAuthSericeCreateUserSuccess;
        authServiceManager.OnCreateUserFailed = OnAuthSericeCreateUserFailed;

        if (authServiceManager.GetCurrentUser() != null)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnAuthSericeSignInSuccess(SignInResult signInResult)
    {
        SetErrorLine("");
        CloseAllPanel();
        SceneManager.LoadScene("MainScene");
    }

    private void OnAuthSericeSignInFailed(HMSException error)
    {
        SetErrorLine(error.WrappedCauseMessage);
        Debug.Log($"{Tag} => OnAuthSericeSignInFailed => {error}");
        CloseAllPanel();
    }
    
    private void OnAuthSericeCreateUserSuccess(SignInResult signInResult)
    {
        SetErrorLine("");
        CloseAllPanel();
        SceneManager.LoadScene("MainScene");
    }

    private void OnAuthSericeCreateUserFailed(HMSException error)
    {
        SetErrorLine(error.WrappedCauseMessage);
        Debug.Log($"{Tag} => OnAuthSericeCreateUserFailed => {error}");
        CloseAllPanel();
    }

    private void OnAccountKitLoginSuccess(AuthAccount authHuaweiId)
    {
        AGConnectAuthCredential credential = HwIdAuthProvider.CredentialWithToken(authHuaweiId.AccessToken);
        authServiceManager.SignIn(credential);
    }

    public void SignInWithHuaweiAccount()
    {
        HMSAccountManager.Instance.OnSignInSuccess = OnAccountKitLoginSuccess;
        HMSAccountManager.Instance.OnSignInFailed = OnAuthSericeSignInFailed;
        HMSAccountManager.Instance.SignIn();
    }

    public void SignInAnonymously() => authServiceManager.SignInAnonymously();

    public void SignInWithPhone()
    {
        AGConnectAuthCredential phoneAuthCredential = PhoneAuthProvider.CredentialWithPassword(PhoneCountryCode.text, PhoneNumber.text, PhonePassword.text);
        authServiceManager.SignIn(phoneAuthCredential);
    }

    public void RequestVerifyCodeWithPhoneNumber()
    {
        VerifyCodeSettings verifyCodeSettings = new VerifyCodeSettings.Builder()
            .Action(VerifyCodeSettings.ACTION_REGISTER_LOGIN)
            .Locale(Locale.GetDefault())
            .SendInterval(60).Build();

        PhoneAuthProvider.RequestVerifyCode(registerPhoneCountryCode.text, registerPhoneNumber.text, verifyCodeSettings)
            .AddOnSuccessListener(verifyCodeResult =>
            {
                Debug.Log($"{Tag} Phone Verification Code Sent");
            })
            .AddOnFailureListener(exception =>
            {
                Debug.Log($"{Tag} => RequestVerifyCodeWithPhoneNumber {exception}");
                SetErrorLine(exception.WrappedCauseMessage);
                CloseAllPanel();
            });
    }

    public void RegisterWithPhoneNumber()
    {
        PhoneUser phoneUser = new PhoneUser.Builder()
            .SetCountryCode(registerPhoneCountryCode.text)
            .SetPhoneNumber(registerPhoneNumber.text)
            .SetPassword(registerPhonePassword.text)
            .SetVerifyCode(registerPhoneVerifyCode.text)
            .Build();
        authServiceManager.CreateUser(phoneUser);
    }

    public void SignInWithEmail()
    {
        AGConnectAuthCredential emailAuthCredential = EmailAuthProvider.CredentialWithPassword(Email.text, EmailPassword.text);
        authServiceManager.SignIn(emailAuthCredential);    
    }

    public void RequestVerifyCodeWithEmail()
    {
        VerifyCodeSettings verifyCodeSettings = new VerifyCodeSettings.Builder()
            .Action(VerifyCodeSettings.ACTION_REGISTER_LOGIN)
            .Locale(Locale.GetDefault())
            .SendInterval(30).Build();

        EmailAuthProvider.RequestVerifyCode(registerEmail.text, verifyCodeSettings)
            .AddOnSuccessListener(result =>
            {
                Debug.Log($"{Tag} Email Verification Code Sent");
            })
            .AddOnFailureListener(error =>
            {
                Debug.Log($"{Tag} => RequestVerifyCodeWithEmail {error}");
                SetErrorLine(error.WrappedCauseMessage);
                CloseAllPanel();
            });
    }

    public void RegisterWithEmail()
    {
        EmailUser emailUser = new EmailUser.Builder()
            .SetEmail(registerEmail.text)
            .SetVerifyCode(registerEmailVerifyCode.text)
            .SetPassword(registerEmailPassword.text)
            .Build();
        authServiceManager.CreateUser(emailUser);
    }

    public void OpenPanel(string panel)
    {
        switch (panel)
        {
            case "loginWithEmail":
                loginWithEmail.SetActive(true);
                break;
            case "registerWithEmail":
                registerWithEmail.SetActive(true);
                break;
            case "loginWithPhone":
                loginWithPhone.SetActive(true);
                break;
            case "registerWithPhone":
                registerWithPhone.SetActive(true);
                break;
            default:
                Debug.Log($"{Tag} => Open Panel Error");
                break;
        }
    }

    public void ClosePanel(string panel)
    {
        switch (panel)
        {
            case "loginWithEmail":
                loginWithEmail.SetActive(false);
                break;
            case "registerWithEmail":
                registerWithEmail.SetActive(false);
                break;
            case "loginWithPhone":
                loginWithPhone.SetActive(false);
                break;
            case "registerWithPhone":
                registerWithPhone.SetActive(false);
                break;
            default:
                Debug.Log($"{Tag} => Close Panel Error");
                break;
        }
    }

    private void CloseAllPanel()
    {
        loginWithEmail.SetActive(false);
        registerWithEmail.SetActive(false);
        loginWithPhone.SetActive(false);
        registerWithPhone.SetActive(false);
    }

    public void DeleteUser()
    {
        authServiceManager.DeleteUser();
    }

    public void SignOut()
    {
        authServiceManager.SignOut();
    }

    private void SetErrorLine(string error) => errorLine.text = $"{Tag} => {error}";

    // Facebook SDK
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log($"{Tag} => Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FbLogin()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            SignInWithFacebook(Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
        }
        else
        {
            Debug.Log($"{Tag} User cancelled login");
        }
    }
       
    private void SignInWithFacebook(string accessToken)
    {
        AGConnectAuthCredential credential = FacebookAuthProvider.CredentialWithToken(accessToken);
        authServiceManager.SignIn(credential);
    }

}
