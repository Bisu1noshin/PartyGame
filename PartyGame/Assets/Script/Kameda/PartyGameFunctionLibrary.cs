using UnityEngine;
using UnityEngine.InputSystem;
using System;


public static class PartyGameFunctionLibrary
{
    /// <summary>
    /// プレイヤーのインスタンス生成
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="type"></param>
    /// <param name="device"></param>
    /// <param name="playerIndex"></param>
    /// <param name="position"></param>
    /// <param name="roatation"></param>
    /// <returns></returns>
    public static PlayerParent CreatePlayer(
        GameObject prefab,
        Type type,
        InputDevice device,
        int playerIndex,
        Vector3 position,
        Quaternion roatation
        )
    {
        if (!prefab.GetComponent<PlayerInput>())
        {

            Debug.Log("PlayerInputコンポーネントがアタッチされていません");
            return null;
        }

        PlayerInput pi = PlayerInput.Instantiate(
            prefab: prefab,
            playerIndex: playerIndex,
            pairWithDevice: device
            );

        pi.gameObject.transform.localPosition = position;
        pi.gameObject.transform.rotation = roatation;

        pi.gameObject.AddComponent(type);
        PlayerParent p = pi.gameObject.GetComponent<PlayerParent>();
        return p;
    }
}
