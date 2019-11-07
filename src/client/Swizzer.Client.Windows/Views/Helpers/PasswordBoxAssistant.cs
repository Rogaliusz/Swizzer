using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Swizzer.Client.Windows.Views.Helpers
{
    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword
            = DependencyProperty.RegisterAttached(nameof(BoundPassword), typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword
            = DependencyProperty.RegisterAttached(nameof(BindPassword), typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        public static readonly DependencyProperty UpdatingPassword
            = DependencyProperty.RegisterAttached(nameof(UpdatingPassword), typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null || !GetBindPassword(passwordBox))
            {
                return;
            }

            passwordBox.PasswordChanged -= HandlePasswordChanged;

            var password = (string)e.NewValue;

            if (!GetUpdatingPassword(passwordBox))
            {
                passwordBox.Password = password;
            }

            passwordBox.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }

            var wasBound = (bool)e.OldValue;
            var needToBind = (bool)e.NewValue;

            if (wasBound)
            {
                passwordBox.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            SetUpdatingPassword(passwordBox, true);
            SetBoundPassword(passwordBox, passwordBox.Password);
            SetUpdatingPassword(passwordBox, false);
        }

        public static void SetBindPassword(DependencyObject obj, bool value)
            => obj.SetValue(BindPassword, value);
        
        public static bool GetBindPassword(DependencyObject dp)
            => (bool)dp.GetValue(BindPassword);

        public static void SetBoundPassword(DependencyObject dp, string value)
            => dp.SetValue(BoundPassword, value);

        public static string GetBoundPassword(DependencyObject dp)
            => (string)dp.GetValue(BoundPassword);

        private static bool GetUpdatingPassword(DependencyObject dp) 
            => (bool)dp.GetValue(UpdatingPassword);

        private static void SetUpdatingPassword(DependencyObject dp, bool value) 
            => dp.SetValue(UpdatingPassword, value);
    }
}
