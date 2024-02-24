# Hlavní informace
- Autor: Michal Štilec
- Třída: C4b
- Kontakt: stilec2@spsejecna.cz
- Datum vypracování: 9.2.2024 - 25.2.2024
- Název školy: Střední průmyslová elektrotechnická škola (SPŠE) Ječná 
- Hardwareové požadavky: Windows 10/11, internetové připojení
- Architektonický návrhový styl: Single Responsibility Principle (SRP)
- Jazyk: C#
  
# Jak spustit program
1. Spusťte PuTTY

2. Do PuTTY zadejte následující informace
	* Host Name: jouda@dev.spsejecna.net
	* Port: 20448
	* Connection type: SSH

3. Zmáčkněte Open a napište heslo jooouda

4. Teď už jen zbývá použít postupně tyto tři příkazy a program by se měl spustit
```
export PATH="$PATH:$HOME/.dotnet"
```
```
cd Alpha4/bin/Debug/net6.0/
```
```
dotnet Alpha4.dll
```

# Použité knihovny
* using System.Collections.Generic;
* using System.Linq;
* using System.Net.Sockets;
* using System.Net;
* using System.Text;
* using System.Threading.Tasks;
* using System.Text.Json;

# Struktura programu

## Třída UDPdiscovery
- Třída zajišťuje spojení UDPclienta pro odesílání zpráv ostatním
- clientům a také přijímá zprávy s odpovědí "ok"
## Metody: 
### Start(): 
- Spustí poslouchání UDP v samostatném vlákně

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/d95213b3-0f51-4e16-ade8-637eb1ecba20)

### UdpListener(): 
- Vyhledává příchozí zprávy UDP a zpracovává je

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/fec73236-abbe-46f3-9124-50d5cf7a5eee)

### ReceiveMessage(): 
- Zpracuje přijatou zprávu UDP a vygeneruje odpověď

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/58e1cbd2-a052-4b4d-9f8c-81ea811c3dea)

### UdpDiscovery(): 
- Odešle zprávu každých 5 sekund

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/39ac02bf-9f1a-4771-9029-21737d2f858d)


### SendMessage(): 
- Odešle zprávu všem peerům v síti

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/d456a3c2-f4c0-4f7c-a7bd-0da0261d46bc)



## Třída Config
- Třída pro načítání konfigurace z konfiguračního souboru.

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/57025550-d130-4f0e-8940-5ceec0a8acfb)

## Metody:
### LoadPeer(): 
- Načte peer-id
### LoadPort():
- Načte port

# Třída Logs
- Třída pro zaznamenání úspěšného startu programu a hlavně zapisuje chyby z programu

![image](https://github.com/MichalStilec/Alpha4/assets/113086016/806f00f9-12aa-40c9-8ffd-a25a69f68e4c)

## Metody:
### LogSuccess(): 
- Uloží do json souboru informaci po něčem úspěšném
### LogError():
- Uloží do json souboru informaci o výskytu erroru

