using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MoreBeams.Tiles;

[Autoload(false)]
public class BeamTile : ModTile
{
    public override string Name { get; }
    private readonly int _dust;
    private readonly bool _shouldGlow;
    private Asset<Texture2D> _glow = null!;

    public BeamTile(string name, int dust, bool shouldGlow = false) {
        Name = name;
        _dust = dust;
        _shouldGlow = shouldGlow;
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

    public override void Load()
    {
        if (_shouldGlow) {
            _glow = ModContent.Request<Texture2D>(Texture + "_Glow");
        }
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if(!_shouldGlow) return;

        Tile tile = Main.tile[i, j];

        if(!TileDrawing.IsVisible(tile)) return;


        Vector2 vector = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

        Rectangle glowSourceRect = new(tile.TileFrameX, tile.TileFrameY, 16, 16);
        Vector2 position = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + vector;

        spriteBatch.Draw(_glow.Value, position, glowSourceRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
}

