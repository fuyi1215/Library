﻿using System;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;

namespace Library
{
	public class ScanListView : ContentPage
	{
		ZXingScannerPage scanPage;
		Button buttonScanDefaultOverlay;
		Button buttonScanCustomOverlay;
		Button buttonScanContinuously;
		Button buttonScanCustomPage;
		Button buttonGenerateBarcode;

				public ScanListView() : base ()
		        {
			buttonScanDefaultOverlay = new Button
			{
				Text = "Scan with Default Overlay",
				AutomationId = "scanWithDefaultOverlay",
			};
			buttonScanDefaultOverlay.Clicked += async delegate
			{
				scanPage = new ZXingScannerPage();
				scanPage.OnScanResult += (result) =>
				{
					scanPage.IsScanning = false;

					Device.BeginInvokeOnMainThread( ()=>
					{
						Navigation.PopAsync();

						DisplayAlert("Scanned Barcode", result.Text, "OK");
					});
				};

				await Navigation.PushAsync(scanPage);
			};


			buttonScanCustomOverlay = new Button
			{
				Text = "Scan with Custom Overlay",
				AutomationId = "scanWithCustomOverlay",
			};
			buttonScanCustomOverlay.Clicked += async delegate
			{
				// Create our custom overlay
				var customOverlay = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.FillAndExpand
				};
				var torch = new Button
				{
					Text = "Toggle Torch"
				};
				torch.Clicked += delegate
				{
					scanPage.ToggleTorch();
				};
				customOverlay.Children.Add(torch);

				scanPage = new ZXingScannerPage(customOverlay: customOverlay);
				scanPage.OnScanResult += (result) =>
				{
					scanPage.IsScanning = false;

					Device.BeginInvokeOnMainThread(() =>
					{
						Navigation.PopAsync();
						DisplayAlert("Scanned Barcode", result.Text, "OK");
					});
				};
				await Navigation.PushAsync(scanPage);
			};


			buttonScanContinuously = new Button
			{
				Text = "Scan Continuously",
				AutomationId = "scanContinuously",
			};
			buttonScanContinuously.Clicked += async delegate
			{
				scanPage = new ZXingScannerPage();
				scanPage.OnScanResult += (result) =>
					Device.BeginInvokeOnMainThread(() =>
					   DisplayAlert("Scanned Barcode", result.Text, "OK"));

				await Navigation.PushAsync(scanPage);
			};

			buttonScanCustomPage = new Button
			{
				Text = "Scan with Custom Page",
				AutomationId = "scanWithCustomPage",
			};
			//.Clicked += async delegate
			//{
				//var customScanPage = new CustomScanPage();
				//await Navigation.PushAsync(customScanPage);
			//};


			buttonGenerateBarcode = new Button
			{
				Text = "Barcode Generator",
				AutomationId = "barcodeGenerator",
			};
			buttonGenerateBarcode.Clicked += async delegate
			{
				string sfdafds;
				await Navigation.PushAsync(new BarCodePage());
			};

			var stack = new StackLayout();
			stack.Children.Add(buttonScanDefaultOverlay);
			stack.Children.Add(buttonScanCustomOverlay);
			stack.Children.Add(buttonScanContinuously);
			stack.Children.Add(buttonScanCustomPage);
			stack.Children.Add(buttonGenerateBarcode);

			Content = stack;   }
	}
}

