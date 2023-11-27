﻿using System;

using Android.Content.Res;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Sharpnado.CollectionView.Droid.Helpers
{
    public class AndroidPlatformHelper : PlatformHelper
    {
        public override int DpToPixels(int dp, Rounding rounding = Rounding.Round) => DpToPixels((double)dp, rounding);

        public override int DpToPixels(double dp, Rounding rounding = Rounding.Round)
        {
            switch (rounding)
            {
                case Rounding.Round:
                    return (int)Math.Round(dp * Resources.System.DisplayMetrics.Density);
                case Rounding.Floor:
                    return (int)Math.Floor(dp * Resources.System.DisplayMetrics.Density);
                default:
                    return (int)Math.Ceiling(dp * Resources.System.DisplayMetrics.Density);
            }
        }

        public override double PixelsToDp(double pixels)
        {
            return Math.Floor((pixels / Resources.System.DisplayMetrics.Density) * 100) / 100;
        }

        public override string DumpNativeViewHierarchy(View formsView, bool verbose)
        {
#if NET6_0_OR_GREATER
            if (formsView.Handler?.PlatformView is not Android.Views.View nativeView)
            {
                return "platform view is null";
            }

            return nativeView.DumpHierarchy(verbose) ?? "null renderer";
#else
            var renderer = Platform.GetRenderer(formsView);
            Platform.SetRenderer(formsView, renderer);
            return renderer?.View.DumpHierarchy(verbose) ?? "null renderer";
#endif
        }

        public override string DumpNativeViewInfo(View formsView)
        {
#if NET6_0_OR_GREATER
            if (formsView.Handler?.PlatformView is not Android.Views.View nativeView)
            {
                return "platform view is null";
            }

            return nativeView.DumpInfo() ?? "null renderer";
#else
            var renderer = Platform.GetRenderer(formsView);
            Platform.SetRenderer(formsView, renderer);
            return renderer?.View.DumpInfo() ?? "null renderer";
#endif
        }
    }
}