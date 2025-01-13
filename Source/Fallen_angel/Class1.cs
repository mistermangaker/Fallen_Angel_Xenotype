using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse; 


namespace Fallen_angel
{
    public class Class1
    {
    }

    public class PlaceWorker_placeonwall : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            AcceptanceReport result = false;
            if (loc.Impassable(map))
            {
                result = true;
            }
            if (!loc.Impassable(map))
            {
                return "need wall dingus";
            }
            return result;
        }
    }

   

    public class PlaceWorker_ongroundonly : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            AcceptanceReport result = false;
            CellRect cellRect = GenAdj.OccupiedRect(loc, rot, checkingDef.Size);
            bool notblocked = true;
            bool outmap = false;
            foreach (IntVec3 item in cellRect)
            {
                IntVec3 c = item - rot.FacingCell;
                if (item.Impassable(map))
                {
                    result = "Oi you daft cunt. you cant place that in a wall";
                    notblocked = false;
                }
                
                if ((item.InNoBuildEdgeArea(map) || c.InNoBuildEdgeArea(map)))
                {
                    result = "Oi you daft cunt you cant place that there. its too close to the map edge";
                    outmap = true;
                }
            }
            if (notblocked && !outmap)
            {
                result = true;
            }
            return result;
        }
    }

    public class CompProperties_LickCheck : CompProperties_AbilityEffect
    {
        public CompProperties_LickCheck()
        {
            compClass = typeof(CompAbilityEffect_LickCheck);
        }
    }

    public class CompAbilityEffect_LickCheck : CompAbilityEffect
    {
        public new CompProperties_LickCheck Props => (CompProperties_LickCheck)props;

        public override bool HideTargetPawnTooltip => true;

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return false;
            }
            if (!AbilityUtility.ValidateMustBeHuman(pawn, throwMessages, parent))
            {
                return false;
            }
            if (!AbilityUtility.ValidateSickOrInjured(pawn, throwMessages, parent))
            {
                return false;
            }
            return true;
        }
    }



}
