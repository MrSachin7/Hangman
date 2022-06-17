// See https://aka.ms/new-console-template for more information

using HangMan;

HangManGame hangManGame = new HangManGame(5);
await hangManGame.play();