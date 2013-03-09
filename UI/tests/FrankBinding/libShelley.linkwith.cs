using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libShelley.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
