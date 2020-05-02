rm main.exe
mcs Main.cs Agent.cs MMAS.cs -out:main.exe
mono main.exe
