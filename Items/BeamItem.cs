using Terraria.ID;
using Terraria.ModLoader;

namespace MoreBeams.Items;

[Autoload(false)]
public class BeamItem: ModItem
{
    public override string Name { get; }
    private readonly short _baseWood;
    private readonly ushort _tileId;

    protected override bool CloneNewInstances => true;

    public BeamItem(string name, short baseWood, ushort tileId) {
        Name = name;
        _baseWood = baseWood;
        _tileId = tileId;
    }

    public override void SetDefaults() {
        Item.CloneDefaults(ItemID.WoodenBeam);
        Item.ResearchUnlockCount = 50;
        Item.createTile = _tileId;
    }

    public override void AddRecipes() {
        CreateRecipe(2)
            .AddIngredient(_baseWood)
            .AddTile(TileID.Sawmill)
            .Register();
    }
}