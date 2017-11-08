﻿using C3DE.Components.Lighting;
using C3DE.Components.Rendering;
using C3DE.Demo.Scripts;
using C3DE.Graphics.Geometries;
using C3DE.Graphics.Materials;
using C3DE.Graphics.Rendering;
using C3DE.Utils;
using C3DE.VR;
using Microsoft.Xna.Framework;

namespace C3DE.Demo.Scenes
{
	public class VirtualRealityDemo : Scene
    {
        public VirtualRealityDemo() : base("Virtual Reality") { }

        public override void Initialize()
        {
            base.Initialize();

            var cameraGo = GameObjectFactory.CreateCamera();
			Add(cameraGo);

			var trackingSpace = new GameObject();
			Add(trackingSpace);
			cameraGo.Transform.Parent = trackingSpace.Transform;

			var head = new GameObject();
			head.Transform.Position = new Vector3(0, 1.8f, 0);
			Add(head);
			trackingSpace.Transform.Parent = head.Transform;

			var player = new GameObject();
			Add(player);
			head.Transform.Parent = player.Transform;

            var vrDevice = GetService();
            if (vrDevice.TryInitialize() == 0)
			{
				var vrRenderer = new VRRenderer(Application.GraphicsDevice, vrDevice);
				Application.Engine.Renderer = vrRenderer;
			}

            BuildScene();
		}

        private VRService GetService()
        {
#if DESKTOPGL
            return new OSVRService(Application.Engine);
#else
            return new NullVRService(Application.Engine);
#endif
        }

        public override void Unload()
        {
            Application.Engine.Renderer = new ForwardRenderer(Application.GraphicsDevice);
            base.Unload();
        }

        private void BuildScene()
        {
            var lightGo = GameObjectFactory.CreateLight(LightType.Directional);
            lightGo.Transform.Position = new Vector3(-15, 15, 15);
            lightGo.Transform.Rotation = new Vector3(0, MathHelper.Pi, 1);
            lightGo.Transform.Rotation = new Vector3(-1, 1, 0);
            lightGo.AddComponent<DemoBehaviour>();
            Add(lightGo);

            var light = lightGo.GetComponent<Light>();
            light.Range = 105;
            light.Intensity = 2.0f;
            light.FallOf = 5f;
            light.Color = Color.Violet;
            light.ShadowGenerator.ShadowStrength = 0.6f; // FIXME need to be inverted

			// Terrain
			var terrainMaterial = new StandardMaterial(scene);
			terrainMaterial.Texture = GraphicsHelper.CreateBorderTexture(Color.LightGreen, Color.LightSeaGreen, 128, 128, 4);
			terrainMaterial.Shininess = 10;
			terrainMaterial.Tiling = new Vector2(64);

            var terrainGo = GameObjectFactory.CreateTerrain();
            var terrain = terrainGo.GetComponent<Terrain>();
            terrain.Renderer.Geometry.Size = new Vector3(2);
            terrain.Renderer.Geometry.Build();
            terrain.Flatten();
            terrain.Renderer.Material = terrainMaterial;
            terrain.Transform.Translate(-terrain.Width >> 1, 0, -terrain.Depth / 2);
			Add(terrainGo);

			var cubMat = new StandardMaterial(this);
			cubMat.Texture = GraphicsHelper.CreateCheckboardTexture(Color.Red, Color.White);
			cubMat.Tiling = new Vector2(2, 2);

			for (var i = 0; i < 10; i++)
			{
				var go = new GameObject("Cube " + i);
				go.Transform.Position = RandomHelper.GetVector3(-20, 0.5f, -20, 20, 0.5f, 20);
				Add(go);
				var renderer = go.AddComponent<MeshRenderer>();
				renderer.Geometry = new CubeGeometry();
				renderer.Geometry.Build();
				renderer.Material = cubMat;
			}

			// Skybox
			RenderSettings.Skybox.Generate(Application.GraphicsDevice, Application.Content, DemoGame.BlueSkybox, 100);
        }
    }
}