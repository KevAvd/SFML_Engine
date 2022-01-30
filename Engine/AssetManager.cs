using System;
using System.Collections.Generic;
using System.IO;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Engine
{
    internal class AssetManager
    {
        string _fontsPaths; //Contains path to the fonts
        string _texturesPaths; //Contains path to the textures
        Dictionary<string, Texture> _textures = new Dictionary<string, Texture>(); //Contains loaded textures
        Dictionary<string, Font> _fonts = new Dictionary<string, Font>(); //Contains loaded fonts

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fp"> Paths to the fonts </param>
        /// <param name="tp"> Paths to the textures </param>
        public AssetManager(string fp, string tp)
        {
            _fontsPaths = fp;
            _texturesPaths = tp;
        }

        /// <summary>
        /// Loads files
        /// </summary>
        public void Loadfiles()
        {
            string[] files;

            for (int i = 0; i < 2; i++)
            {
                files = Directory.GetFiles(i == 0 ? _fontsPaths : _texturesPaths);
                for (int j = 0; j < files.Length; j++)
                {
                    if (File.Exists(files[j]))
                    {
                        if (i == 0) { _fonts.Add(files[j].Remove(0, _fontsPaths.Length + 1), new Font(files[j])); }
                        else { _textures.Add(files[j].Remove(0, _texturesPaths.Length + 1), new Texture(files[j])); }
                    }
                    else
                    {
                        LogHandler.GetInstance().AddLog($"[ASSETMANAGER][LOADFILES-ERROR] {files[j].Remove(0, _fontsPaths.Length + 1)} doesn't exist");
                    }
                }
            }
        }

        /// <summary>
        /// Get texture
        /// </summary>
        /// <param name="name"> Texture's file name </param>
        /// <returns> Texture </returns>
        public Texture GetTexture(string name)
        {
            Texture tex = null;
            _textures.TryGetValue(name, out tex);

            if(tex == null)
            {
                LogHandler.GetInstance().AddLog($"[ASSETMANAGER][GETTEXTURE-ERROR] {name} doesn't exist");
                return new Texture(10,10);
            }

            return tex;
        }

        /// <summary>
        /// Get font
        /// </summary>
        /// <param name="name"> Font's file name </param>
        /// <returns> Font </returns>
        public Font GetFont(string name)
        {
            Font font = null;
            _fonts.TryGetValue(name, out font);

            if (font == null)
            {
                LogHandler.GetInstance().AddLog($"[ASSETMANAGER][GETTEXTURE-ERROR] {name} doesn't exist");
            }

            return font;
        }
    }
}
