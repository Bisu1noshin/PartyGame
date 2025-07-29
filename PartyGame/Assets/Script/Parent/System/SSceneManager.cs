using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;

public static class SSceneManager
{
    private static ISceneLifetimeManager _currentSceneLifetimeManager;

    public static async UniTaskVoid LoadScene<T>(PlayerInformation[] data) where T : ISceneLifetimeManager,new()
    {
        _currentSceneLifetimeManager?.OnUnLoaded();
        _currentSceneLifetimeManager = new T();

        await SceneManager.LoadSceneAsync(_currentSceneLifetimeManager.SceneName).ToUniTask();
        _currentSceneLifetimeManager.OnLoaded(data);
    }
}
public interface ISceneLifetimeManager
{
    public string SceneName { get; }
    public void OnLoaded(PlayerInformation[] data);
    public void OnUnLoaded();
}
