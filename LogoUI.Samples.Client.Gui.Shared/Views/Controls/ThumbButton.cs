using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LogoUI.Samples.Client.Gui.Shared.Views.Controls
{
    public class ThumbButton : ContentControl, IDragThumb
    {
        #region Fields

        private static readonly Type s_thisType = typeof(ThumbButton);

        private Point _oldMousePos;

        private bool _dragged;

        #endregion

        #region Dependency Properties

        public static readonly DependencyPropertyKey IsPressedPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsPressed",
                typeof(bool),
                s_thisType,
                new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsPressedProperty =
            IsPressedPropertyKey.DependencyProperty;

        public static readonly DependencyProperty EmulateButtonProperty =
            DependencyProperty.Register(
                "EmulateButton",
                typeof(bool),
                s_thisType,
                new PropertyMetadata(true));

        #endregion

        #region Public Properties

        /// <summary>
        /// Get button pressed state.
        /// </summary>
        [Browsable(false), ReadOnly(true), Category("Appearance")]
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            private set { SetValue(IsPressedPropertyKey, value); }
        }

        public bool EmulateButton
        {
            get { return (bool)GetValue(EmulateButtonProperty); }
            set { SetValue(EmulateButtonProperty, value); }
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent ClickEvent =
            ButtonBase.ClickEvent.AddOwner(s_thisType);

        [Category("Behavior")]
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion

        #region Protected Members

        protected virtual void OnClick()
        {
            Focus();
            RoutedEventArgs e = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(e);
        }

        #endregion

        #region Overrides

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _dragged = false;

            if (e.ClickCount == 1)
            {
                _oldMousePos = Mouse.PrimaryDevice.GetPosition(this);
                IsPressed = CaptureMouse();
                e.Handled = true;
            }

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsPressed)
            {
                e.Handled = true;
                ReleaseMouseCapture();

                if (_dragged)
                {
                    Point mousePos = Mouse.PrimaryDevice.GetPosition(this);
                    Vector delta = mousePos - _oldMousePos;
                    OnDragCompleted(new DragCompletedEventArgs(delta.X, delta.Y, false));
                }
                else if (EmulateButton)
                {
                    OnClick();
                }

                IsPressed = false;
            }

            _dragged = false;
            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsPressed)
            {
                e.Handled = true;

                Point mousePos = Mouse.PrimaryDevice.GetPosition(this);
                Vector delta = mousePos - _oldMousePos;
                if (delta.Length > 2)
                {
                    _dragged = true;
                    OnDragStarted(new DragStartedEventArgs(_oldMousePos.X, _oldMousePos.Y));
                    OnDragDelta(new DragDeltaEventArgs(delta.X, delta.Y));
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            IsPressed = false;
        }

        #endregion

        #region Private Members

        private void OnDragStarted(DragStartedEventArgs e)
        {
            if (DragStarted != null)
            {
                DragStarted(this, e);
            }
        }

        private void OnDragDelta(DragDeltaEventArgs e)
        {
            if (DragDelta != null)
            {
                DragDelta(this, e);
            }
        }

        private void OnDragCompleted(DragCompletedEventArgs e)
        {
            if (DragCompleted != null)
            {
                DragCompleted(this, e);
            }
        }

        #endregion

        #region Implementation of IDragThumb

        public event DragStartedEventHandler DragStarted;

        public event DragCompletedEventHandler DragCompleted;

        public event DragDeltaEventHandler DragDelta;

        public void CancelDrag()
        {
            if (IsPressed)
            {
                IsPressed = false;

                Vector delta = Mouse.PrimaryDevice.GetPosition(this) - _oldMousePos;
                ReleaseMouseCapture();
                OnDragCompleted(new DragCompletedEventArgs(delta.X, delta.Y, false));
            }

        }

        public void BeginDrag()
        {
            Point mousePos = Mouse.PrimaryDevice.GetPosition(this);
            Vector delta = mousePos - _oldMousePos;
            OnDragStarted(new DragStartedEventArgs(_oldMousePos.X, _oldMousePos.Y));
            OnDragDelta(new DragDeltaEventArgs(delta.X, delta.Y));
        }

        #endregion
    }
}