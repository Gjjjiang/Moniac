//
// Academic License - for use in teaching, academic research, and meeting
// course requirements at degree granting institutions only.  Not for
// government, commercial, or other organizational use.
//
// File: ert_main.cpp
//
// Code generated for Simulink model 'water_tank'.
//
// Model version                  : 1.54
// Simulink Coder version         : 9.4 (R2020b) 29-Jul-2020
// C/C++ source code generated on : Sat Jan 30 02:45:32 2021
//
// Target selection: ert.tlc
// Embedded hardware selection: Intel->x86-64 (Windows64)
// Code generation objectives: Unspecified
// Validation result: Not run
//
#include <stddef.h>
#include <stdio.h>                // This ert_main.c example uses printf/fflush
#include "water_tank.h"                // Model's header file
#include "rtwtypes.h"
#include <iostream>

 static water_tankModelClass water_tank_Obj;// Instance of model class
 static water_tankModelClass watertank2;
//
// Associating rt_OneStep with a real-time clock or interrupt service routine
// is what makes the generated code "real-time".  The function rt_OneStep is
// always associated with the base rate of the model.  Subrates are managed
// by the base rate from inside the generated code.  Enabling/disabling
// interrupts and floating point context switches are target specific.  This
// example code indicates where these should take place relative to executing
// the generated code step function.  Overrun behavior should be tailored to
// your application needs.  This example simply sets an error status in the
// real-time model and returns from rt_OneStep.
//

//
// The example "main" function illustrates what is required by your
// application code to initialize, execute, and terminate the generated code.
// Attaching rt_OneStep to a real-time clock is target specific.  This example
// illustrates how you do this relative to initializing the model.
//
int_T main(int_T argc, const char *argv[])
{
  // Unused arguments
  (void)(argc);
  (void)(argv);
    water_tankModelClass::ExtU_water_tank_T obj_in, tank2;
    obj_in.flow_in = 0;
    obj_in.flow_out = 0;
    obj_in.StartVol = 10;


    tank2.flow_in = 0;
    tank2.flow_out = 0;
    tank2.StartVol = 0;
  // Initialize model
  water_tank_Obj.initialize();
  watertank2.initialize();
  
  water_tank_Obj.setExternalInputs(&obj_in);
  watertank2.setExternalInputs(&tank2);
  //watertank2.setExternalInputs();

  // Simulating the model step behavior (in non real-time) to
  //   simulate model behavior at stop time.

  while (true) {
      water_tank_Obj.step();
    watertank2.step();
    std::cout << water_tank_Obj.getExternalOutputs().CurrentVol<<" "<< watertank2.getExternalOutputs().CurrentVol << std::endl;
  }

  // Disable rt_OneStep() here

  // Terminate model
  water_tank_Obj.terminate();
  return 0;
}

//
// File trailer for generated code.
//
// [EOF]
//
