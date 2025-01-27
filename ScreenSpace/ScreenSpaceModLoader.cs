using Game;
using GameEntitySystem;

namespace ScreenSpace {
    public class ScreenSpaceModLoader : ModLoader {

        public override void __ModInitialize() {
            base.__ModInitialize();
            ModsManager.RegisterHook("OnProjectLoaded", this);
        }

        public override void OnProjectLoaded(Project project) {
            base.OnProjectLoaded(project);
            project.FindSubsystem<SubsystemTerrain>().TerrainRenderer = new TerrainRenderer(project.FindSubsystem<SubsystemTerrain>());
        }
    }
}