// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using Flare.Battleship.UI.Console;
[assembly: ExcludeFromCodeCoverage]

var game = new Game();
var s = new CancellationTokenSource();
await game.GameLoop(s.Token);
