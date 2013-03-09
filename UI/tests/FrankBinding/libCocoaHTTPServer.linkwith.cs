using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCocoaHTTPServer.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true)]
