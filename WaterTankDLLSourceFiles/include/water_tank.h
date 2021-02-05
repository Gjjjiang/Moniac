//
// Academic License - for use in teaching, academic research, and meeting
// course requirements at degree granting institutions only.  Not for
// government, commercial, or other organizational use.
//
// File: water_tank.h
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
#ifndef RTW_HEADER_water_tank_h_
#define RTW_HEADER_water_tank_h_
#include <cstring>
#include "rtwtypes.h"
#include "rtw_continuous.h"
#include "rtw_solver.h"
#include "water_tank_types.h"

// Macros for accessing real-time model data structure
#ifndef rtmGetErrorStatus
#define rtmGetErrorStatus(rtm)         ((rtm)->errorStatus)
#endif

#ifndef rtmSetErrorStatus
#define rtmSetErrorStatus(rtm, val)    ((rtm)->errorStatus = (val))
#endif

#ifndef rtmGetStopRequested
#define rtmGetStopRequested(rtm)       ((rtm)->Timing.stopRequestedFlag)
#endif

#ifndef rtmSetStopRequested
#define rtmSetStopRequested(rtm, val)  ((rtm)->Timing.stopRequestedFlag = (val))
#endif

#ifndef rtmGetStopRequestedPtr
#define rtmGetStopRequestedPtr(rtm)    (&((rtm)->Timing.stopRequestedFlag))
#endif

#ifndef rtmGetT
#define rtmGetT(rtm)                   (rtmGetTPtr((rtm))[0])
#endif

#ifndef rtmGetTPtr
#define rtmGetTPtr(rtm)                ((rtm)->Timing.t)
#endif

#ifndef ODE3_INTG
#define ODE3_INTG

// ODE3 Integration Data
typedef struct {
  real_T *y;                           // output
  real_T *f[3];                        // derivatives
} ODE3_IntgData;

#endif

// Class declaration for model water_tank
class water_tankModelClass {
  // public data and function members
 public:
  // Block signals (default storage)
  typedef struct {
    real_T dVdt;                       // '<Root>/Water_Tank_Function'
  } B_water_tank_T;

  // Block states (default storage) for system '<Root>'
  typedef struct {
    int_T Integrator2_IWORK;           // '<Root>/Integrator2'
  } DW_water_tank_T;

  // Continuous states (default storage)
  typedef struct {
    real_T Integrator2_CSTATE;         // '<Root>/Integrator2'
  } X_water_tank_T;

  // State derivatives (default storage)
  typedef struct {
    real_T Integrator2_CSTATE;         // '<Root>/Integrator2'
  } XDot_water_tank_T;

  // State disabled
  typedef struct {
    boolean_T Integrator2_CSTATE;      // '<Root>/Integrator2'
  } XDis_water_tank_T;

  // External inputs (root inport signals with default storage)
  typedef struct {
    real_T flow_in;                    // '<Root>/flow_in'
    real_T flow_out;                   // '<Root>/flow_out'
    real_T StartVol;                   // '<Root>/StartVol'
  } ExtU_water_tank_T;

  // External outputs (root outports fed by signals with default storage)
  typedef struct {
    real_T CurrentVol;                 // '<Root>/CurrentVol'
  } ExtY_water_tank_T;

  // Real-time Model Data Structure
  struct RT_MODEL_water_tank_T {
    const char_T *errorStatus;
    RTWSolverInfo solverInfo;
    X_water_tank_T *contStates;
    int_T *periodicContStateIndices;
    real_T *periodicContStateRanges;
    real_T *derivs;
    boolean_T *contStateDisabled;
    boolean_T zCCacheNeedsReset;
    boolean_T derivCacheNeedsReset;
    boolean_T CTOutputIncnstWithState;
    real_T odeY[1];
    real_T odeF[3][1];
    ODE3_IntgData intgData;

    //
    //  Sizes:
    //  The following substructure contains sizes information
    //  for many of the model attributes such as inputs, outputs,
    //  dwork, sample times, etc.

    struct {
      int_T numContStates;
      int_T numPeriodicContStates;
      int_T numSampTimes;
    } Sizes;

    //
    //  Timing:
    //  The following substructure contains information regarding
    //  the timing information for the model.

    struct {
      uint32_T clockTick0;
      time_T stepSize0;
      uint32_T clockTick1;
      boolean_T firstInitCondFlag;
      SimTimeStep simTimeStep;
      boolean_T stopRequestedFlag;
      time_T *t;
      time_T tArray[2];
    } Timing;
  };

  // model initialize function
  void initialize();

  // model step function
  void step();

  // model terminate function
  void terminate();

  // Constructor
  water_tankModelClass();

  // Destructor
  ~water_tankModelClass();

  // Root-level structure-based inputs set method

  // Root inports set method
  void setExternalInputs(const ExtU_water_tank_T* pExtU_water_tank_T)
  {
    water_tank_U = *pExtU_water_tank_T;
  }

  // Root-level structure-based outputs get method

  // Root outports get method
  const water_tankModelClass::ExtY_water_tank_T & getExternalOutputs() const
  {
    return water_tank_Y;
  }

  // Real-Time Model get method
  water_tankModelClass::RT_MODEL_water_tank_T * getRTM();

  // private data and function members
 private:
  // Block signals
  B_water_tank_T water_tank_B;

  // Block states
  DW_water_tank_T water_tank_DW;
  X_water_tank_T water_tank_X;         // Block continuous states

  // External inputs
  ExtU_water_tank_T water_tank_U;

  // External outputs
  ExtY_water_tank_T water_tank_Y;

  // Real-Time Model
  RT_MODEL_water_tank_T water_tank_M;

  // Continuous states update member function
  void rt_ertODEUpdateContinuousStates(RTWSolverInfo *si );

  // Derivatives member function
  void water_tank_derivatives();
};

//-
//  The generated code includes comments that allow you to trace directly
//  back to the appropriate location in the model.  The basic format
//  is <system>/block_name, where system is the system number (uniquely
//  assigned by Simulink) and block_name is the name of the block.
//
//  Use the MATLAB hilite_system command to trace the generated code back
//  to the model.  For example,
//
//  hilite_system('<S3>')    - opens system 3
//  hilite_system('<S3>/Kp') - opens and selects block Kp which resides in S3
//
//  Here is the system hierarchy for this model
//
//  '<Root>' : 'water_tank'
//  '<S1>'   : 'water_tank/Water_Tank_Function'

#endif                                 // RTW_HEADER_water_tank_h_

//
// File trailer for generated code.
//
// [EOF]
//
