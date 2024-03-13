using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;


public class GPG_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    private int attempt;
    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        attempt++;
    }
    


    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();

            string id = PlayGamesPlatform.Instance.GetUserId();

            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            if(m_Text) m_Text.SetText("Sign In Successed: " + name);
        }
        else
        {
            if(m_Text) m_Text.SetText("Sign In Failed, Attempt: " + attempt);
        }
    }
}
