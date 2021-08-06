using HmsPlugin;
using HuaweiMobileServices.AuthService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private HMSAuthServiceManager authServiceManager = null;
    private AGConnectUser user = null;
    public Text loggedInUser;

    void Start()
    {
        authServiceManager = HMSAuthServiceManager.Instance;
        user = authServiceManager.GetCurrentUser();

        if (user != null)
        {
            loggedInUser.text = user.IsAnonymous() ? "Welcome Guest" : "Welcome " + user.DisplayName;
        }
        else
        {
            SceneManager.LoadScene("LoginScene");
        }
    }

    public void SignOut()
    {
        authServiceManager.SignOut();
        SceneManager.LoadScene("LoginScene");
    }
}
