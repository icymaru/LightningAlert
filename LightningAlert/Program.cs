using System;
using System.Collections.Generic;
using System.Linq;
using LightningAlert.Assets;
using LightningAlert.Receivers;

namespace LightningAlert
{
	class Program
	{
		private static IAssetStore _assetStore;
		private static List<string> _alertTracker;

		static void Main()
		{
			_alertTracker = new List<string>();

			_assetStore = new InMemoryAssetStore();

			var alertReceiver = new LightningReceiver();

			alertReceiver.Received += AlertReceiver_Received;

			alertReceiver.Listen();
		}

		private static void AlertReceiver_Received(object sender, LightingReceivedEventArgs e)
		{
			var asset = _assetStore.Get(e.Lightning.Latitude, e.Lightning.Longitude, 12);
			if (asset is null)
				return;

			var alreadyAlerted = _alertTracker.Any(a => a == asset.QuadKey);
			if (alreadyAlerted)
				return;

			_alertTracker.Add(asset.QuadKey);

			Console.WriteLine($"lightning alert for {asset.AssetOwner}:{asset.AssetName}");
		}
	}
}
