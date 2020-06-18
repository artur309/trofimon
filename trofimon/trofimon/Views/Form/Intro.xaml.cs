﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Intro : CarouselPage
    {
        public Intro()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new FormTab());
    }
}