using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCocoaAsyncSocketMac.a", LinkTarget.Simulator, ForceLoad = true)]
