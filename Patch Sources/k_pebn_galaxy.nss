//:: k_pebn_galaxy
/*
    Brings up the galaxy map with
    the current planet selected
    Planetary.2DA indexes should be
    used instead of PLANET_ constants

0          Endar_Spire
1          Taris
2          Dantooine
3          Kashyyyk
4          Manaan
5          Korriban
6          Tatooine
7          Leviathan
8          Unknown_World
9          Star_Forge

    Modifications by Peter T.
    When the player uses the galaxy map and only Dantooine
    is available (i.e. after Taris, but before being given
    the main quest),

*/
//:: Created By: Preston Watamaniuk
//:: Modified By: Peter Thomas 11/03/03
//:: Copyright (c) 2002 Bioware Corp.

#include "k_inc_debug"

void main()
{
    Db_PostString("Running v3.0", 5, 5, 3.0);
	int nMain = GetGlobalNumber("K_KOTOR_MASTER");
	
    SetPlanetAvailable(PLANET_DANTOOINE, TRUE); //DAT
    SetPlanetSelectable(PLANET_DANTOOINE, TRUE);
	SetPlanetAvailable(PLANET_DANTOOINE, TRUE); //DAT
	SetPlanetSelectable(PLANET_DANTOOINE, TRUE);
	SetPlanetAvailable(PLANET_KASHYYYK, TRUE); //KAS
	SetPlanetSelectable(PLANET_KASHYYYK, TRUE);
	SetPlanetAvailable(PLANET_MANAAN, TRUE); //MAN
	SetPlanetSelectable(PLANET_MANAAN, TRUE);
	SetPlanetAvailable(PLANET_KORRIBAN, TRUE); //KOR
	SetPlanetSelectable(PLANET_KORRIBAN, TRUE);
	SetPlanetAvailable(PLANET_TATOOINE, TRUE); //TAT
	SetPlanetSelectable(PLANET_TATOOINE, TRUE);
	
	if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG1))
	{
		SetPlanetAvailable(11, TRUE);
		SetPlanetSelectable(11, TRUE);
	}
	else if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG2))
	{
		SetPlanetAvailable(12, TRUE);
		SetPlanetSelectable(12, TRUE);
	}
	else if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG3))
	{
		SetPlanetAvailable(13, TRUE);
		SetPlanetSelectable(13, TRUE);
	}
	else if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG4))
	{
		SetPlanetAvailable(14, TRUE);
		SetPlanetSelectable(14, TRUE);
	}
	else if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG5))
	{
		SetPlanetAvailable(15, TRUE);
		SetPlanetSelectable(15, TRUE);
	}
	else if(GetIsLiveContentAvailable(LIVE_CONTENT_PKG6))
	{
		SetPlanetAvailable(16, TRUE);
		SetPlanetSelectable(16, TRUE);
	}

	// Peter T. 11/03/03
	// remove the journal entry about the Ebon Hawk
	Db_PostString("journal removed");
	RemoveJournalQuestEntry("k_ebonhawk");
	
	SetPlanetAvailable(PLANET_UNKNOWN_WORLD, TRUE);
	SetPlanetSelectable(PLANET_UNKNOWN_WORLD, TRUE);
	SetPlanetAvailable(PLANET_STAR_FORGE, TRUE);
	SetPlanetSelectable(PLANET_STAR_FORGE, TRUE);

	int nPlanet = GetGlobalNumber("K_CURRENT_PLANET");
	if(nPlanet == 5)
	{
		ShowGalaxyMap(PLANET_ENDAR_SPIRE);
	}
	else if(nPlanet == 10)
	{
		ShowGalaxyMap(PLANET_TARIS);
	}
	else if(nPlanet == 15)
	{
		ShowGalaxyMap(PLANET_DANTOOINE);
	}
	else if(nPlanet == 20)
	{
		ShowGalaxyMap(PLANET_KASHYYYK);
	}
	else if(nPlanet == 25)
	{
		ShowGalaxyMap(PLANET_MANAAN);
	}
	else if(nPlanet == 30)
	{
		ShowGalaxyMap(PLANET_KORRIBAN);
	}
	else if(nPlanet == 35)
	{
		ShowGalaxyMap(PLANET_TATOOINE);
	}
	else if(nPlanet == 40)
	{
		ShowGalaxyMap(PLANET_LEVIATHAN);
	}
	else if(nPlanet == 45)
	{
		ShowGalaxyMap(PLANET_UNKNOWN_WORLD);
	}
	else if(nPlanet == 50)
	{
		ShowGalaxyMap(PLANET_STAR_FORGE);
	}
	else
	{
		ShowGalaxyMap(PLANET_DANTOOINE);
	}
}
