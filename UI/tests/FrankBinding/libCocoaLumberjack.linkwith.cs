using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCocoaLumberjack.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
