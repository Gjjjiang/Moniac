//
// Academic License - for use in teaching, academic research, and meeting
// course requirements at degree granting institutions only.  Not for
// government, commercial, or other organizational use.
//
// File: water_tank.cpp
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
#include "pch.h"
#include "water_tank.h"
#include "water_tank_private.h"

//
// This function updates continuous states using the ODE3 fixed-step
// solver algorithm
//
void water_tankModelClass::rt_ertODEUpdateContinuousStates(RTWSolverInfo *si )
{
  // Solver Matrices
  static const real_T rt_ODE3_A[3] = {
    1.0/2.0, 3.0/4.0, 1.0
  };

  static const real_T rt_ODE3_B[3][3] = {
    { 1.0/2.0, 0.0, 0.0 },

    { 0.0, 3.0/4.0, 0.0 },

    { 2.0/9.0, 1.0/3.0, 4.0/9.0 }
  };

  time_T t = rtsiGetT(si);
  time_T tnew = rtsiGetSolverStopTime(si);
  time_T h = rtsiGetStepSize(si);
  real_T *x = rtsiGetContStates(si);
  ODE3_IntgData *id = static_cast<ODE3_IntgData *>(rtsiGetSolverData(si));
  real_T *y = id->y;
  real_T *f0 = id->f[0];
  real_T *f1 = id->f[1];
  real_T *f2 = id->f[2];
  real_T hB[3];
  int_T i;
  int_T nXc = 1;
  rtsiSetSimTimeStep(si,MINOR_TIME_STEP);

  // Save the state values at time t in y, we'll use x as ynew.
  (void) std::memcpy(y, x,
                     static_cast<uint_T>(nXc)*sizeof(real_T));

  // Assumes that rtsiSetT and ModelOutputs are up-to-date
  // f0 = f(t,y)
  rtsiSetdX(si, f0);
  water_tank_derivatives();

  // f(:,2) = feval(odefile, t + hA(1), y + f*hB(:,1), args(:)(*));
  hB[0] = h * rt_ODE3_B[0][0];
  for (i = 0; i < nXc; i++) {
    x[i] = y[i] + (f0[i]*hB[0]);
  }

  rtsiSetT(si, t + h*rt_ODE3_A[0]);
  rtsiSetdX(si, f1);
  this->step();
  water_tank_derivatives();

  // f(:,3) = feval(odefile, t + hA(2), y + f*hB(:,2), args(:)(*));
  for (i = 0; i <= 1; i++) {
    hB[i] = h * rt_ODE3_B[1][i];
  }

  for (i = 0; i < nXc; i++) {
    x[i] = y[i] + (f0[i]*hB[0] + f1[i]*hB[1]);
  }

  rtsiSetT(si, t + h*rt_ODE3_A[1]);
  rtsiSetdX(si, f2);
  this->step();
  water_tank_derivatives();

  // tnew = t + hA(3);
  // ynew = y + f*hB(:,3);
  for (i = 0; i <= 2; i++) {
    hB[i] = h * rt_ODE3_B[2][i];
  }

  for (i = 0; i < nXc; i++) {
    x[i] = y[i] + (f0[i]*hB[0] + f1[i]*hB[1] + f2[i]*hB[2]);
  }

  rtsiSetT(si, tnew);
  rtsiSetSimTimeStep(si,MAJOR_TIME_STEP);
}

// Model step function
void water_tankModelClass::step()
{
  if (rtmIsMajorTimeStep((&water_tank_M))) {
    // set solver stop time
    rtsiSetSolverStopTime(&(&water_tank_M)->solverInfo,(((&water_tank_M)
      ->Timing.clockTick0+1)*(&water_tank_M)->Timing.stepSize0));
  }                                    // end MajorTimeStep

  // Update absolute time of base rate at minor time step
  if (rtmIsMinorTimeStep((&water_tank_M))) {
    (&water_tank_M)->Timing.t[0] = rtsiGetT(&(&water_tank_M)->solverInfo);
  }

  // Integrator: '<Root>/Integrator2' incorporates:
  //   Inport: '<Root>/StartVol'

  if (water_tank_DW.Integrator2_IWORK != 0) {
    water_tank_X.Integrator2_CSTATE = water_tank_U.StartVol;
  }

  // Outport: '<Root>/CurrentVol' incorporates:
  //   Integrator: '<Root>/Integrator2'

  water_tank_Y.CurrentVol = water_tank_X.Integrator2_CSTATE;

  // MATLAB Function: '<Root>/Water_Tank_Function' incorporates:
  //   Inport: '<Root>/flow_in'
  //   Inport: '<Root>/flow_out'

  water_tank_B.dVdt = water_tank_U.flow_in - water_tank_U.flow_out;
  if (rtmIsMajorTimeStep((&water_tank_M))) {
    // Update for Integrator: '<Root>/Integrator2'
    water_tank_DW.Integrator2_IWORK = 0;
  }                                    // end MajorTimeStep

  if (rtmIsMajorTimeStep((&water_tank_M))) {
    rt_ertODEUpdateContinuousStates(&(&water_tank_M)->solverInfo);

    // Update absolute time for base rate
    // The "clockTick0" counts the number of times the code of this task has
    //  been executed. The absolute time is the multiplication of "clockTick0"
    //  and "Timing.stepSize0". Size of "clockTick0" ensures timer will not
    //  overflow during the application lifespan selected.

    ++(&water_tank_M)->Timing.clockTick0;
    (&water_tank_M)->Timing.t[0] = rtsiGetSolverStopTime(&(&water_tank_M)
      ->solverInfo);

    {
      // Update absolute timer for sample time: [0.016666666666666666s, 0.0s]
      // The "clockTick1" counts the number of times the code of this task has
      //  been executed. The resolution of this integer timer is 0.016666666666666666, which is the step size
      //  of the task. Size of "clockTick1" ensures timer will not overflow during the
      //  application lifespan selected.

      (&water_tank_M)->Timing.clockTick1++;
    }
  }                                    // end MajorTimeStep
}

// Derivatives for root system: '<Root>'
void water_tankModelClass::water_tank_derivatives()
{
  water_tankModelClass::XDot_water_tank_T *_rtXdot;
  _rtXdot = ((XDot_water_tank_T *) (&water_tank_M)->derivs);

  // Derivatives for Integrator: '<Root>/Integrator2'
  _rtXdot->Integrator2_CSTATE = water_tank_B.dVdt;
}

// Model initialize function
void water_tankModelClass::initialize()
{
  // Registration code
  {
    // Setup solver object
    rtsiSetSimTimeStepPtr(&(&water_tank_M)->solverInfo, &(&water_tank_M)
                          ->Timing.simTimeStep);
    rtsiSetTPtr(&(&water_tank_M)->solverInfo, &rtmGetTPtr((&water_tank_M)));
    rtsiSetStepSizePtr(&(&water_tank_M)->solverInfo, &(&water_tank_M)
                       ->Timing.stepSize0);
    rtsiSetdXPtr(&(&water_tank_M)->solverInfo, &(&water_tank_M)->derivs);
    rtsiSetContStatesPtr(&(&water_tank_M)->solverInfo, (real_T **)
                         &(&water_tank_M)->contStates);
    rtsiSetNumContStatesPtr(&(&water_tank_M)->solverInfo, &(&water_tank_M)
      ->Sizes.numContStates);
    rtsiSetNumPeriodicContStatesPtr(&(&water_tank_M)->solverInfo,
      &(&water_tank_M)->Sizes.numPeriodicContStates);
    rtsiSetPeriodicContStateIndicesPtr(&(&water_tank_M)->solverInfo,
      &(&water_tank_M)->periodicContStateIndices);
    rtsiSetPeriodicContStateRangesPtr(&(&water_tank_M)->solverInfo,
      &(&water_tank_M)->periodicContStateRanges);
    rtsiSetErrorStatusPtr(&(&water_tank_M)->solverInfo, (&rtmGetErrorStatus
      ((&water_tank_M))));
    rtsiSetRTModelPtr(&(&water_tank_M)->solverInfo, (&water_tank_M));
  }

  rtsiSetSimTimeStep(&(&water_tank_M)->solverInfo, MAJOR_TIME_STEP);
  (&water_tank_M)->intgData.y = (&water_tank_M)->odeY;
  (&water_tank_M)->intgData.f[0] = (&water_tank_M)->odeF[0];
  (&water_tank_M)->intgData.f[1] = (&water_tank_M)->odeF[1];
  (&water_tank_M)->intgData.f[2] = (&water_tank_M)->odeF[2];
  (&water_tank_M)->contStates = ((X_water_tank_T *) &water_tank_X);
  rtsiSetSolverData(&(&water_tank_M)->solverInfo, static_cast<void *>
                    (&(&water_tank_M)->intgData));
  rtsiSetSolverName(&(&water_tank_M)->solverInfo,"ode3");
  rtmSetTPtr((&water_tank_M), &(&water_tank_M)->Timing.tArray[0]);
  (&water_tank_M)->Timing.stepSize0 = 0.016666666666666666;
  rtmSetFirstInitCond((&water_tank_M), 1);

  // InitializeConditions for Integrator: '<Root>/Integrator2'
  if (rtmIsFirstInitCond((&water_tank_M))) {
    water_tank_X.Integrator2_CSTATE = 0.0;
  }

  water_tank_DW.Integrator2_IWORK = 1;

  // End of InitializeConditions for Integrator: '<Root>/Integrator2'

  // set "at time zero" to false
  if (rtmIsFirstInitCond((&water_tank_M))) {
    rtmSetFirstInitCond((&water_tank_M), 0);
  }
}

// Model terminate function
void water_tankModelClass::terminate()
{
  // (no terminate code required)
}

// Constructor
water_tankModelClass::water_tankModelClass() :
  water_tank_B(),
  water_tank_DW(),
  water_tank_X(),
  water_tank_U(),
  water_tank_Y(),
  water_tank_M()
{
  // Currently there is no constructor body generated.
}

// Destructor
water_tankModelClass::~water_tankModelClass()
{
  // Currently there is no destructor body generated.
}

// Real-Time Model get method
water_tankModelClass::RT_MODEL_water_tank_T * water_tankModelClass::getRTM()
{
  return (&water_tank_M);
}

//
// File trailer for generated code.
//
// [EOF]
//
