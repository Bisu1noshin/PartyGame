using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;

public static class SSceneManager
{
    private static ISceneLifetimeManager _currentSceneLifetimeManager;
    public static async UniTaskVoid LoadScene(PlayerInformation[] data, SceneLeftimeManager sceneLeftime, LoadSceneMode mode = LoadSceneMode.Single) 
    {
        _currentSceneLifetimeManager?.OnUnLoaded();
        _currentSceneLifetimeManager = sceneLeftime;

        await SceneManager.LoadSceneAsync(_currentSceneLifetimeManager.SceneName, mode).ToUniTask();
        _currentSceneLifetimeManager.OnLoaded(data);
    }
}
public interface ISceneLifetimeManager
{
    public string SceneName { get; }
    public void OnLoaded(PlayerInformation[] data);
    public void OnUnLoaded();
}
public sealed class SceneLeftimeManager : ISceneLifetimeManager
{
    private string sceneName;
    private Action<PlayerInformation[]> onLoad;
    private Action unLoad;

    public SceneLeftimeManager(string _sceneName, Action<PlayerInformation[]> _onLoad, Action _unLoad) {

        sceneName = _sceneName;
        onLoad = _onLoad;
        unLoad = _unLoad;
    }

    public string SceneName => sceneName;
    public void OnLoaded(PlayerInformation[] data) => onLoad.Invoke(data);
    public void OnUnLoaded() => unLoad.Invoke();
}

