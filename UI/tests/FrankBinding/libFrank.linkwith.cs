using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libFrank.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
