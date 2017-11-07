using C3DE.Demo.Scenes;
using C3DE.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C3DE.Demo
{
    public static class DemoGame
    {
        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 800;

        public static string[] BlueSkybox = new string[6]
        {
            "Textures/Skybox/bluesky/px",
            "Textures/Skybox/bluesky/nx",
            "Textures/Skybox/bluesky/py",
            "Textures/Skybox/bluesky/ny",
            "Textures/Skybox/bluesky/pz",
            "Textures/Skybox/bluesky/nz"
        };

        public static string[] StarsSkybox = new string[]
        {
            "Textures/Skybox/starfield/px",
            "Textures/Skybox/starfield/nx",
            "Textures/Skybox/starfield/py",
            "Textures/Skybox/starfield/ny",
            "Textures/Skybox/starfield/pz",
            "Textures/Skybox/starfield/nz"
        };

        public static GUISkin CreateSkin(ContentManager content, bool customSkin = true)
        {
            GUISkin skin = new GUISkin("Font/Default");
            skin.LoadContent(Application.Content);

            if (customSkin)
            {
                skin.Box = content.Load<Texture2D>("Textures/UI/grey_panel");
                skin.Buttons[0] = content.Load<Texture2D>("Textures/UI/grey_button00");
                skin.Buttons[1] = content.Load<Texture2D>("Textures/UI/grey_button01");
                skin.Buttons[2] = content.Load<Texture2D>("Textures/UI/grey_button02");
                skin.Checkbox[0] = content.Load<Texture2D>("Textures/UI/grey_box");
                skin.Checkbox[1] = content.Load<Texture2D>("Textures/UI/grey_checkmarkWhite");
                skin.Checkbox[2] = content.Load<Texture2D>("Textures/UI/grey_checkmarkGrey");
                skin.Sliders[0] = content.Load<Texture2D>("Textures/UI/grey_button02");
                skin.Sliders[1] = content.Load<Texture2D>("Textures/UI/grey_button00");
                skin.TextColor = Color.Black;
            }

            return skin;
        }

#if !NETFX_CORE && !ANDROID

        // Entry point.
        static void Main(string[] args)
        {
            using (var game = new Engine("C3DE Game Engine", ScreenWidth, ScreenHeight))
            {
                Application.SceneManager.Add(new MenuDemo(), true);
                Application.SceneManager.Add(new HeightmapDemo());
                Application.SceneManager.Add(new ProceduralTerrainWater());
                Application.SceneManager.Add(new ProceduralTerrainLava());
                Application.SceneManager.Add(new PostProcessingDemo());
                Application.SceneManager.Add(new LightingDemo());
                Application.SceneManager.Add(new SponzaDemo());
                Application.SceneManager.Add(new PreLightingDemo());
                Application.SceneManager.Add(new HexagonTerrainDemo());
                Application.SceneManager.Add(new GUIDemo());
                Application.SceneManager.Add(new VirtualRealityDemo());
                Application.SceneManager.LoadLevel(0);
                Screen.Fullscreen = false; // Set to true in VR
                game.Run();
            }
        }

#endif
    }
}
