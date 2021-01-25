//:: k_ren_visionland
/*
     Play the vision STUNT_00 land sequence
*/
//:: Created By: Preston Watamaniuk
//:: Copyright (c) 2002 Bioware Corp.

#include "k_inc_debug"
#include "k_inc_stunt"

void main()
{
	int current_planet = GetGlobalNumber("K_CURRENT_PLANET");
	int future_planet = GetGlobalNumber("K_FUTURE_PLANET");
    if(current_planet == 5)
    {
        StartNewModule("tar_m02af","","01f");
    }
    // the special circumstances for the Dantooine version of Stunt_00
    else if(GetGlobalBoolean("DAN_STUNT00"))
    {
        SetGlobalBoolean("DAN_STUNT00",FALSE);
        StartNewModule("Danm13","","09");
    }
    else if(future_planet == 20 || future_planet == 25 || future_planet == 30 || future_planet == 35)
    {
        ST_PlayVisionLanding();
    }
	else if(current_planet == 10)
	{
		StartNewModule("tar_m02af");
	}
	else
	{
		StartNewModule("ebo_m12aa");
	}
}

