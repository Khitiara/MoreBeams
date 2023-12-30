using MoreBeams.Items;
using MoreBeams.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoreBeams
{
	public class MoreBeams : Mod
	{
		private static readonly (string, short, short)[] Woods = {
			("AshWood", DustID.Ash, ItemID.AshWood),
			("Ebonwood", DustID.Ebonwood, ItemID.Ebonwood),
			("Shadewood", DustID.Shadewood, ItemID.Shadewood),
			("PalmWood", DustID.PalmWood, ItemID.PalmWood),
			("DynastyWood", DustID.DynastyWood, ItemID.DynastyWood),
			("Pearlwood", DustID.Pearlwood, ItemID.Pearlwood),
			("SpookyWood", DustID.SpookyWood, ItemID.SpookyWood),
		};

		// private List<int>     _beamTilesAdded  = new();
		// private Dictionary<(int,int), int[]> _restoreTileData = new(5);
		// private static readonly int[] vanillaTilesToModify = new[] {
		// 	442,593,630,631,136,
		// };
		public override void Load() {
			foreach ((string wood, short dust, short item) in Woods) {
				BeamTile tile = new($"{wood}Beam", dust);
				AddContent(tile);
				// _beamTilesAdded.Add(tile.Type);
				AddContent(new BeamItem($"{wood}BeamItem", item, tile.Type));
			}

			// for (int index = 0; index < vanillaTilesToModify.Length; index++) {
			// 	int tile = vanillaTilesToModify[index];
			// 	TileObjectData data = TileObjectData.GetTileData(tile,0);
			// 	for (int i = 1; i <= data.AlternatesCount; i++) {
			// 		TileObjectData altData = TileObjectData.GetTileData(tile, 0, i);
			//
			// 	}
			// }
		}

		// public override void Unload() {
		// 	base.Unload();
		// }
	}
}