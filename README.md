# Table of contents
1. [Download](#download)
2. [Features](#features)
3. [Supported card list](#cards)
4. [Settings](#settings)
5. [FAQ](#faq)
6. [Winrate](#winrate)
7. [Decks](#dicks)
8. [Vmware gudie](#vmware)
9. [How to register full version](#register)

# Introduction <a name="introduction"></a>
*Gwent-daily* is a bot for gwent the witcher CCG card game. Bot is based on robust image detection / OCR. It helps complete daily quests and smooth new player experience. Bot farms almost 4 packs and some scraps over night.

[![Gameplay video](https://lh3.googleusercontent.com/-coABn925fOA/WcbzGiVR2PI/AAAAAAAAS6k/GAJDBCWCqrQC4xTCzbYsNnLEMhE4oEAmwCHMYCw/s0/chrome_2017-09-24_02-49-41.png)](https://youtu.be/fDUe4CJEEcM "Gameplay video")

# Features <a name="features"></a>

![alt text](https://lh3.googleusercontent.com/-n-sT--SHseo/WbllV1__8nI/AAAAAAAASys/aXC-vKwVlYQWsWebgWdHXnZlo4qlcW0pACHMYCw/s0/chrome_2017-09-13_20-05-17.png "proladder 9/13/17")

* Can bot casual / ranked (got me to R19 10/23/2017) / pro ladder ST (got 1116 peak mmr and all 100+ st games were played with bot)
* No injecting. Its safer to use and harder to detect
* Emotes support
* Farms 370 ore and 145 scraps over 6 hours
* Up to 35-55% win rate. It depends on your casual MMR, account level and deck used. Bot may need few days to tank your casual MMR
* Pass on round win without wasting extra cards
* Human like mouse movement
* Will GG after match
* Close defeat / victory / dc / forfeit screens
* Can restart gwent every few hours (avoid memory leaks)
* Random sleep timers and coordinates for clicking
* Smart mulligan (with blacklisting)
* Random bot exe/window name

# Hotkeys

Ctrl + **F10** - stop bot after current game

Ctrl + **F11** - start/pause bot

Ctrl + **F12** - exit bot

# Download <a name="download"></a>

```diff
- Demo limit 25 runs x 10 days x up to 1 hour each session (BOT WILL CRASH AFTER)
```
Get last setup.exe https://github.com/vadash/gwent-daily-demo/releases/latest

VirusTotal is around **6/64**. I am using custom protector that makes it difficult to avoid AV miss detection. You can bot in virtual machine if you want to be perfect safe.

https://www.virustotal.com/#/file/4dd090c17cdffd7197abdd7c4e2737034a9fcea5019ee63c8a443fc1fe705346/detection

```diff
- Demo limit 25 runs x 10 days x up to 1 hour each session (BOT WILL CRASH AFTER)
```
Bot is updated for OBT **0.9.10.x** patch, many happy users.

![alt text](https://lh3.googleusercontent.com/-U5TxtkQsGAo/WT0D4gYqT7I/AAAAAAAARAw/WR11q7-R32sJJalo1HDFStWD3RhADhx5QCHM/s0/Discord_2017-06-11_11-47-08.png "discord")

# Supported cards <a name="cards"></a>

Dont forget to enable new premium option (Premium = OFF) in game if u want to use animated cards with bot!

**Bronze**: Mahakam Defender, Mahakam Guard, First Light, Dwarven Skirmisher, Vrihedd Dragoon, Dol Blathanna Archer, Thunderbolt potion, Wardancer, Hawker Healer, Elven Mercenary (limited, with first light), Mahakam Marauder, Dwarven Agitator

**Silver**: Barclay Els, Yarpen Zigrin, Dennis Cranmer, King of Beggars, Morenn, Olgierd, Roach, Toruviel OR Sheldon, Alzur cross, Commanders Horn (only with extreme melee stacking)

**Gold**: Geralt (starter gold), Royal Decree (starter gold, use only with 4 starter golds), Dragon saskia (starter gold), Triss (starter gold), Ragnaroog, Yennefer: The Conjurer, Avallach, Triss Butt, Milva, both Zoltans

**Leader**: Brouver Hoog only

Bot can play unsuported cards too. Keep it minimum and simple like Tremors. Bot will play them last after all known/detected cards.

## Tips for new players

Finish single player challenges and subscribe for gog newsletter for 7.5+1 keks. You may buy starter pack (good value equals ~5 nights botting). Open kegs and choose supported cards. Choose 1 least interested faction and mill all their cards (except leaders) if you want to speed up progression. Queue ranked ladder as soon as you hit lvl 10.

Got rank 17 on new account after 4 days
![alt text](https://lh3.googleusercontent.com/-kJpGLTM3kOE/WeiNakI7pSI/AAAAAAAATkU/aJ2tfpGjrek-Q0XR6DzYOZe2jGFhPyHyQCHMYCw/s0/Gwent_2017-10-19_14-32-09.png "10/19/2017 new acc")

## Craft order

3 Mahakam Defender + 3 Mahakam Marauder + 3 Dwarven Agitator (its important core) -> other bronzes to replace missing golds and silvers (craft 2 copies of 3 and get last one from kegs) -> Dennis (core) -> Yarpen (cant go wrong with resilence) -> Barclay (yee more dorfs) -> Operator (give enemy useless card) -> Finish silvers -> Start crafting golds (Zoltan chivay -> Milva -> Zoltan Tamer)

# Gwent settings <a name="settings"></a>
## general (can skip this)

![alt text](https://lh3.googleusercontent.com/-j3mRUs9mH7A/WSgeINPQgBI/AAAAAAAAQKI/7mGVKLE8SsU176zYujmZtMBuR6vZ0g3zwCHM/s0/Gwent_2017-05-26_15-22-51.png "gwent settings")

## 4k resolution (can skip for 720p)
Set gwent like this and start bot. It will remove gwent's window title, change size to 720p and move it to top left

![alt text](https://lh3.googleusercontent.com/-PMuQcCzcyBI/WPsgFKsmE_I/AAAAAAAAPa4/NGGzHUB_OB4/s0/Gwent_2017-04-22_12-19-00.png "!720p")

## scaling (can skip with 100% windows scaling)
1) Find gwent game exe (not gog)

![alt text](https://lh3.googleusercontent.com/-Riow_0Aq0t8/WYNSnp25eTI/AAAAAAAAR3o/n2S9JfBVz1gW3nGxFVOBsaugfoMsUp_gACHMYCw/s0/explorer_2017-08-03_19-43-08.png "scaling1")

2) Change scaling mode 

![alt text](https://lh3.googleusercontent.com/-Bzd5Y2jgwIg/WYNSy0QV1II/AAAAAAAAR3s/57RYhR55x8YaGcx6a_9uKq7kVut7UDAmACHMYCw/s0/explorer_2017-08-03_19-43-53.png "scaling2")

3) Change windows scaling

![alt text](https://lh3.googleusercontent.com/-Fk6Ip4vRqw8/WYNS8FxeqmI/AAAAAAAAR3w/0B8tKmYcF78jFDzcGCX3kiGSG3iLQ-XNwCHMYCw/s0/ApplicationFrameHost_2017-08-03_19-44-30.png "scaling3")

4) Reboot PC

# FAQ <a name="faq"></a>

Q: How I can track bot winrate ?

A: Install gwent tracker and hide all GUI

![alt text](https://lh3.googleusercontent.com/-VZibfbPuvek/WcYaRMM1F_I/AAAAAAAAS5k/jAIfaEtDPKUI-vGTLF0kbvti8tt8SVPVgCHMYCw/s0/Gwent_2017-09-23_11-24-11.png "Gwent tracker")

Q: Game and windows settings ?

A: gwent's ENGLISH language, 1280x720 or more, windows 7sp1-10, no cyrillic/special symbols/spaces in windows username, vcredist 2008 + 2012 + 2013 + 2015 x86, and .NET 4.6.2

![alt text](https://lh3.googleusercontent.com/-4_dBitPoZac/WQsKqZOmw2I/AAAAAAAAPkw/IOaJhpuZWiIETTlinj2ZrmPdVomIixIPQCHM/s0/ApplicationFrameHost_2017-05-04_14-04-06.png "Scaling")

Q: How fast bot can complete X daily tiers ?

A: Assuming 44 games over 8.5 hours, 45% winrate => 11.6 minutes average game, 0.85 rounds for lose (sometimes enemy will 2-0 you)

2 tiers = 18 / (0.45 * 2 + 0.55 * 0.85) * 11.6 = **155** minutes

3 tiers = 42 / (0.45 * 2 + 0.55 * 0.85) * 11.6 = **360** minutes

4 tiers (max) = 66 / (0.45 * 2 + 0.55 * 0.85) * 11.6 = **560** minutes

Q: WTF is HWID?

A: Its HardWare IDentification key. Its generated based on CPU serial number and Motherboard serial. Looks like this
![alt text](https://lh3.googleusercontent.com/-GCnSAawXp0w/WN-QGuFfdWI/AAAAAAAAPCw/RwX7whsUIu8/s0/Bg110fBHacjE1c_2017-04-01_14-33-43.png "HWID")

Q: Anyone banned yet ?

A: No bans reported so far

# Winrate examples <a name="winrate"></a>

![alt text](https://lh3.googleusercontent.com/-7Xl0ZphGMHo/WbPTsu_XjaI/AAAAAAAASrM/N1bYSx6grFk89J4-ijoUxYY_Wo0IuZoWgCHMYCw/s0/Gwent_2017-09-09_14-42-37.png "9/9/2017")

# Decks (better decks in discord) <a name="dicks"></a>

![alt text](https://cdn.discordapp.com/attachments/321176575057985536/352901282656288779/unknown.png "ST 0")

Incase you have a lot of animated cards run this (it will be slighlty worse)

![alt text](https://lh3.googleusercontent.com/-zrVirGr7jKU/Wa1eH0pUEVI/AAAAAAAASig/n6v4xZ4MUcc8q30f2quIIXnb17R00m0AQCHMYCw/s0/Gwent_2017-09-04_17-07-21.png "ST 1")

Make sure Brouver and Dennis are non animated and cut extra bronzes. No need to run 6/6 silvers and 4/4 golds. Dont use buffs like horn and potions with animated cards!

# Vmware guide <a name="vmware"></a> (optional, allows you to use ur computer and bot at the same time) 9/27/2017
## Important
Changing number of cores in vmware will change HWID too! Usually 1 (i5 class cpu) - 2 cores is enough

## Links
vmware player 12 free https://www.vmware.com/go/tryplayerpro-win-64

Windows 7 x86 EN image or use any win 7/8.1/10 x86/x64 original torrent + magnet

https://rutracker.org/forum/viewtopic.php?t=5121311

magnet:?xt=urn:btih:AF0CF9EA0673B98BF10DB89637C1A8389C968878&tr=http%3A%2F%2Fbt.t-ru.org%2Fann%3Fmagnet&dn=Microsoft%20Windows%207%20with%20SP1%20Updated%2012.05.2011%20English%20%5B%D0%92%D1%81%D0%B5%20%D1%80%D0%B5%D0%B4%D0%B0%D0%BA%D1%86%D0%B8%D0%B8%5D

## Vmware settings
SSD settings: 1400 (win 7 x86 lite) - 1600 (win 7 x64 lite) - 1800 (win 7) - 2000 (win 10 lite) - 2500 (win 10) RAM / 1000-2400 pagefile (total ram + pagefile should be ~4 GB) / 512 (win 7) - 1024 (win 10) MB VRAM / 1-2 cpu cores / 20 gb HDD

For HDD increase RAM +400 MB and VRAM to 1000-1500 MB

![alt text](https://lh3.googleusercontent.com/-sCKc4QZgSUA/WcrUmK43YPI/AAAAAAAAS-M/S-LbxPZGgXojXqOnj7AoUwiksK-YP4CnACHMYCw/s0/vmplayer_2017-09-27_01-28-37.png "SSD botting")

## Guest OS power options (inside vmware)

![alt text](https://lh3.googleusercontent.com/-2OaB7Pa4DH8/WTUg103kXPI/AAAAAAAAQp8/3KN-4eUc5ukW9HyoexRuuQDyCiGBZB6fACHM/s0/explorer_2017-06-05_12-13-56.png "Power options")

## Detailed guide

[![Detailed guide](https://lh3.googleusercontent.com/-RURR4ixnAQs/Wcub2YewNiI/AAAAAAAATAY/HS94dRyUwMwMDzijbQ1STDk4FPid-waywCHMYCw/s0/chrome_2017-09-27_15-38-46.png)](https://www.youtube.com/watch?v=bwL7awAwYOo&index=2&list=PLRILnyXG1idBRO6iK2ZKvQwonMHlTXjxq
 "Detailed guide")

# Full version
DM me @ discord:
dlr5668#5210
https://discord.gg/P2XdSrR

# How to register bot after you buy it <a name="register"></a>

1. Right click "GwentBot" desktop shortcut and press "Open file location".

![alt text](https://lh3.googleusercontent.com/-jopxphPl6Ao/WRnI_uXu0tI/AAAAAAAAPyw/ATvgPUBFPxQ5uDKYXmUzmxOrnsZnhSR_gCHM/s0/2017-05-15_18-27-55.png "desktop shortcut")

2. Rename license file to **lic** (no file name extension) and paste it here.

Bot **wont find** lic file if it doesnt have this exact name ("9DF9-F123-10E1-C82F-36AB.lic" - wrong name, "lic.lic" - wrong, "lic" - right).

![alt text](https://lh3.googleusercontent.com/-ECHfLApeYBg/WTvV-WoCK8I/AAAAAAAAQ-Q/rtxMKHLGgvMU2ESLfpsPGcRs0xyaE09dQCHM/s0/explorer_2017-06-10_14-20-22.png "where to place file")

3. Restart bot. 

PS Avast auto sandbox mode will change your HWID too. Disable it (sandbox) for bot.