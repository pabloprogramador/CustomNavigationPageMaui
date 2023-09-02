﻿using System;
using System.ComponentModel;
using Android.Widget;
using AndroidX.Fragment.App;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using AView = Android.Views.View;
using Color = Microsoft.Maui.Graphics.Color;
using LP = Android.Views.ViewGroup.LayoutParams;

namespace Plugins.CNPM.Platforms.Android
{
	public class CustomShellRenderer : ShellRenderer
	{
        IShellFlyoutRenderer _flyoutView;
        IMauiContext _mauiContext;
        bool _disposed;
        IShellItemRenderer _currentView;

        public CustomShellRenderer()
		{
		}

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            return new CustomShellItemRenderer(this);
        }

        



        protected override void SwitchFragment(FragmentManager manager, global::Android.Views.View targetView, ShellItem newItem, bool animate = true)
        {
           

            var previousView = _currentView;
            _currentView = CreateShellItemRenderer(newItem);
            _currentView.ShellItem = newItem;
            var fragment = _currentView.Fragment;

            FragmentTransaction transaction = manager.BeginTransaction();


            if (animate)
                transaction.SetTransition(Microsoft.Maui.Controls.Resource.Animation.enterfromleft);
                //transaction.SetTransition((int)global::Android.App.FragmentTransit.FragmentClose);
                //transaction.SetCustomAnimations(Microsoft.Maui.Controls.Resource.Animation.enterfromleft, Microsoft.Maui.Controls.Resource.Animation.ent);

                transaction.Replace(targetView.Id, fragment);

            // Don't force the commit if this is our first load 
            if (previousView == null)
            {
                transaction
                    .SetReorderingAllowed(true);
            }

            transaction.CommitAllowingStateLoss();

            void OnDestroyed(object sender, EventArgs args)
            {
                previousView.Destroyed -= OnDestroyed;

                previousView.Dispose();
                previousView = null;
            }

            if (previousView != null)
                previousView.Destroyed += OnDestroyed;
            //base.SwitchFragment(manager, targetView, newItem, animate);
        }
    }
}

