void sub1() {
	SetPartyLeader(0xFFFFFFFF);
	int p = 0;
	while (p < 12) {
		if (IsNPCPartyMember(p)) {
			RemoveNPCFromPartyToBase(p);
		}
		(p++);
	}
}

void main() {
	object oDisciple = GetObjectByTag("Disciple", 0);
	if (GetIsObjectValid(oDisciple)) {
		ChangeToStandardFaction(oDisciple, 2);
		//sub1();
		AddAvailableNPCByObject(11, oDisciple);
		SetGlobalNumber("000_Disciple_Joined", 1);
		AddPartyMember(11, oDisciple);
		ShowPartySelectionGUI("", 0xFFFFFFFF, 0xFFFFFFFF, 0);
	}
	else {
		AurPostString("ERROR: Disciple not valid.", 5, 15, 10.0);
	}
}

