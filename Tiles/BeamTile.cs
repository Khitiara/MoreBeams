using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MoreBeams.Tiles;

[Autoload(false)]
public class BeamTile : ModTile
{
    public override string Name { get; }
    private readonly int _dust;
    public BeamTile(string name, int dust) {
        Name = name;
        _dust = dust;
    }

    // private static readonly (ushort,ushort,ushort)[] TilesToModify = [
    //     TileID.StinkbugHousingBlocker, TileID.StinkbugHousingBlockerEcho, TileID.Switches, TileID.Torches,
    //     TileID.ProjectilePressurePad,
    // ];
    public override void SetStaticDefaults() {
        TileID.Sets.IsBeam[Type] = true;
        DustType = _dust;
        AddMapEntry(Color.Brown);
        // for (var i = 0; i < TilesToModify.Length; i++) {
        //     TileObjectData.GetTileData(i)
        // }
    }
}

