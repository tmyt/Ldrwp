﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Ldrwp {
    static public class Util {
        static public bool Navigate(this NavigationService n, string localUrlString) {
            return n.Navigate(new Uri(localUrlString, UriKind.Relative));
        }
    }
}
