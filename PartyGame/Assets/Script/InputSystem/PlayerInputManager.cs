using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public GameObject[] Player = new GameObject[playerLenge];

    private PartyGame inputAction;
    private const int playerLenge = 2;

    private void Awake()
    {
        inputAction = new PartyGame();

        inputAction.Test.Test.started += OnTestInput;

        inputAction.Enable();
    }

    // ���̓C�x���g

    public void OnTestInput(InputAction.CallbackContext context)
    {
        // CallbackContext����Control���擾
        var control = context.control;

        // Control����f�o�C�X���擾
        var device = control.device;

        // ���O�o��
        print($"Control Path: {control.path}, Device: {device}, Control: {control}");

        // �v���C���[��o�^
        for (int i = 0; i < Player.Length; i++) {

            if (Player[i] == null) {

                Player[i] = Instantiate(playerPrefab);
                Player[i].GetComponent<TestPlayer>().SetUserNumber(i);

                int user = i + 1;
                Debug.Log("�������܂��� player:" + user);
                return;
            }

            Debug.Log("player�̐����ő�l�ł�");
        }
    }
}
