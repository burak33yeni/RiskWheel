using Design.Patterns.ServiceLocator;
using UnityEngine;

public class SceneContextCreator : MonoBehaviour
{
    [SerializeField] private Context[] contextPrefabs;

    private void Awake()
    {
        for (int i = 0; i < contextPrefabs.Length; i++)
        {
            if(contextPrefabs[i].gameObject.scene.name != null)
            {
                throw new ObjectNotPrefabException();
            }
            
            Context context = Instantiate(contextPrefabs[i], transform);
            context.Initialize(ProjectContextCreator.ProjectContext);
        }
    }
}
    
public static class ProjectContextCreator
{
    private static Context _projectContext;
    public static Context ProjectContext => _projectContext;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadProjectContext()
    {
        if (_projectContext != null) return;
        Context projectContextPrefab = Resources.Load<Context>("ProjectContext");
        _projectContext = Object.Instantiate(projectContextPrefab);
        _projectContext.Initialize();
        _projectContext.CheckPersistence();
        Object.DontDestroyOnLoad(_projectContext);
    }
}