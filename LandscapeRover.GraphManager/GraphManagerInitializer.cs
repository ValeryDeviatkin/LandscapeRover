using LandscapeRover.GraphManager.Interfaces;
using LandscapeRover.GraphManager.Services;
using Unity;

namespace LandscapeRover.GraphManager
{
    public static class GraphManagerInitializer
    {
        public static void Initialize(IUnityContainer container)
        {
            container
               .RegisterSingleton<ILandscapeMatrixService, LandscapeMatrixService>()
                ;
        }
    }
}