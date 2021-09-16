#  Unity Mobile Reference Game with Auth Service

This reference project is created for that to show features HMS Auth Service. In this project was used several authentication modes.
* Facebook
* Huawei Account
* Mobile Number
* Email Adress
* Anonymous Account

# Game - (Magic Game)
This Project consists of 2 scenes with [HMS Unity Plugin](https://github.com/EvilMindDevs/hms-unity-plugin). In the first scene, it contains  authentication modes. In the second scene, after the login, it contains the information from the auth service.
![image](https://user-images.githubusercontent.com/32878124/128505959-7fca240d-f9c2-4048-957e-2a92f7339232.png)

## Auth Service Features
Auth Service supports multiple authentication modes, and provides a powerful management console, enabling you to easily develop and manage user authentication.

By integrating the Auth Service SDK into your app, you can easily and quickly provide functions such as registration and sign-in for your users. You can choose to provide your users with one or more of the following authentication modes.

* Mobile number
* Email address
* Third-party accounts
    * HUAWEI ID
    * HUAWEI GameCenter account
    * WeChat account
    * QQ account
    * Weibo account
    * Apple ID
    * Google account
    * Google Play Games account
    * Facebook account
    * Twitter account
* Anonymous account
* Self-owned account

## Requirements
Android SDK min 21
Net 4.x

## Important
This plugin supports:
* Unity version 2019, 2020 - Developed in master Branch
* Unity version 2018 - Developed in 2.0-2018 Branch

**Make sure to download the corresponding unity package for the Unity version you are using from the release section**

## Troubleshooting
Please check our [wiki page](https://github.com/EvilMindDevs/hms-unity-plugin/wiki/Troubleshooting)

## Status
This is an ongoing project, currently WIP. Feel free to contact us if you'd like to collaborate and use Github issues for any problems you might encounter. We'd try to answer in no more than a working day.

## Connect your game Huawei Mobile Services in 5 easy steps

1. Register your app at Huawei Developer
2. Import the Plugin to your Unity project
3. Connect your game with the HMS Kit Managers

### 1 - Register your app at Huawei Developer

#### 1.1-  Register at [Huawei Developer](https://developer.huawei.com/consumer/en/)

#### 1.2 - Create an app in AppGallery Connect.
During this step, you will create an app in AppGallery Connect (AGC) of HUAWEI Developer. When creating the app, you will need to enter the app name, app category, default language, and signing certificate fingerprint. After the app has been created, you will be able to obtain the basic configurations for the app, for example, the app ID and the CPID.

1. Sign in to Huawei Developer and click **Console**.
2. Click the under **Ecosystem services**, click on **App Services**.
3. Click on the **AppGallery Connect** under Distribution and Promotion.
4. Click **My apps**.
5. On the displayed **My apps** page, click **New app** on top right.
6. Enter the App name, select App category (Game), and select Default language as needed.
7. Upon successful app creation, the App information page will automatically display. There you can find the App ID that is assigned by the system to your app.

#### 1.3 Add Package Name
Set the package name of the created application on the AGC.

1. In app information page, there is a label at top saying **"My Apps"**. Mouse hover on it and select **My Project**. This will lead you to the project information of your application
2. You should see a pop up asking about your package name for the application. Select **Manually enter a package name**
3. Fill in the application package name in the input box and click save.

> Your package name should end in .huawei in order to release in App Gallery

#### Generate a keystore.

Create a keystore using Unity or Android Tools. make sure your Unity project uses this keystore under the **Build Settings>PlayerSettings>Publishing settings**


#### Generate a signing certificate fingerprint.

During this step, you will need to export the SHA-256 fingerprint by using keytool provided by the JDK and signature file.

1. Open the command window or terminal and access the bin directory where the JDK is installed.
2. Run the keytool command in the bin directory to view the signature file and run the command.

    ``keytool -list -v -keystore D:\Android\WorkSpcae\HmsDemo\app\HmsDemo.jks``
3. Enter the password of the signature file keystore in the information area. The password is the password used to generate the signature file.
4. Obtain the SHA-256 fingerprint from the result. Save for next step.


#### Add fingerprint certificate to AppGallery Connect
During this step, you will configure the generated SHA-256 fingerprint in AppGallery Connect.

1. In AppGallery Connect, go to **My Project** and select your project.
2. Go to the App information section, click on **+** button and enter the SHA-256 fingerprint that you generated earlier.
3. Click √ to save the fingerprint.

____

### 2 - Import the plugin to your Unity Project

To import the plugin:

1. Download the [.unitypackage](https://github.com/EvilMindDevs/hms-unity-plugin/releases)
2. Open your game in Unity
3. Choose Assets> Import Package> Custom
![Import Package](http://evil-mind.com/huawei/images/importCustomPackage.png "Import package")
4. In the file explorer select the downloaded HMS Unity plugin. The Import Unity Package dialog box will appear, with all the items in the package pre-checked, ready to install.
![image](https://user-images.githubusercontent.com/6827857/113576269-e8e2ca00-9627-11eb-9948-e905be1078a4.png)
5. Select Import and Unity will deploy the Unity plugin into your Assets Folder
____

### 3 - Update your agconnect-services.json file.

In order for the plugin to work, some kits are in need of agconnect-json file. Please download your latest config file from AGC and import into Assets/StreamingAssets folder.
![image](https://user-images.githubusercontent.com/6827857/113585485-f488bd80-9634-11eb-8b1e-6d0b5e06ecf0.png)
____
#### Enabling Auth Service
1. Click your project for which you need to enable Auth Service from the project list.
2. Go to Build > Auth Service. If it is the first time that you use Auth Service, click Enable now in the upper right corner.
![image](https://user-images.githubusercontent.com/32878124/128504970-e3be6ab4-344d-4e99-9647-89518db9be6d.png)
3. Click Enable in the row of each authentication mode to be enabled.
![image](https://user-images.githubusercontent.com/32878124/128504971-c90074da-2e26-4d35-b37a-c8dad4d9f496.png)

____
### 4 - Connect your game with any HMS Kit
In order for the plugin to work, you need to select the needed kits Huawei > Kit Settings.
In this project , I selected the Account kit and Auth Service. 

![2](https://user-images.githubusercontent.com/32878124/128504973-1bb41ef2-e810-4c25-ba97-a92c2e489d4b.png)
I selected Account kit also, because i use Huawei Account for authentication mode.

Now you need to call HMSAuthServiceManager as below.
```csharp
  public void Start(){
        authServiceManager = HMSAuthServiceManager.Instance;
        authServiceManager.OnSignInSuccess = OnAuthSericeSignInSuccess;
        authServiceManager.OnSignInFailed = OnAuthSericeSignInFailed;
        authServiceManager.OnCreateUserSuccess = OnAuthSericeCreateUserSuccess;
        authServiceManager.OnCreateUserFailed = OnAuthSericeCreateUserFailed;

        if (authServiceManager.GetCurrentUser() != null) { // skip the login scene }
    }
    private void OnAuthSericeSignInSuccess(SignInResult signInResult) { }
    private void OnAuthSericeSignInFailed(HMSException error) { }
    private void OnAuthSericeCreateUserSuccess(SignInResult signInResult) { }
    private void OnAuthSericeCreateUserFailed(HMSException error) { }

```

Now HMS Auth Service is ready to use. Lets look at the authentication modes.

## Huawei Account
First you need to sign in with Huawei Account Kit and take the access token. And then call AuthService SignIn method and pass the credential parameter with this method.
```csharp
private void OnAccountKitLoginSuccess(AuthAccount authHuaweiId)
{
    AGConnectAuthCredential credential = HwIdAuthProvider.CredentialWithToken(authHuaweiId.AccessToken);
    authServiceManager.SignIn(credential);
}
private void OnAccountKitLoginFailed(HMSException error)
{
   // ...
}
public void SignInWithHuaweiAccount()
{
    HMSAccountManager.Instance.OnSignInSuccess = OnAccountKitLoginSuccess;
    HMSAccountManager.Instance.OnSignInFailed = OnAccountKitLoginFailed;
    HMSAccountManager.Instance.SignIn();
}
```

## Anonymous Account
You need to SignInAnonymously method from AuthServiceManager.
```csharp
public void SignInAnonymously() => authServiceManager.SignInAnonymously();
```

## Facebook Account
First you need to sign in with [Facebook Unity SDK](https://developers.facebook.com/docs/unity/gettingstarted/) and take the access token. And then call AuthService SignIn method and pass the credential parameter with this method.

```csharp
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
```

## Mobile Number
### SignIn
To sign-in with mobile phone you need to take Country Code, Mobile Number and Password from user. And then pass these parameters with SignIn.
```csharp
public void SignInWithPhone()
{
    AGConnectAuthCredential phoneAuthCredential = PhoneAuthProvider.CredentialWithPassword(PhoneCountryCode.text, PhoneNumber.text, PhonePassword.text);
    authServiceManager.SignIn(phoneAuthCredential);
}
```
### Registeration
For registration, first you need to take verification code. To take this verification code you need to know Country Code and Phone Number. And then call verification code request method.
```csharp
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
        });
}
```
And then you need to call CreateUser method with Verification Code, Country Code, Phone Number and Password.
```csharp
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
```

## Email Adress
### SignIn
To sign-in with Email adress you need to take e-mail and password from user. And then pass these parameters with SignIn.
```csharp
public void SignInWithEmail()
{
    AGConnectAuthCredential emailAuthCredential = EmailAuthProvider.CredentialWithPassword(Email.text, EmailPassword.text);
    authServiceManager.SignIn(emailAuthCredential);    
}
```
### Registeration
For registration, first you need to take verification code. To take this verification code you need to know E-mail adress. And then call verification code request method.
```csharp
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
        });
}

```
    And then you need to call CreateUser method with Verification Code, E-mail and Password.
```csharp
public void RegisterWithEmail()
{
    EmailUser emailUser = new EmailUser.Builder()
        .SetEmail(registerEmail.text)
        .SetVerifyCode(registerEmailVerifyCode.text)
        .SetPassword(registerEmailPassword.text)
        .Build();
    authServiceManager.CreateUser(emailUser);
}   
```

## Sign Out
Use SignOut method to log out.
```csharp
authServiceManager.SignOut();
```

## Delete User
To delete the user from the database of the auth service, use the DeleteUser method.
```csharp
authServiceManager.DeleteUser();
```



## Troubleshooting 2
1. If you received package name error , please check your package name on File->Build Settings -> Player Settings -> Other Settings -> Identification
![image](https://user-images.githubusercontent.com/8115505/128307687-6629559d-d873-4e6f-9b2f-54545360e0c0.png)

2. If you received min sdk error , 
![image](https://user-images.githubusercontent.com/67346749/125592730-940912c8-f9b4-4f8b-8fe4-b13532342613.PNG)
Please set your API level as implied in the **Requirements** section
![image](https://user-images.githubusercontent.com/8115505/128321008-a8fb7d82-dce8-4b1d-bce2-05dcc690e249.png)

3. If you reveived "com.android.support.support-compat" error. Assets -> Huawei -> Editor -> Utils -> HMSGradleWorker.cs
![image](https://user-images.githubusercontent.com/32878124/128504974-fee2bfa9-9437-42ab-a31f-69d2fb77f44d.png)
## References
HMS Auth Service [Check](https://developer.huawei.com/consumer/en/doc/development/AppGallery-connect-Guides/agc-auth-introduction-0000001053732605)

## License
MIT