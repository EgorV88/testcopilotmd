# testcopilotmd

Projects structure:

Entry points: 
monitor: Monitoring Service - responsible for keeping all processes alive.
gui: GUI - WinForms Application, showing human-friendly logs and application 
configuration. 
network: Network Listener Process - (windows process, DLL/EXE) listens for TCP, saves the 
.DCM file 
deidentifier: Deidentifier Process - (windows process, DLL/EXE) replaces all PHI in .DCM with 
stubs 
extractor: Extracter Process - (windows process, DLL/EXE) extracts .PNG files from .DCM

Libraries:
CoPilodMD.Core - shared logic for all projects.
CoPilotMD.NetworkListener - logic of network listener service
CoPilotMD.Deidentifier - logic of deidentifier service
CoPilotMD.Monitor - logic of Monitoring Service

Tests:
CoPilotMD.Tests - tests of main logic parts
