using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LightningAlert.Dtos;
using LightningAlert.Utils;

namespace LightningAlert.Assets
{
	public class InMemoryAssetStore : IAssetStore
	{
		private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "assets.json");
		private readonly Dictionary<string, AssetDto> _assets = new Dictionary<string, AssetDto>();

		public InMemoryAssetStore()
		{
			LoadData();
		}

		public AssetDto Get(double latitude, double longitude, int levelOfDetail)
		{
			var quadKey = TileSystem.LatLongToQuadKey(latitude, longitude, levelOfDetail);

			return _assets.GetValueOrDefault(quadKey);
		}

		private void LoadData()
		{
			if (!File.Exists(_path))
			{
				Console.WriteLine("No asset file has been loaded.");
				return;
			}

			try
			{
				var file = File.ReadAllBytes(_path);
				var assets = JsonSerializer.Deserialize<IEnumerable<AssetDto>>(file, Constants.JsonSerializerOptions);

				foreach (var asset in assets)
				{
					if (_assets.ContainsKey(asset.QuadKey))
						continue;

					_assets.Add(asset.QuadKey, asset);
				}
			}
			catch (JsonException)
			{
				Console.WriteLine("Asset file content is not a valid JSON.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unknown error occured. {ex.Message}");
			}
		}
	}
}
