// See https://aka.ms/new-console-template for more information

using Flare.Battleship;
using Flare.Battleship.UI.Console;

var game = new Game();
var s = new CancellationTokenSource();
await game.GameLoop(s.Token);
