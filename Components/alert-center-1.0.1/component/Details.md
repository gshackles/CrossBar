`AlertCenter` displays notifications in a manner similar to native iOS
banner notifications.

```csharp
using Xamarin.Controls;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();
	AlertCenter.Default.PostMessage ("Knock knock!", "Who's there?");

	AlertCenter.Default.PostMessage ("Interrupting cow.", "Interrupting cow who?",
		UIImage.FromFile ("cow.png"), delegate {
		Console.WriteLine ("Moo!");
	});
}
```
