using Engine;
using Engine.Graphics;
using Game;

namespace ScreenSpace {
    public class NewAlphaTestedShader : Shader {
        public ShaderParameter m_worldViewProjectionMatrixParameter;
        public ShaderParameter m_solutionParameter;
        public readonly ShaderTransforms Transforms;

        public NewAlphaTestedShader() : base(ShaderCodeManager.GetFast("Shaders/AlphaTested.vsh"), ShaderCodeManager.GetFast("Shaders/AlphaTested.psh"), new ShaderMacro[] { new("ALPHATESTED") }) {
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