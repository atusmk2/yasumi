using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace yasumi.Common
{
	public class yasumiConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("Configure Maximum Per-Item Upgrade")]
		[Label("Set Maximum Damage")]
		[Tooltip("Default is 120.\nThis settings only applied post-Moon Lord.")]
		[Increment(10)]
		[Range(120, 1000)]
		[DefaultValue("120")]
		[Slider]
		[ReloadRequired]
		public int maxDamage;
		[Label("Set Maximum Defense")]
 		[Tooltip("Default is 20.\nThis settings only applied post-Moon Lord.")]
		[Range(20, 50)]
		[DefaultValue("20")]
		[Slider]
		[ReloadRequired]
		public int maxDefense;
	}
}
	