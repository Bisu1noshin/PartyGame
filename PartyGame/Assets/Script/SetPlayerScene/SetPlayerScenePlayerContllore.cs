using UnityEngine;

public class SetPlayerScenePlayerContllore : PlayerParent2
{
    bool SetUserNum;
    bool onButtonA;
    int UIContllore;

    private string FBXpath;
    private PlayerInformation information = default;

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
    private void ExitUserNum() { }

    private void EnterChoseFBX() { }
    private void UpDateChoseFBX() { }
    private void ExitChoseFBX() { }

    private void EnterEnd() {
        //information = new PlayerInformation(playerInput.devices, playerInput.playerIndex,FBXpath);
    }
    private void UpDateEnd() { }
    private void ExitEnd() { }

    private void SetPlayerInformation() {

        GameObject sm = GameObject.Find("SceneManager");
        sm.GetComponent<SetPlayerSceneManager>().
            SetPlayerInformation(information, playerInput.playerIndex);
    }
}
