// Prototypes
int GetCurrentPlanet() {
	//The game figures out what planet we're on using the 
	//Ebon Hawk background of all things
	//This isn't my code, but it works
	int nGlobal = GetGlobalNumber("003EBO_BACKGROUND");
	switch (nGlobal) {
		case 0:
			return 2; //Ebon Hawk
			break;
		case 1:
			return 10; //Cit Station Imprison Cutscene
			break;
		case 2:
			return 9; //Telos
			break;
		case 3:
			return 6; //Nar Shad
			break;
		case 4:
			return 1; //Dxun
			break;
		case 5:
			return 0; //Dan
			break;
		case 6:
			return 3; //Korr
			break;
		case 7:
			return 4; //M4-78
			break;
		case 8:
			return 2; //EBon Hawk again?
			break;
		case 9:
			return 5; //Malachor
			break;
		case 10:
			return 2; //Also Ebon Hawk
			break;
		default:
			return 2; //Ebon Hawk
	}
	return 2;
}

void main() {
	int id = 0;
	while (id < 11) {
		SetPlanetAvailable(id, 1);
		SetPlanetSelectable(id, 1);
		(++id);
	}
	
	SetPlanetAvailable(10, 0); //Get rid of Cit Station Cutscene
	SetPlanetSelectable(10, 0);
	SetPlanetAvailable(4, 0); //No M4-78
	SetPlanetSelectable(4, 0);
	SetPlanetAvailable(2, 0); //No Ebon Hawk
	SetPlanetSelectable(2, 0);
	int curr_Id = GetCurrentPlanet();
	if (((GetGlobalNumber("003EBO_BACKGROUND") == 8) || (GetGlobalNumber("003EBO_BACKGROUND") == 10))) {
		curr_Id = 2;
		SetPlanetAvailable(2, 1);
	}
	SetPlanetSelectable(curr_Id, 0);
	ShowGalaxyMap(curr_Id);
}

