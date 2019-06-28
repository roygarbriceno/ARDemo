using UIKit;
using SceneKit;
using ARKit;
using System;
using Foundation;
using System.Linq;

namespace ARDemo.iOS.Delegates
{
    /// <summary>
    /// Delete for AR Rendering
    /// </summary>
    public class PlaceModelDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor != null && anchor is ARPlaneAnchor)
            {
                PlaceAnchorNode(node, anchor as ARPlaneAnchor);
            }
        }
        
        /// <summary>
        ///  Places the anchor node
        /// </summary>        
        void PlaceAnchorNode(SCNNode node, ARPlaneAnchor anchor)
        {
            var plane = SCNPlane.Create(anchor.Extent.X, anchor.Extent.Z);

            var material = new SCNMaterial();
            material.Diffuse.Contents = UIImage.FromFile("art.scnassets/PlaneGrid/grid.png");
            plane.Materials = new[] { material };
            plane.FirstMaterial.Transparency = 0.1f;

            var planeNode = SCNNode.FromGeometry(plane);
            planeNode.Position = new SCNVector3(anchor.Center.X, 0.0f, anchor.Center.Z);
            planeNode.Transform = SCNMatrix4.CreateRotationX((float)(-Math.PI / 2.0));
            node.AddChildNode(planeNode);
        }


        /// <summary>
        /// Something wrong happened, tries again to create the session
        /// </summary>        
        public override void DidFail(ARSession session, NSError error)
        {
            if (error.Code == 102)
            {
                session.Pause();                
                session.Run(new ARWorldTrackingConfiguration
                {
                    AutoFocusEnabled = true,
                    PlaneDetection = ARPlaneDetection.Horizontal,
                    LightEstimationEnabled = true,
                    WorldAlignment = ARWorldAlignment.Gravity
                }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

            }
        }


        //On node update
        public override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARPlaneAnchor planeAnchor)
            {
                var currentPlaneNode = node.ChildNodes.FirstOrDefault();
                if (currentPlaneNode?.Geometry is SCNPlane currentPlane)
                {
                    currentPlaneNode.Position = new SCNVector3(planeAnchor.Center.X, 0.0f, planeAnchor.Center.Z);
                    currentPlane.Width = planeAnchor.Extent.X;
                    currentPlane.Height = planeAnchor.Extent.Z;
                }
            }
        }
    }
}