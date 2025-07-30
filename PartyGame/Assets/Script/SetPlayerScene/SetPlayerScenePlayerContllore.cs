using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerScenePlayerContllore : PlayerParent
{
    bool SetUserNum;
    bool onButtonA;
    private string FBXpath = " ";
    private PlayerInformation information = default;
    private GameObject[] prefabs;

    private Vector3[] pos = new Vector3[4] {

        new Vector3(-5,1,0),
        new Vector3(4.5f,1,0),
        new Vector3(-5,-4,0),
        new Vector3(4.5f,-3.5f,0)
    };

    private Vector3[] scale = new Vector3[4] {

        new Vector3(3,3,3),
        new Vector3(2.5f,2.5f,2.5f),
        new Vector3(3,3,3),
        new Vector3(2.5f,2.5f,2.5f)
    };
    private enum StateType {

        Non,
        UserNum,
        ChoseFBX,
        End
    }

    private enum TriggerType{

        UserNum,
        ChoseFBX,
        End
    }

    private StateMachine<StateType, TriggerType> st;

    private void Start()
    {
        // StateMachine
        {
            // 初期化
            st = new StateMachine<StateType, TriggerType>(StateType.UserNum);

            // 遷移情報の登録
            st.AddTransition(StateType.UserNum, StateType.ChoseFBX, TriggerType.ChoseFBX);
            st.AddTransition(StateType.ChoseFBX, StateType.ChoseFBX, TriggerType.UserNum);
            st.AddTransition(StateType.ChoseFBX, StateType.End, TriggerType.End);
            st.AddTransition(StateType.End, StateType.ChoseFBX, TriggerType.ChoseFBX);
            st.AddTransition(StateType.End, StateType.UserNum, TriggerType.UserNum);

            // Actionの登録
            st.SetupState(StateType.UserNum, EnterUserNum, ExitUserNum, deltaTime => UpDateUserNum());
            st.SetupState(StateType.ChoseFBX, EnterChoseFBX, ExitChoseFBX, deltaTime => UpDateChoseFBX());
            st.SetupState(StateType.End, EnterEnd, ExitEnd, deltaTime => UpDateEnd());
        }

        onButtonA = false;
        prefabs = new GameObject[4];

        // FBXのファイル
        {
            for (int i = 0; i < 4; i++)
            {
                prefabs[i]= Resources.Load<GameObject>("Player/VRM/VRM_" + (i+1).ToString());
            }
        }
    }

    protected override void MoveUpdate(Vector2 vec)
    {
        
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {
        onButtonA = true;
    }

    protected override void UpButtonA() {

        onButtonA = false;
    }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }

    private void Update() {

        // StateMachineの更新
        st.Update(Time.deltaTime);
    }

    private void EnterUserNum() {

        SetUserNum = false;
    }
    private void UpDateUserNum() {

        if (onButtonA) {

            SetUserNum = true;
        }

        if (SetUserNum) {

            st.ExecuteTrigger(TriggerType.ChoseFBX);
        }
    }
    private void ExitUserNum() {

        Debug.Log("ExitUserNum");
    }

    private void EnterChoseFBX() { }
    private void UpDateChoseFBX() {

        st.ExecuteTrigger(TriggerType.End);
        int index = playerInput.playerIndex;
        GameObject go =
         Instantiate(prefabs[index], pos[index], Quaternion.identity);

        go.transform.localScale = scale[index];
    }
    private void ExitChoseFBX() {

        Debug.Log("ExitChoseFBX");
    }

    private void EnterEnd() {

        information = new PlayerInformation(playerInput.playerIndex,FBXpath);
    }
    private void UpDateEnd() {

        SetPlayerInformation();
    }
    private void ExitEnd() {

        information = new PlayerInformation(playerInput.playerIndex, null);
    }

    private void SetPlayerInformation() {

        GameObject sm = GameObject.Find("SceneManager");
        sm.GetComponent<SetPlayerSceneManager>().
            SetPlayerInformation(information, playerInput.playerIndex);

        Debug.Log("player" + playerInput.playerIndex + "のデータを追記しました。");
    }
}
