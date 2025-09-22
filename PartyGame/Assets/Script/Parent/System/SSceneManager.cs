using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;

public static class SSceneManager
{
    private static ISceneLifetimeManager _currentSceneLifetimeManager;
    public static async UniTask<T> LoadScene<T>(PlayerInformation[] data, SceneLeftimeManager sceneLeftime, LoadSceneMode mode = LoadSceneMode.Single)
        where T : InGameManeger
    {
        _currentSceneLifetimeManager?.OnUnLoaded();
        _currentSceneLifetimeManager = sceneLeftime;

        await SceneManager.LoadSceneAsync(_currentSceneLifetimeManager.SceneName, mode).ToUniTask();
        Scene scene = SceneManager.GetSceneByName(_currentSceneLifetimeManager.SceneName);

        foreach (GameObject go in scene.GetRootGameObjects())
        {
            var presenter = go.GetComponent<T>();
            if (presenter != null)
            {
                return presenter;
            }
        }
        
        return null;
    }
}
public interface ISceneLifetimeManager
{
    public string SceneName { get; }
    public void OnUnLoaded();
}
public sealed class SceneLeftimeManager : ISceneLifetimeManager
{
    private string sceneName;
    private Action unLoad;

    public SceneLeftimeManager(string _sceneName, Action _unLoad) {

        sceneName = _sceneName;
        unLoad = _unLoad;
    }

    public string SceneName => sceneName;
    public void OnUnLoaded() => unLoad.Invoke();
}

