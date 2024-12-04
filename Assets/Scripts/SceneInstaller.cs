using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private LayerMask _hangLayerMask;
    [SerializeField]
    private LayerMask _lemmingLayerMask;
    public override void InstallBindings()
    {
        Debug.Log("bind");
        Container.Bind<LayerMask>().WithId("_hangLayerMask").FromInstance(_hangLayerMask);
        Container.Bind<LayerMask>().WithId("_lemmingLayerMask").FromInstance(_lemmingLayerMask);
        Container.Bind<InputHandler>().AsSingle().NonLazy();
    }
}
