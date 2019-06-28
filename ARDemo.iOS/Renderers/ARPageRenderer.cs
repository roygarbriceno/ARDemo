using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SceneKit;
using UIKit;
using ARKit;
using CoreGraphics;
using Foundation;
using OpenTK;
using System.Linq;
using System;
using ARDemo.iOS.Delegates;
using ARDemo.iOS.Renderers;
using ARDemo.Core.Pages;

[assembly: ExportRenderer(typeof(ARPage), typeof(ARPageRenderer))]
namespace ARDemo.iOS.Renderers
{
    //Page Renderer to display AR Screen View from Forms Code, implementing AR ScreenView Delegate
    public class ARPageRenderer : PageRenderer, IARSCNViewDelegate
    {
        private ARSCNView sceneView;

        public ARPageRenderer() 
        {
            this.sceneView = new ARSCNView();
         
        }

        public override bool ShouldAutorotate() => true;


    
        /// <summary>
        /// Setup the frame for the sceneview
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.sceneView = new ARSCNView
            {
                Frame = this.View.Frame,
                UserInteractionEnabled = true,
                Delegate = new PlaceModelDelegate(),
                Session =
                {
                    Delegate = new SessionDelegate()
                },
            };
            this.View.AddSubview(this.sceneView);
        }



        /// <summary>
        /// Load the resources for the scene
        /// </summary>        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.sceneView.Session.Run(new ARWorldTrackingConfiguration
            {
                AutoFocusEnabled = true,
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true,
                WorldAlignment = ARWorldAlignment.GravityAndHeading
            }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

        }


        /// <summary>
        /// Pause the AR scene
        /// </summary>        
        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.sceneView.Session.Pause();
        }


        /// <summary>
        /// Handle the touches
        /// </summary>        
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var loc = touch.LocationInView(this.sceneView);
                var worldPos = WorldPositionFromHitTest(loc);
                if (worldPos.Item1.HasValue)
                {
                    PlaceModel(worldPos.Item1.Value);
                }
            }
        }



        private SCNVector3 PositionFromTransform(NMatrix4 xform)
        {
            return new SCNVector3(xform.M14, xform.M24, xform.M34);
        }

   
        /// <summary>
        /// Getting world position from touch hit
        /// </summary>
        Tuple<SCNVector3?, ARAnchor> WorldPositionFromHitTest(CGPoint pt)
        {
            //Hit test against existing anchors
            var hits = this.sceneView.HitTest(pt, ARHitTestResultType.ExistingPlaneUsingExtent);
            if (hits != null && hits.Length > 0)
            {
                var anchors = hits.Where(r => r.Anchor is ARPlaneAnchor);
                if (anchors.Count() > 0)
                {
                    var first = anchors.First();
                    var pos = PositionFromTransform(first.WorldTransform);
                    return new Tuple<SCNVector3?, ARAnchor>(pos, (ARPlaneAnchor)first.Anchor);
                }
            }
            return new Tuple<SCNVector3?, ARAnchor>(null, null);
        }


        /// <summary>
        /// Places the model
        /// </summary>        
        private void PlaceModel(SCNVector3 pos)
        {
            var asset = $"art.scnassets/Andy/andy.obj";
            var texture = $"art.scnassets/Andy/andy.png";
            var model = CreateModelFromFile(asset, texture, "andy", pos);
            if (model == null) return;            
            this.sceneView.Scene.RootNode.AddChildNode(model);            
        }


        /// <summary>
        /// Loads the model from file
        /// </summary>     
        private SCNNode CreateModelFromFile(string modelName, string textureName, string nodeName, SCNVector3 vector)
        {
            try
            {
                var mat = new SCNMaterial();
                mat.Diffuse.Contents = UIImage.FromFile(textureName);
                mat.LocksAmbientWithDiffuse = true;
               
                var scene = SCNScene.FromFile(modelName);
                var geometry = scene.RootNode.ChildNodes[0].Geometry;
                var modelNode = new SCNNode
                {
                    Position = vector,
                    Geometry = geometry,
                    Scale = new SCNVector3(1.0f, 1.0f, 1.0f)
                };
                modelNode.Geometry.Materials = new[] {mat};                               
                return modelNode;
            }
            catch(Exception ex)
            {
                var e = ex.Message;
            }

            return null;

        }
    }
}