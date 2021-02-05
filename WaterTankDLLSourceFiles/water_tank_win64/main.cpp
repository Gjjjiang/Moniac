#include "pch.h"
#include <stddef.h>
#include <stdio.h>
#include "water_tank.h"
#include "rtwtypes.h"
#include <iostream>

extern "C" __declspec(dllexport) water_tankModelClass * createWaterTank() {
	
	water_tankModelClass *water_tank = new water_tankModelClass();
	water_tank->initialize();
	return water_tank;
}

extern "C" __declspec(dllexport) void waterTankDestructor(water_tankModelClass *waterTankModel) {
	waterTankModel->terminate();
	delete waterTankModel;
	waterTankModel = nullptr;
}

extern "C" __declspec(dllexport) void setWaterTankInput(water_tankModelClass * waterTankModel, double flowIn, double flowOut, double startVol) {
	water_tankModelClass::ExtU_water_tank_T in;

	
	//std::cout << "hi";
	in.flow_in = flowIn;
	in.flow_out = flowOut;
	in.StartVol = startVol;
	waterTankModel->setExternalInputs(&in);
}

extern "C" __declspec(dllexport) double getWaterTankVolume(water_tankModelClass * waterTankModel) {
	return waterTankModel->getExternalOutputs().CurrentVol;
}
extern "C" __declspec(dllexport) void waterTankStep(water_tankModelClass *waterTankModel) {
	waterTankModel->step();
}

extern "C" __declspec(dllexport) int test() {
	return 123456789;
}