using Godot;
using System;
using Data;

// A dummy class currently. It will include damage mostly. Ranged & Melee will branch off of this perhaps. Or each be seperate. Early days on that.
public partial class Attack : Ability
{
	[Export] private float m_damage;
	[Export] private Shape2D m_hitbox;

    public override void Activate()
    {
        //Either move TESTFUNC here once its applicable, or call other functions from here. Do whatever
    }

    private void TESTFUNC(Node2D player){
        
        //GameManager.Instance.GetWorld2D(); //Both are valid approaches.
        var space = player.GetWorld2D().DirectSpaceState;
        var query = new PhysicsShapeQueryParameters2D{
            CollideWithAreas = true,
            Shape = m_hitbox,
            Transform = Transform2D.Identity.Translated(player.GlobalPosition), // This will take in position & size. This should be affected by items. Not sure how we'll do that but thats for later.
            //Collision mask, set to only hit enemies. Perhaps have some items enable contact damage. Work out later.
        };

        //Returns EVERYTHING that intersects.
        //Should be fine, alter behaviour as necessary.
        var result = space.IntersectShape(query);

        //Announce to the world that damage will be dealt to targeted creatures
        //Let items hear the signal and apply effects/maybe influence damage then?
        //Damage should probably be set at the start.
        
    }
}
