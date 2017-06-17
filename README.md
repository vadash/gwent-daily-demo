# Table of contents
1. [Introduction](#introduction)
2. [Download](#download)
3. [Features](#features)
4. [Settings](#settings)
5. [FAQ](#faq)
6. [Winrate](#winrate)
7. [Decks](#dicks)
8. [Vmware gudie](#vmware)
9. [How to register full version](#register)

# Introduction <a name="introduction"></a>
*Gwent-daily* is a bot for gwent the witcher CCG card game. Bot is based on robust image detection / OCR. It helps complete daily quests and smooth new player experience. Bot farms almost 4 packs and some scraps over night.

# Download <a name="download"></a>
Get last setup.exe 

https://github.com/vadash/gwent-daily-demo/releases/latest

Demo limit *25 runs x 10 days x up to 5 games each session*

Bot is updated for OBT **0.9.7** patch, many happy users

![alt text](https://lh3.googleusercontent.com/-U5TxtkQsGAo/WT0D4gYqT7I/AAAAAAAARAw/WR11q7-R32sJJalo1HDFStWD3RhADhx5QCHM/s0/Discord_2017-06-11_11-47-08.png "discord")

# Features <a name="features"></a>
* No injecting. Its safer to use and harder to detect
* Bot plays minions with position in mind. For example gold cards to the right, regular ones to the left
* Emotes support
* Farms 370 ore and 145 scraps over 6 hours
* Up to 25-50% win rate. It depends on your casual MMR, account level and deck used
* Pass on round win without wasting extra cards
* Human like mouse movement
* Will GG after match
* Close defeat / victory / dc / forfeit screens
* Restart gwent every few hours (memory leaks)
* Random sleep timers and coordinates for clicking
* Smart mulligan
* Random bot exe/window name

# Gwent settings <a name="settings"></a>
## Use for all resolution

![alt text](https://lh3.googleusercontent.com/-j3mRUs9mH7A/WSgeINPQgBI/AAAAAAAAQKI/7mGVKLE8SsU176zYujmZtMBuR6vZ0g3zwCHM/s0/Gwent_2017-05-26_15-22-51.png "gwent settings")

## 1440p or 1680x1050 (skip for 1080p)
Set gwent like this and start bot

![alt text](https://lh3.googleusercontent.com/-PMuQcCzcyBI/WPsgFKsmE_I/AAAAAAAAPa4/NGGzHUB_OB4/s0/Gwent_2017-04-22_12-19-00.png "!1080")

# FAQ <a name="faq"></a>
Q: Any dependency ?

A: vcredist 2015 x64 and net 4.5.2 (skip for win 10)

Q: Game and windows settings ?

A: ENGLISH language, 100% scaling in windows, 1920x1080 or more (1680x1050 limited support), Monitor#1, windows 7sp1-10 x64, no cyrillic/special symbols/spaces in windows username
![alt text](https://lh3.googleusercontent.com/-4_dBitPoZac/WQsKqZOmw2I/AAAAAAAAPkw/IOaJhpuZWiIETTlinj2ZrmPdVomIixIPQCHM/s0/ApplicationFrameHost_2017-05-04_14-04-06.png "Scaling")

Q: How fast bot can complete 3 daily tiers ?

A: 6.5 hours. Assuming 34 games over 7 hours, 40% winrate. So for 3 tiers it will be 42 / (0.4 * 2 + 0.6 * 1) = 30 games or 30/34 * 7 = 6.2 hours

![alt text](https://lh3.googleusercontent.com/-AnNGBw1EgWw/WTEVcei-yPI/AAAAAAAAQis/wTjXMTfXNc81LMRnkQ2TBYjK4rVLanx8QCHM/s0/chrome_2017-06-02_10-36-14.png "Daily rewards")

Q: WTF is HWID?

A: Its HardWare IDentification key. Its generated based on CPU serial number and Motherboard serial. Looks like this
![alt text](https://lh3.googleusercontent.com/-GCnSAawXp0w/WN-QGuFfdWI/AAAAAAAAPCw/RwX7whsUIu8/s0/Bg110fBHacjE1c_2017-04-01_14-33-43.png "HWID")

Q: How does the bot determine which deck to use?

A: Last played deck (top left). You have to help bot and click it one time.

Q: Do I start bot at the main menu?

A: Yes. Ingame works too.

Q: Can I run bot in background?

A: Nope, its both for safety and impossible since we dont inject in game. People bot over night or with vmware (check guide below).

Q: Do I need additional key for virtual machine ?

A: Yep. Also make sure to take snapshot after you install windows.

Q: Can I use gwent tracker with bot ?

A: Yes but make sure it doesnt cover important parts of screen (aka "Deck selection", "Play card" in right corner, scores, "Your turn", etc)

Q: What virtual machine should I use ?

A: Vmware 12+ pro + vmware tools

# Winrate examples (OBT) <a name="winrate"></a>

![alt text](https://i.imgur.com/RzluDMh.png "best deck")

# Decks (better decks in discord) <a name="dicks"></a>
ST starter OBT (up to 500 scraps)
![alt text](https://lh3.googleusercontent.com/-ZHAjwRchU7Q/WSZZdepoXnI/AAAAAAAAQFY/GXYyccFTF2E6oS-nCVzpiDRxBlmEitlDwCHM/s0/Gwent_2017-05-25_07-11-29.png "ST 0")

# Vmware guide <a name="vmware"></a>
## Important
Changing number of cores in vmware will change HWID too!

## Links
vmware http://www.vmware.com/go/tryworkstation-win

vmware serial https://www.google.com/webhp?q=keys+for+vmware+12.5

Windows 7 X64 EN image (original) 

https://rutracker.org/forum/viewtopic.php?t=5121311 or

magnet:?xt=urn:btih:AF0CF9EA0673B98BF10DB89637C1A8389C968878&tr=http%3A%2F%2Fbt.t-ru.org%2Fann%3Fmagnet&dn=Microsoft%20Windows%207%20with%20SP1%20Updated%2012.05.2011%20English%20%5B%D0%92%D1%81%D0%B5%20%D1%80%D0%B5%D0%B4%D0%B0%D0%BA%D1%86%D0%B8%D0%B8%5D

Select "en_windows_7_enterprise_with_sp1_x64_dvd_u_677651.iso"

## Vmware settings
SSD settings: 1600 (win 7 lite)-1800 (win 7) - 2000 (win 10 lite) - 2500 (win 10) RAM / 1000-2400 pagefile (total ram + pagefile should be ~4 GB) / 512 (win 7) - 1024 (win 10) MB VRAM / 1-2 cpu cores / 15-20 gb HDD

For HDD increase RAM to 2500-3000 and VRAM to 1000-1500.

![alt text](https://lh3.googleusercontent.com/-Vxtj8GqpQfM/WTUiID5LHeI/AAAAAAAAQqE/nR8DJvOqbAgABeziy3t69p1lJWf-ciz7QCHM/s0/vmware_2017-06-05_12-19-25.png "SSD botting")

## Guest OS power options (inside vmware)

![alt text](https://lh3.googleusercontent.com/-2OaB7Pa4DH8/WTUg103kXPI/AAAAAAAAQp8/3KN-4eUc5ukW9HyoexRuuQDyCiGBZB6fACHM/s0/explorer_2017-06-05_12-13-56.png "Power options")

## Dxtory (optional)
This app limits fps to reduce vmware load by 20-30%. I use 20 fps limit. Here is version with serial

magnet:?xt=urn:btih:8EF2E1828CA03C1567DF7A31A3AD3F1AA28986B7&tr=http%3A%2F%2Fbt4.t-ru.org%2Fann%3Fmagnet&dn=Dxtory%202.0.122%20x86%2Bx64%20%5B2013%2C%20ENG%5D

![alt text](https://lh3.googleusercontent.com/-r-YzgiCQlzY/WQOEhbsUvUI/AAAAAAAAPeQ/_qSZ9TQQpP8EpA41rfB1nPvhR3k_Di3hgCHM/s0/vmware_2017-04-28_21-05-53.png "dxtory")

## Detailed guide in pictures
https://goo.gl/photos/vC2aA8zfiBoqUcPw7

# Full version
DM me @ discord:
dlr5668#5210

# How to register bot after you buy it <a name="register"></a>

1. Right click "GwentBot" desktop shortcut and press "Open file location".

![alt text](https://lh3.googleusercontent.com/-jopxphPl6Ao/WRnI_uXu0tI/AAAAAAAAPyw/ATvgPUBFPxQ5uDKYXmUzmxOrnsZnhSR_gCHM/s0/2017-05-15_18-27-55.png "desktop shortcut")

2. Rename license file to **lic** (no file name extension) and paste it here.

Bot **wont find** lic file if it doesnt have this exact name ("9DF9-F123-10E1-C82F-36AB.lic" - wrong name, "lic.lic" - wrong, "lic" - right).

![alt text](https://lh3.googleusercontent.com/-ECHfLApeYBg/WTvV-WoCK8I/AAAAAAAAQ-Q/rtxMKHLGgvMU2ESLfpsPGcRs0xyaE09dQCHM/s0/explorer_2017-06-10_14-20-22.png "where to place file")

3. Restart bot. 

PS Avast auto sandbox mode will change your HWID too. Disable it (sandbox) for bot.