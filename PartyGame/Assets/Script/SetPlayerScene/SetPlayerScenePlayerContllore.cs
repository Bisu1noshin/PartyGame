using System;
using UnityEngine;

public class SetPlayerScenePlayerContllore : PlayerParent
{
    bool SetUserNum;
    bool onButtonA;
    int UIContllore;

    private string FBXpath = " ";
    private PlayerInformation information = default;
    private GameObject[] prefabs;

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
        UIContllore = 0;
        prefabs = new GameObject[4];

        // FBXのファイル
        {
            for (int i = 0; i < 4; i++)
            {
                prefabs[i]= Resources.Load<GameObject>("Player/Test/Cube_" + i.ToString());
            }
        }
    }

    protected override void MoveUpdate(Vector2 vec)
    {
        UIContllore = (int)vec.x;
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
    }
    private void ExitChoseFBX() {

        Debug.Log("ExitChoseFBX");
    }

    private void EnterEnd() {

        information = new PlayerInformation(playerInput.playerIndex,FBXpath);
    }
    private void UpDateEnd() {

        if(onButtonA) {

            SetPlayerInformation();
            onButtonA = false;
        }
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
