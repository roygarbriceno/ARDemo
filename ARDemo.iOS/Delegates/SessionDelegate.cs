using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARKit;
using Foundation;
using UIKit;

namespace ARDemo.iOS.Delegates
{
    public class SessionDelegate : ARSessionDelegate
    {
        public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
        {
            var state = "";
            var reason = "";

            switch (camera.TrackingState)
            {
                case ARTrackingState.NotAvailable:
                    state = "Tracking Not Available";
                    break;
                case ARTrackingState.Normal:
                    state = "Tracking Normal";
                    break;
                case ARTrackingState.Limited:
                    state = "Tracking Limited";
                    switch (camera.TrackingStateReason)
                    {
                        case ARTrackingStateReason.ExcessiveMotion:
                            reason = "because of excessive motion";
                            break;
                        case ARTrackingStateReason.Initializing:
                            reason = "because tracking is initializing";
                            break;
                        case ARTrackingStateReason.InsufficientFeatures:
                            reason = "because of insufficient features in the environment";
                            break;
                        case ARTrackingStateReason.None:
                            reason = "because of an unknown reason";
                            break;
                    }
                    break;
            }

            // Inform user
            Console.WriteLine("{0} {1}", state, reason);
        }      
    }

}