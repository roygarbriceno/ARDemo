using Android.App;
using Android.Content;
using Android.Views;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using Google.AR.Sceneform.UX;
using ARDemo.Core.Pages;
using ARDemo.Core.ViewModels;
using ARDemo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ARPage), typeof(ARPageRenderer))]
namespace ARDemo.Droid.Renderers
{

    /// <summary>
    /// Page Renderer for our AR session
    /// </summary>
    public class ARPageRenderer : PageRenderer
    {
        private readonly Context context;
        private ArFragment arFragment;
        private ModelRenderable andyRenderable;
        private Android.Views.View view;
        private ARViewModel viewModel;


        public ARPageRenderer(Context context) : base(context)
        {
            this.AutoPackage = false;
            this.context = context;
        }


        /// <summary>
        /// Gets a reference to the ViewModel and the ArFragment
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Activity;

            this.viewModel = this.Element.BindingContext as ARViewModel;

            this.view = activity.LayoutInflater.Inflate(Resource.Layout.ARLayout, this, false);
            AddView(this.view);

            this.arFragment = activity.GetFragmentManager().FindFragmentById(Resource.Id.ar_fragment) as ArFragment;
            if (this.arFragment != null)
            {
                ModelRenderable.InvokeBuilder().SetSource(this.context, Resource.Raw.andy).Build(renderable =>
                {
                    andyRenderable = renderable;
                });
                arFragment.TapArPlane += OnTapArPlane;
            }
        }


        /// <summary>
        /// Fix the layout measures to fill the whole view
        /// </summary>        
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            this.view.Measure(msw, msh);
            this.view.Layout(0, 0, r - l, b - t);
        }


        /// <summary>
        /// Remoev the AR fragment when the pages closes otherwise will throw an error when returning
        /// </summary>
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            var activity = this.Context as Activity;
            activity.GetFragmentManager().BeginTransaction().Remove(this.arFragment).Commit();
        }



        /// <summary>
        /// Adds a model whe the user taps on the plane
        /// </summary>        
        private void OnTapArPlane(object sender, BaseArFragment.TapArPlaneEventArgs e)
        {
            if (andyRenderable == null) return;

            // Create the Anchor.
            var anchor = e.HitResult.CreateAnchor();
            var anchorNode = new AnchorNode(anchor);
            anchorNode.SetParent(arFragment.ArSceneView.Scene);

            // Create the transformable andy and add it to the anchor.
            var andy = new TransformableNode(arFragment.TransformationSystem);
            andy.SetParent(anchorNode);
            andy.Renderable = andyRenderable;
            andy.Select();
        }
    }
}