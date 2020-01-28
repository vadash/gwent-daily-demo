# Introduction
Gwent-daily is a bot for gwent the witcher CCG card game. Bot helps complete daily quests and smooth new player experience. Created to farm reward points and some experience over night.

# Table of contents
1. [Requirements](#requirements)
2. [Download](#download)
3. [Features](#features)
4. [Settings](#settings)
5. [Decks](#decks)

# Requirements <a name="requirements"></a>

* 64 bit OS win10

* 1920x1080 or bigger monitor (720p support may come later)

* Gwent with ENGLISH locale. Check [Settings](#settings). They are very important for smooth gameplay with bot!

* CPU with AVX instruction (even 6 years old ones have it)

# Troubleshooting

* Crash fix (failed to initialize OCR) https://cdn.discordapp.com/attachments/646370777347784728/648124330839900160/Untitled_error.jpg -> You are probably running (N) version of Windows. Settings -> Apps -> Optional features -> Add a feature -> Media Feature Pack. Wait for install and reboot

* Bot cant resize game window and stuck like this https://cdn.discordapp.com/attachments/652653041941610497/652740625933926413/Inkedgwent-2019-12-07-12-15-40_LI.jpg
-> Hide task bar in windows settings

* Insta crash -> Check that your CPU has AVX instruction (basically newer than amd 7 years old fx 6300 and some xeon)

* Not reacting to F9 key. Settings > Ease of Access > Keyboard and use virtual keyboard

# Features <a name="features"></a>
## Current
* Ok winrate (for a bot and Tier 3 deck)

30-40% winrate over 500 games, R15 achived

* Safe as possible

No injecting. Random exe and title name. Human like mouse movement. Random sleep timers and coordinates for clicking. GG after match. Close to zero missplays with right deck. About 10000 games botted on main, still alive 

* Easy to start

Deck costs 3k scraps and you can start since day one with ~500 scraps invested

## Planned
* Auto updating
* More decks (only monsters atm)
* Work in background

# Hotkeys

**F10** or **Tab** - stop bot

**F9** - start bot

## Download <a name="download"></a>
Download link https://cloud.mail.ru/public/nUrc/24ZAL8Cfh

Can use on vmware (virtual machine) for extra safety

Check guide in discord https://discord.gg/axjFQps

# Decks <a name="decks"></a>

## Why monsters
Allows zero interaction with enemy board. Cheap to start and doesnt contain complex mechanics so less miss plays

## Starter
~700 scraps
https://www.playgwent.com/en/decks/44050fe31406ebb920e24c4c96df6b5b

Should be enough to win few rounds here and there 

Craft order : 
* woodland spirit leader (0 scraps). Unlock it in reward book ASAP
* primordial dao (200)
* katakan (200)
* brewess + weavess (200+200)
* old speartip (800)
* weavess incantaion (800)

Dont forget to remove bad bronzes when you add new card. Keep 25 cards in deck for maximum value. Supported cards list

15_old_spaertip, 11_primordial_dao, 11_weavess_incantation, 10_golyat, 10_old_spaertip_asleep, 10_protofleder, 9_katakan, 9_ozzrel, 8_ice_giant, 8_whispess, 8_brewess, 8_weavess, 8_count_caldwell, 8_imlerith_wrath, 7_griffin, 6_celaeno_harpy, 6_cyclops, 6_ghoul, 6_parasite, 6_swallow, 6_wild_hunt_rider, 5_alpha_werewolf, 5_wyvern, 4_archespore, 4_cutthroat, 4_foglet, 4_nekker, 4_nekker_warrior, 4_plumard, 4_werewolf, 0_tactical_advantage

## Best
~3000 scraps
https://www.playgwent.com/en/decks/b03673ca178431219ddce649873e6c72

**for your own SAFETY please replace few cards and dont use it as is**

## Future plans
DOESNT WORK NOW

Implement dwarf deck https://www.playgwent.com/en/decks/a479f43450343989acdb3d062e550bb9

DOESNT WORK NOW

# Gwent settings <a name="settings"></a>

options->general->camera move on turn end : OFF (helps bot to detect cards better)

options->general->auto turn end : ON (speed up process)

options->general->battlefield board selection : MINE (optional)

options->graphic->premium : DISABLED (or use non premium cards in deck)

options->resolution->fullscreen : DISABLED (dont need if you run game in 1920x1080 resolution)

FLUX or windows 10 night light should be disabled as well

## Scaling (can skip with 100% windows scaling)
1) Find gwent game exe (not gog)

![alt text](https://lh3.googleusercontent.com/-Riow_0Aq0t8/WYNSnp25eTI/AAAAAAAAR3o/n2S9JfBVz1gW3nGxFVOBsaugfoMsUp_gACHMYCw/s0/explorer_2017-08-03_19-43-08.png "scaling1")

2) Change scaling mode 

![alt text](https://lh3.googleusercontent.com/-Bzd5Y2jgwIg/WYNSy0QV1II/AAAAAAAAR3s/57RYhR55x8YaGcx6a_9uKq7kVut7UDAmACHMYCw/s0/explorer_2017-08-03_19-43-53.png "scaling2")

3) Change windows scaling

![alt text](https://lh3.googleusercontent.com/-Fk6Ip4vRqw8/WYNS8FxeqmI/AAAAAAAAR3w/0B8tKmYcF78jFDzcGCX3kiGSG3iLQ-XNwCHMYCw/s0/ApplicationFrameHost_2017-08-03_19-44-30.png "scaling3")

4) Reboot PC
