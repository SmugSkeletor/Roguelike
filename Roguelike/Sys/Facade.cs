using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Core;
using RogueLike;
using Roguelike.Interfaces;
using RLNET;


namespace Roguelike.Sys
{
    class Facade
    {
        public void ShowMenu()
        {
            RLKeyPress keyPress = Game._mainConsole.Keyboard.GetKeyPress();
            if (keyPress != null) { Game._start = false; Game._renderChange = true; }
            Game._startConsole = new RLConsole(Game._screenWidth, Game._screenHeight);
            Game._startConsole.SetBackColor(0, 0, Game._screenWidth, Game._screenHeight, Colors.FloorBGFov);
            Game._startConsole.Print(22, 28, "R            R", Colors.MenuColor);
            Game._startConsole.Print(22, 27, "R           R", Colors.MenuColor);
            Game._startConsole.Print(22, 26, "R          R", Colors.MenuColor);
            Game._startConsole.Print(22, 25, "R         R", Colors.MenuColor);
            Game._startConsole.Print(22, 24, "R        R", Colors.MenuColor);
            Game._startConsole.Print(22, 23, "R       R", Colors.MenuColor);
            Game._startConsole.Print(22, 22, "R      R", Colors.MenuColor);
            Game._startConsole.Print(22, 21, "R     R", Colors.MenuColor);
            Game._startConsole.Print(22, 20, "R    R", Colors.MenuColor);
            Game._startConsole.Print(22, 19, "R   R", Colors.MenuColor);
            Game._startConsole.Print(22, 18, "R  R", Colors.MenuColor);
            Game._startConsole.Print(22, 17, "RRR", Colors.MenuColor);
            Game._startConsole.Print(22, 16, "RR", Colors.MenuColor);
            Game._startConsole.Print(22, 15, "RRRRRRRRRRRRR", Colors.MenuColor);
            Game._startConsole.Print(22, 14, "RR          RR", Colors.MenuColor);
            Game._startConsole.Print(22, 13, "R            RR", Colors.MenuColor);
            Game._startConsole.Print(22, 12, "R             R", Colors.MenuColor);
            Game._startConsole.Print(22, 11, "R             R", Colors.MenuColor);
            Game._startConsole.Print(22, 10, "R             R", Colors.MenuColor);
            Game._startConsole.Print(22, 9, "R            RR", Colors.MenuColor);
            Game._startConsole.Print(22, 8, "RR          RR", Colors.MenuColor);
            Game._startConsole.Print(22, 7, "RRRRRRRRRRRRR", Colors.MenuColor);

            Game._startConsole.Print(42, 28, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 27, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 26, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 25, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 24, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 23, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 22, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 21, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 20, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 19, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 18, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 17, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 16, "P", Colors.MenuColor);
            Game._startConsole.Print(42, 15, "PPPPPPPPPPPPP", Colors.MenuColor);
            Game._startConsole.Print(42, 14, "PP          PP", Colors.MenuColor);
            Game._startConsole.Print(42, 13, "P            PP", Colors.MenuColor);
            Game._startConsole.Print(42, 12, "P             P", Colors.MenuColor);
            Game._startConsole.Print(42, 11, "P             P", Colors.MenuColor);
            Game._startConsole.Print(42, 10, "P             P", Colors.MenuColor);
            Game._startConsole.Print(42, 9, "P            PP", Colors.MenuColor);
            Game._startConsole.Print(42, 8, "PP          PP", Colors.MenuColor);
            Game._startConsole.Print(42, 7, "PPPPPPPPPPPPP", Colors.MenuColor);

            Game._startConsole.Print(62, 28, "GGGGGGGGGGGGGGG", Colors.MenuColor);
            Game._startConsole.Print(62, 27, "GG           GG", Colors.MenuColor);
            Game._startConsole.Print(62, 26, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 25, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 24, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 23, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 22, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 21, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 20, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 19, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 18, "G            GG", Colors.MenuColor);
            Game._startConsole.Print(62, 17, "G      GGGGGGGG", Colors.MenuColor);
            Game._startConsole.Print(62, 16, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 15, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 14, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 13, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 12, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 11, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 10, "G", Colors.MenuColor);
            Game._startConsole.Print(62, 9, "G             G", Colors.MenuColor);
            Game._startConsole.Print(62, 8, "GG           GG", Colors.MenuColor);
            Game._startConsole.Print(62, 7, "GGGGGGGGGGGGGGG", Colors.MenuColor);

            Game._startConsole.Print(36, 40, "Autor: Adam Bartulewicz", Colors.Gold);
            Game._startConsole.Print(40, 44, "Klawiszologia:", Colors.Gold);
            Game._startConsole.Print(30, 48, "Poruszanie sie i atakowanie wrogow", Colors.Text);
            Game._startConsole.Print(32, 50, "odbywa sie za pomoca strzalek", Colors.Text);
            Game._startConsole.Print(34, 52, "By zejsc po schodach ( ", Colors.Text);
            Game._startConsole.Print(57, 52, ">", Colors.GameOverColor);
            Game._startConsole.Print(58, 52, " )", Colors.Text);
            Game._startConsole.Print(21, 54, "nalezy stojac na nich nacisnac symbol je oznaczajacy ", Colors.Text);
            Game._startConsole.Print(74, 54, ">", Colors.GameOverColor);
            Game._startConsole.Print(40, 56, "@", Colors.Player);
            Game._startConsole.Print(42, 56, "- Gracz", Colors.Text);

            Game._startConsole.Print(36, 58, "g", Colors.KoboldColor);
            Game._startConsole.Print(38, 58, "k", Colors.KoboldColor);
            Game._startConsole.Print(40, 58, "o", Colors.OrcColor);
            Game._startConsole.Print(42, 58, "- slabi wrogowie", Colors.Text);
            Game._startConsole.Print(38, 60, "G", Colors.KoboldColor);
            Game._startConsole.Print(40, 60, "B", Colors.BeholderColor);
            Game._startConsole.Print(42, 60, "- silni wrogowie", Colors.Text);
            Game._startConsole.Print(18, 63, "ZBIERZ JAK NAJWIECEJ ZLOTA PRZED SWOJA NIEUCHRONNA SMIERCIA", Colors.Gold);
            Game._startConsole.Print(26, 65, "NACISNIJ DOWOLNA STRZALKE ABY ZACZAC PRZYGODE", Colors.Gold);

            RLConsole.Blit(Game._startConsole, 0, 0, Game._screenWidth, Game._screenHeight, Game._mainConsole, 0, 0);
            Game._mainConsole.Draw();
        }
    }
}
