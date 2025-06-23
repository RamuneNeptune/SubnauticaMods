

namespace RamuneLib.Extensions
{
    public static class RendererExtensions
    {
        /// <summary>
        /// Types of textures that can be set
        /// </summary>
        public enum TextureType
        {
            Main,
            Specular,
            Illum,
            Emissive,
        }


        /// <summary>
        /// Sets a texture on the specified material index of the Renderer.
        /// </summary>
        /// <param name="type">The type of texture to set (Main, Specular, Illum)</param>
        /// <param name="texture">The texture to apply</param>
        /// <param name="materialIndex">The index of the material to set the texture on (default is 0.</param>
        /// <returns>The modified Renderer.</returns>
        public static Renderer SetTexture(this Renderer renderer, TextureType type, Texture2D texture, bool applyToEverything = false)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetTexture: renderer is null");

            switch(type)
            {
                case TextureType.Main:
                    if(applyToEverything) renderer.materials.ForEach(m => m.SetTexture(ShaderPropertyID._MainTex, texture));
                    else renderer.material.SetTexture(ShaderPropertyID._MainTex, texture);
                    break;

                case TextureType.Specular:
                    if(applyToEverything) renderer.materials.ForEach(m => m.SetTexture(ShaderPropertyID._SpecTex, texture));
                    else renderer.material.SetTexture(ShaderPropertyID._SpecTex, texture);
                    break;

                case TextureType.Illum:
                    if(applyToEverything) renderer.materials.ForEach(m => m.SetTexture(ShaderPropertyID._Illum, texture));
                    else renderer.material.SetTexture(ShaderPropertyID._Illum, texture);
                    break;

                case TextureType.Emissive:
                    if(applyToEverything) renderer.materials.ForEach(m => m.SetTexture(ShaderPropertyID._Illum, texture));
                    else renderer.material.SetTexture(ShaderPropertyID._EmissiveTex, texture);
                    break;
            }

            return renderer;
        }


        /// <summary>
        /// Sets a texture for multiple materials
        /// </summary>
        /// <param name="type">The type of texture to set (Main, Specular, Illum)</param>
        /// <param name="texture">The texture to apply</param>
        /// <param name="materialIndexes">An array of material indexes to apply the textures to</param>
        /// <returns>The modified Renderer.</returns>
        public static Renderer SetTexture(this Renderer renderer, TextureType type, Texture2D texture, params int[] materialIndexes)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetTexture: renderer is null");

            materialIndexes.ForEach(i =>
            {
                switch (type)
                {
                    case TextureType.Main:

                        renderer.materials[i].SetTexture(ShaderPropertyID._MainTex, texture);
                        break;

                    case TextureType.Specular:
                        renderer.materials[i].SetTexture(ShaderPropertyID._SpecTex, texture);
                        break;

                    case TextureType.Illum:
                        renderer.materials[i].SetTexture(ShaderPropertyID._Illum, texture);
                        break;

                    case TextureType.Emissive:
                        renderer.materials[i].SetTexture(ShaderPropertyID._EmissiveTex, texture);
                        break;
                }
            });

            return renderer;
        }


        /// <summary>
        /// Sets the glow strength of the specified material
        /// </summary>
        /// <param name="strength">The glow strength value</param>
        public static Renderer SetGlowStrength(this Renderer renderer, float strength, bool applyToEverything = false)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetGlowStrength: renderer is null");

            if(applyToEverything)
            {
                renderer.materials.ForEach(m =>
                {
                    m.SetFloat(ShaderPropertyID._GlowStrength, strength);
                    m.SetFloat(ShaderPropertyID._GlowStrengthNight, strength);
                });
            }
            else
            {
                renderer.material.SetFloat(ShaderPropertyID._GlowStrength, strength);
                renderer.material.SetFloat(ShaderPropertyID._GlowStrengthNight, strength);
            }

            return renderer;
        }


        /// <summary>
        /// Sets the glow strength on multiple materials
        /// </summary>
        /// <param name="strength">The glow strength value</param>
        /// <param name="materialIndexes">An array of material indexes to apply the glow strength to</param>
        public static Renderer SetGlowStrength(this Renderer renderer, float strength, params int[] materialIndexes)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetGlowStrengths: renderer is null");

            materialIndexes.ForEach(i =>
            {
                renderer.materials[i].SetFloat(ShaderPropertyID._GlowStrength, strength);
                renderer.materials[i].SetFloat(ShaderPropertyID._GlowStrengthNight, strength);
            });
            return renderer;
        }


        /// <summary>
        /// Toggles 'MARMO_EMISSION' keyword on all materials
        /// </summary>
        /// <param name="toggleState">True to enable emission, false to disable</param>
        public static Renderer ToggleEmission(this Renderer renderer, bool toggleState = true)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.ToggleEmission: renderer is null");

            if(toggleState) renderer.materials.ForEach(m => m.EnableKeyword("MARMO_EMISSION"));
            else renderer.materials.ForEach(m => m.DisableKeyword("MARMO_EMISSION"));
            return renderer;
        }


        /// <summary>
        /// Toggles 'MARMO_EMISSION' keyword for the specified material
        /// </summary>
        /// <param name="toggleState">True to enable emission, false to disable</param>
        /// <param name="materialIndex">The index of the material to toggle emission on</param>
        public static Renderer ToggleEmission(this Renderer renderer, bool toggleState, int materialIndex = 0)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.ToggleEmission: renderer is null");

            if(toggleState) renderer.materials[materialIndex].EnableKeyword("MARMO_EMISSION");
            else renderer.materials[materialIndex].DisableKeyword("MARMO_EMISSION");
            return renderer;
        }


        /// <summary>
        /// Toggles 'MARMO_EMISSION' keyword for multiple materials
        /// </summary>
        /// <param name="toggleState">True to enable emission, false to disable</param>
        /// <param name="materialIndexes">An array of material indexes to toggle emission on to</param>
        public static Renderer ToggleEmission(this Renderer renderer, bool toggleState, params int[] materialIndexes)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.ToggleEmission: renderer is null");

            materialIndexes.ForEach(i =>
            {
                if(toggleState) renderer.materials[i].EnableKeyword("MARMO_EMISSION");
                else renderer.materials[i].DisableKeyword("MARMO_EMISSION");
            });
            return renderer;
        }


        public static Renderer SetFloat(this Renderer renderer, int property, float value, bool applyToEverything = false)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetFloat: renderer is null");

            if(applyToEverything) renderer.materials.ForEach(m => m.SetFloat(property, value));
            else renderer.material.SetFloat(property, value);

            return renderer;
        }


        public static Renderer SetFloat(this Renderer renderer, int property, float value, params int[] materialIndexes)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetFloat: renderer is null");

            materialIndexes.ForEach(i => renderer.materials[i].SetFloat(property, value));

            return renderer;
        }


        public static Renderer SetColor(this Renderer renderer, int property, Color color, bool applyToEverything = false)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetColor: renderer is null");

            if(applyToEverything) renderer.materials.ForEach(m => m.SetColor(property, color));
            else renderer.material.SetColor(property, color);

            return renderer;
        }


        public static Renderer SetColor(this Renderer renderer, int property, Color color, params int[] materialIndexes)
        {
            if(renderer == null)
                throw new NullReferenceException("RendererExtensions.SetColor: renderer is null");

            materialIndexes.ForEach(i => renderer.materials[i].SetColor(property, color));
            
            return renderer;
        }
    }
}