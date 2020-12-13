using System;
using LightningAlert.Dtos;

namespace LightningAlert.Receivers
{
	public class LightingReceivedEventArgs : EventArgs
	{
		public LightningDto Lightning { get; }

		public LightingReceivedEventArgs(LightningDto lightning)
		{
			Lightning = lightning ?? throw new ArgumentNullException(nameof(lightning));
		}
	}
}
