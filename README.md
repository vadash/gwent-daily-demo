# Introduction
Gwent-daily is a bot for gwent the witcher CCG card game. Bot helps complete daily quests and smooth new player experience. Created to farm reward points and some experience over night.

# Table of contents
1. [Requirements](#requirements)
2. [Features](#features)
3. [Settings](#settings)
4. [Decks](#decks)
5. [How it works](#how)

# Requirements <a name="requirements"></a>
## Main

* 64 bit OS win7sp1-win10

* 1920x1080 or bigger monitor (720p support may come later)

* Gwent with ENGLISH locale. Check [Settings](#settings). They are very important for smooth gameplay with bot!

## Nvidia
Bot is optimized for **Nvidia GPU**. Anything past 2013 should work (Kepler, Maxwell, Pascal, Volta, Turing microarchitecture). Install CUDA from link below to get decent performance boost

*  [CUDA10.0](https://developer.nvidia.com/compute/cuda/10.0/Prod/network_installers/cuda_10.0.130_win10_network "CUDA10")

10.0 exactly. 10.1 wont work

*  410.48 or newer graphical driver (for CUDA)

![alt text](https://media.discordapp.net/attachments/571798162059034628/571882157300121615/unknown.png "CUDA install settings")

# Features <a name="features"></a>
Current
* Decent winrate (for a bot and Tier 3 deck)
30-45% winrate over 500 games, R10 achived in may season
* Safe as possible
No injecting. Random exe and title name. Human like mouse movement. Random sleep timers and coordinates for clicking. GG after match. Close to zero missplays with right deck. About 10000 games botted on main, still alive 
* Easy to start
Deck costs 3k scraps and you can start since day one with ~500 scraps invested

Planned
* Casual / ranked switch
* Auto updating
* More decks (only monsters atm)
* Work in background

# Hotkeys

**F10** or **Tab** - stop bot

**F9** - start bot

# Download <a name="download"></a>
Beta is up. Check link above

# Decks <a name="decks"></a>

1) starter (0 legendaries, 0 epics, 4 extra cards) https://www.playgwent.com/en/decks/c73dd6c569495d0d755121abe82015ea

should be enough to win few rounds here and there 

2) craft order : 
* woodland spirit leader (0 scraps). Unlock it in reward book ASAP
* primordial dao (200)
* katakan (200)
* brewess + weavess (200+200)
* old speartip (800)
* weavess incantaion (800)

dont forget to remove bad bronzes when you add new card. keep 25 cards in deck for maximum value

3) current best https://www.playgwent.com/en/decks/745a0ba2362914615e2bcc6171fc932c

**for your own SAFETY please replace few cards and dont use it as is**

# Gwent settings <a name="settings"></a>

options->general->camera move on turn end : OFF (helps bot to detect cards better)

options->general->auto turn end : ON (speed up process)

options->general->battlefield board selection : MINE (optional)

options->graphic->premium : DISABLED (or use non premium cards in deck)

options->resolution->fullscreen : DISABLED (dont need if you run game in 1920x1080 resolution)

FLUX or windows 10 night light should be disabled as well

## scaling (can skip with 100% windows scaling)
1) Find gwent game exe (not gog)

![alt text](https://lh3.googleusercontent.com/-Riow_0Aq0t8/WYNSnp25eTI/AAAAAAAAR3o/n2S9JfBVz1gW3nGxFVOBsaugfoMsUp_gACHMYCw/s0/explorer_2017-08-03_19-43-08.png "scaling1")

2) Change scaling mode 

![alt text](https://lh3.googleusercontent.com/-Bzd5Y2jgwIg/WYNSy0QV1II/AAAAAAAAR3s/57RYhR55x8YaGcx6a_9uKq7kVut7UDAmACHMYCw/s0/explorer_2017-08-03_19-43-53.png "scaling2")

3) Change windows scaling

![alt text](https://lh3.googleusercontent.com/-Fk6Ip4vRqw8/WYNS8FxeqmI/AAAAAAAAR3w/0B8tKmYcF78jFDzcGCX3kiGSG3iLQ-XNwCHMYCw/s0/ApplicationFrameHost_2017-08-03_19-44-30.png "scaling3")

4) Reboot PC

# How it works <a name="how"></a>
Neural net [darknet](https://github.com/AlexeyAB/darknet "darknet") to detect cards

OCR, Pixel color checking and image comparing for detecting game state (card count, scores, leader state, end turn, round, etc)

[CoreRT](https://github.com/dotnet/corert "CoreRT") compiller for .NET code