using Design.Patterns.ServiceLocator;
using UnityEngine;

public class GameSceneMonoInstaller : MonoInstaller
{
    [SerializeField] private Context gamePlayContext;
    public override void Install(Context context)
    {
        context.RegisterContext().FromPrefab(gamePlayContext).WithParent(transform);
    }
}
