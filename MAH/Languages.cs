using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAH
{
    static class Languages
    {
        static public int language = 0;
        //0 - русский 1 - английский
        static public string Exit()
        {
            switch(language)
            {
                case 0:
                    return "Выйти";
                    break;
            }
            return "Exit";
        }
        static public string Settings()
        {
            switch (language)
            {
                case 0:
                    return "Настройки";
                    break;
            }
            return "Settings";
        }

        static public string StartGame()
        {
            switch (language)
            {
                case 0:
                    return "Начать игру";
                    break;
            }
            return "Start game";
        }
        static public string Apply()
        {
            switch (language)
            {
                case 0:
                    return "Применить";
                    break;
            }
            return "Apply";
        }
        static public string Save()
        {
            switch (language)
            {
                case 0:
                    return "Сохранить";
                    break;
            }
            return "Save";
        }
        static public string Exit_Without_Saving()
        {
            switch (language)
            {
                case 0:
                    return "Отмена";
                    break;
            }
            return "Cancel";
        }
        static public string Choose_Language()
        {
            switch (language)
            {
                case 0:
                    return "Язык интерфейса:";
                    break;
            }
            return "Interface language:";
        }
        static public string Screen_Size()
        {
            switch (language)
            {
                case 0:
                    return "Разрешение окна:";
                    break;
            }
            return "Screen size:";
        }
        static public string Interface_Color()
        {
            switch (language)
            {
                case 0:
                    return "Цвет интерфейса:";
                    break;
            }
            return "Interface color:";
        }
        static public string Back()
        {
            switch (language)
            {
                case 0:
                    return "Назад";
                    break;
            }
            return "Back";
        }
    }
}
