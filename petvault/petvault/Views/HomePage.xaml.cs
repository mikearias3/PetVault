﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace petvault.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "header");
        }
    }
}
