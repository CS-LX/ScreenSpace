using Engine;
using Engine.Graphics;
using Game;

namespace ScreenSpace {
    public class NewTransparentShader : Shader {
        public ShaderParameter m_worldViewProjectionMatrixParameter;
        public ShaderParameter m_solutionParameter;
        public readonly ShaderTransforms Transforms;

        public NewTransparentShader() : base(ShaderCodeManager.GetFast("Shaders/Transparent.vsh"), ShaderCodeManager.GetFast("Shaders/Transparent.psh"), new ShaderMacro[] { new("Transparent") }) {
            m_worldViewProjectionMatrixParameter = GetParameter("u_worldViewProjectionMatrix");
            m_solutionParameter = GetParameter("u_solution");
            Transforms = new ShaderTransforms(1);
        }

        public override void PrepareForDrawingOverride() {
            Transforms.UpdateMatrices(1, false, false, true);
            m_worldViewProjectionMatrixParameter.SetValue(Transforms.WorldViewProjection, 1);
            m_solutionParameter.SetValue(new Vector2((float)Window.Size.X / Window.Size.Y, 1));
        }
    }
}