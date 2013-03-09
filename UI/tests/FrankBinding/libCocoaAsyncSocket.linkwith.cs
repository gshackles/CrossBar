using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCocoaAsyncSocket.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
