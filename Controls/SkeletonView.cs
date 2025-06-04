using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAnywhere.Mobile.Controls
{
    public class SkeletonView : BoxView
    {
        public SkeletonView()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                this.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith(_ =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.FadeTo(1, 750, Easing.CubicInOut);
                    });
                });

                return true;
            });
        }
    }
}
