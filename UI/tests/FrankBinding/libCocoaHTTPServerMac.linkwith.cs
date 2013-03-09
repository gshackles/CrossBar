using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCocoaHTTPServerMac.a", LinkTarget.Simulator, ForceLoad = true)]
