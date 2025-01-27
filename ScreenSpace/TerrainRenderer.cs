using Game;

namespace ScreenSpace {
    public class TerrainRenderer : Game.TerrainRenderer {
        public TerrainRenderer(SubsystemTerrain subsystemTerrain) : base(subsystemTerrain) {
            if (m_opaqueShader is not NewOpaqueShader) m_opaqueShader = new NewOpaqueShader();
            if (m_alphaTestedShader is not NewAlphaTestedShader) m_alphaTestedShader = new NewAlphaTestedShader();
            if (m_transparentShader is not NewTransparentShader) m_transparentShader = new NewTransparentShader();
        }

        public override void DrawOpaque(Camera camera) {
            if (m_opaqueShader is NewOpaqueShader newOpaqueShader) {
                newOpaqueShader.Transforms.World[0] = camera.ViewProjectionMatrix;
            }
            base.DrawOpaque(camera);
        }

        public override void DrawAlphaTested(Camera camera) {
            if (m_alphaTestedShader is NewAlphaTestedShader newAlphaTestedShader) {
                newAlphaTestedShader.Transforms.World[0] = camera.ViewProjectionMatrix;
            }
            base.DrawAlphaTested(camera);
        }

        public override void DrawTransparent(Camera camera) {
            if (m_transparentShader is NewTransparentShader newTransparentShader) {
                newTransparentShader.Transforms.World[0] = camera.ViewProjectionMatrix;
            }
            base.DrawTransparent(camera);
        }
    }
}