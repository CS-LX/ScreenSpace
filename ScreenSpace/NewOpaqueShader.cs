using Engine;
using Engine.Graphics;
using Game;

namespace ScreenSpace {
    public class NewOpaqueShader : Shader {
        public ShaderParameter m_worldViewProjectionMatrixParameter;
        public ShaderParameter m_solutionParameter;
        public readonly ShaderTransforms Transforms;

        public NewOpaqueShader() : base(ShaderCodeManager.GetFast("Shaders/Opaque.vsh"), ShaderCodeManager.GetFast("Shaders/Opaque.psh"), new ShaderMacro[] { new("Opaque") }) {
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