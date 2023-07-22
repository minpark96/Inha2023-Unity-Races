using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Lab1;
using UnityEngine.UI;
using System.Windows.Input;
using UnityEditor;

public class GameCenter : MonoBehaviour
{
    static GameCenter s_Instance; // 유일성이 보장된다
    public static GameCenter Instance { get { Init(); return s_Instance; } } // 유일한 매니저를 갖고온다

    [SerializeField]
    Transform dogRoot;
    [SerializeField]
    Transform guyRoot;
    [SerializeField]
    Transform playerInfoRoot;
    [SerializeField]
    Transform infoDog;
    [SerializeField]
    Transform panelBetting;
    [SerializeField]
    Button buttonRace;
    [SerializeField]
    InputField inputfieldBet;
    [SerializeField]
    Dropdown dropdownDog;

    public enum Step
    {
        Betting,
        ReadyRace,
        StartRace,
        Racing,
        EndRace,
        ShowResult,
    }

    [SerializeField]
    Step currentStep;

    public Step CurrentStep { get { return currentStep; } }
    Guy[] guys = null;
    Dog[] dogs = null;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        Init();
        if (!buttonRace)
            Debug.LogError("레이스 버튼이 연결되지 않음");
        LoadGuys(guyRoot.childCount);
        LoadDogs(dogRoot.childCount);
    }

    static void Init()
    {
        GameObject go = GameObject.Find("GameCenter");
        if (go == null)
        {
            go = new GameObject { name = "GameCenter" };
            go.AddComponent<GameCenter>();
        }

        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<GameCenter>();
    }

    void LoadGuys(int count)
    {
        guys = new Guy[count];
        for (int i = 0; i < count; i++)
        {
            guys[i] = guyRoot.transform.GetChild(i).GetComponent<Guy>();
            SetPlayerInfo(i);

            Debug.Log(guys[i].name + "읽었다");
        }
    }
    
    void LoadDogs(int count)
    {
        dogs = new Dog[count];
        for (int i = 0; i < count; i++)
        {
            dogs[i] = dogRoot.transform.GetChild(i).GetComponent<Dog>();
            Debug.Log(dogs[i].name + "읽었다");
        }
    }

    void NextStep()
    {
        Step preStep = currentStep;
        currentStep = (currentStep == Step.ShowResult) ? Step.Betting : currentStep + 1;

        switch (currentStep)
        {
            case Step.ReadyRace:
                {
                    buttonRace.interactable = true;
                }
                break;
            case Step.StartRace:
                {
                    buttonRace.interactable = false;
                }
                break;
        }
    }

    void PrevStep()
    {
        currentStep -= 1;
        switch (currentStep)
        {
            case Step.Betting:
                {
                    buttonRace.interactable = false;
                }
                break;
        }
    }

    public void StartRace()
    {
        NextStep();

        Debug.Log("Start Race");

        for(int i = 0; i < dogs.Length; i++) 
        {
            dogs[i].Run();
        }

        NextStep();
    }

    public void EndBet() 
    {
        panelBetting.gameObject.SetActive(false);
        Debug.Log("End Bet");

        for (int i = 0; i < playerInfoRoot.childCount; i++)
            playerInfoRoot.GetChild(i).GetChild(6).GetComponent<Button>().interactable = true;

        NextStep();
    }

    public void StartBet()
    {
        panelBetting.gameObject.SetActive(true);
        Debug.Log("Start Bet");

        for (int i = 0; i < playerInfoRoot.childCount; i++)
            playerInfoRoot.GetChild(i).GetChild(6).GetComponent<Button>().interactable = false;

        PrevStep();
    }

    public void SetPlayerInfo(int i)
    {
        playerInfoRoot.GetChild(i).GetChild(0).GetComponent<Text>().text = guys[i].Name;
        playerInfoRoot.GetChild(i).GetChild(3).GetComponent<Text>().text = guys[i].Bet.Amount.ToString();
        playerInfoRoot.GetChild(i).GetChild(5).GetComponent<Text>().text = guys[i].Cash.ToString();
    }

    public void Bet() 
    {
        guys[0].Bet.SetBetting(dropdownDog.value + 1, int.Parse(inputfieldBet.text));
        Debug.Log(guys[0].Bet.GetDescription());

        SetPlayerInfo(0);

    }

    public void CancelBet()
    {
        
    }
}
