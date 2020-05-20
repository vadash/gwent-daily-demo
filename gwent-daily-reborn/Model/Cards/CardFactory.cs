using System;
using System.Collections.Generic;
using System.Drawing;
using Alturos.Yolo.Model;
using gwent_daily_reborn.Model.Cards.Extra;
using gwent_daily_reborn.Model.Cards.Monster;
using gwent_daily_reborn.Model.Cards.Neutral;

namespace gwent_daily_reborn.Model.Cards
{
    internal class CardFactory : ICardFactory
    {
        public List<Card> Create(IEnumerable<YoloItem> items, Point basePoint)
        {
            if (items == null) return null;
            var cards = new List<Card>();
            foreach (var item in items)
            {
                Type classname = null;
                switch (item.Type)
                {
                    case "15_old_speartip":
                    case "15_old_spaertip":
                        classname = typeof(OldSpeartip);
                        break;
                    case "11_primordial_dao":
                        classname = typeof(PrimordialDao);
                        break;
                    case "11_weavess_incantation":
                        classname = typeof(WeavessIncantation);
                        break;
                    case "10_golyat":
                        classname = typeof(Golyat);
                        break;
                    case "10_old_speartip_asleep":
                    case "10_old_spaertip_asleep":
                        classname = typeof(OldSpeartipAsleep);
                        break;
                    case "10_protofleder":
                        classname = typeof(Protofleder);
                        break;
                    case "9_katakan":
                        classname = typeof(Katakan);
                        break;
                    case "9_ozzrel":
                        classname = typeof(Ozzrel);
                        break;
                    case "8_ice_giant":
                        classname = typeof(IceGiant);
                        break;
                    case "8_whispess":
                        classname = typeof(Whispess);
                        break;
                    case "8_brewess":
                        classname = typeof(Brewess);
                        break;
                    case "8_weavess":
                        classname = typeof(Weavess);
                        break;
                    case "8_count_caldwell":
                        classname = typeof(CountCaldwell);
                        break;
                    case "8_imlerith_wrath":
                        classname = typeof(ImlerithWrath);
                        break;
                    case "7_griffin":
                        classname = typeof(Griffin);
                        break;
                    case "6_celaeno_harpy":
                        classname = typeof(CelaenoHarpy);
                        break;
                    case "6_cyclops":
                        classname = typeof(Cyclops);
                        break;
                    case "6_ghoul":
                        classname = typeof(Ghoul);
                        break;
                    case "6_parasite":
                        classname = typeof(Parasite);
                        break;
                    case "6_swallow":
                        classname = typeof(Swallow);
                        break;
                    case "6_wild_hunt_rider":
                        classname = typeof(WildHuntRider);
                        break;
                    case "5_alpha_werewolf":
                        classname = typeof(AlphaWerewolf);
                        break;
                    case "5_wyvern":
                        classname = typeof(Wyvern);
                        break;
                    case "4_archespore":
                        classname = typeof(Archespore);
                        break;
                    case "4_cutthroat":
                        classname = typeof(Cutthroat);
                        break;
                    case "4_foglet":
                        classname = typeof(Foglet);
                        break;
                    case "4_nekker":
                        classname = typeof(Nekker);
                        break;
                    case "4_nekker_warrior":
                        classname = typeof(NekkerWarrior);
                        break;
                    case "4_plumard":
                        classname = typeof(Plumard);
                        break;
                    case "4_werewolf":
                        classname = typeof(Werewolf);
                        break;
                    case "0_gernichoras_fruit":
                        classname = typeof(GernichorasFruit);
                        break;
                    case "0_tactical_advantage":
                        classname = typeof(TacticalAdvantage);
                        break;
                    case "0_token":
                        classname = typeof(Token);
                        break;
                    default:
                        throw new ApplicationException(
                            $"Found new card class [{item.Type}] while parsing output in CardFactory");
                }
                cards.Add(AddCard(classname, basePoint.X + item.X, basePoint.Y + item.Y, item.Width, item.Height, (float) item.Confidence, classname.ToString(), 0));
            }
            return cards;
        }

        private static Card AddCard(Type classname, int x, int y, int width, int height, float confidence,
            string displayName, int value)
        {
            return (Card) Activator.CreateInstance(classname, x, y, width, height, confidence, value);
        }
    }
}
