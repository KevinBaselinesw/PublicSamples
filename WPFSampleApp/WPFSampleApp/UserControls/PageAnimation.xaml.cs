using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WPFSampleApp.UserControls
{
    public enum PageAnimationType
    {
        None,
        Fade,
        Slide,
        SlideAndFade,
        Grow,
        GrowAndFade,
        Flip,
        FlipAndFade,
        Spin,
        SpinAndFade
    }

    public partial class PageAnimation : UserControl
    {
        Stack<UserControl> pages = new Stack<UserControl>();

        public UserControl CurrentPage { get; set; }

        public static readonly DependencyProperty TransitionTypeProperty = DependencyProperty.Register("TransitionType",
            typeof(PageAnimationType),
            typeof(PageAnimation), new PropertyMetadata(PageAnimationType.SlideAndFade));

        public PageAnimationType TransitionType
        {
            get
            {
                return (PageAnimationType)GetValue(TransitionTypeProperty);
            }
            set
            {
                SetValue(TransitionTypeProperty, value);
            }
        }

        public PageAnimation()
        {
            InitializeComponent();
        }

        public void ShowPage(UserControl newPage)
        {
            if (TransitionType == PageAnimationType.None)
            {
                contentPresenter.Content = newPage;
                CurrentPage = newPage;
                return;
            }

            pages.Push(newPage);

            Task.Factory.StartNew(() => ShowNewPage());
        }

        void ShowNewPage()
        {
            Dispatcher.Invoke((Action)delegate
            {
                if (contentPresenter.Content != null)
                {
                    UserControl oldPage = contentPresenter.Content as UserControl;

                    if (oldPage != null)
                    {
                        oldPage.Loaded -= newPage_Loaded;

                        UnloadPage(oldPage);
                    }
                }
                else
                {
                    ShowNextPage();
                }

            });
        }

        void ShowNextPage()
        {
            UserControl newPage = pages.Pop();

            newPage.Loaded += newPage_Loaded;

            contentPresenter.Content = newPage;
        }

        void UnloadPage(UserControl page)
        {
            Storyboard hidePage = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();

            hidePage.Completed += hidePage_Completed;

            hidePage.Begin(contentPresenter);
        }

        void newPage_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard showNewPage = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;

            showNewPage.Begin(contentPresenter);

            CurrentPage = sender as UserControl;
        }

        void hidePage_Completed(object sender, EventArgs e)
        {
            contentPresenter.Content = null;

            ShowNextPage();
        }
    }
}
