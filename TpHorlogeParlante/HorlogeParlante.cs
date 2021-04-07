using System;
using System.Collections.Generic;
using System.Text;

namespace TpHorlogeParlante
{
    public class HorlogeParlante
    {
        #region Refactorisation

        private static StringBuilder MinuteThatArentFiveMult(int min, string[] minutesOnetoFourStr,
            StringBuilder builder)
        {
            if (min > 5)
                min -= 5;

            for (int i = 0; i < 5; ++i)
            {
                if (i == 4 && min == 4)
                {
                    builder.Append(" à " + minutesOnetoFourStr[i - 1] + " minute près");
                }
                else if (i == min)
                {
                    builder.Append(" à " + minutesOnetoFourStr[i - 1] + " minutes près");
                }
            }

            return builder;
        }

        private static StringBuilder MinuteThatAreFiveMult(int index, string msg1, string msg2, int minutes,
            Dictionary<int, string> fiveStr, StringBuilder builder)
        {
            if ((float) minutes / (float) 15 == (int) ((float) minutes / (float) 15))
            {
                builder.Append(msg1 + fiveStr[index]);
            }
            else
            {
                double minutesRoundUp = Math.Ceiling((double) minutes / 5);
                builder.Append(msg1 + fiveStr[(int) minutesRoundUp] + msg2);
            }

            return builder;
        }

        private static StringBuilder checkHour(int hours, int minutes, string[] hoursStr, StringBuilder builder)
        {
            if (hours > 12)
                hours -= 12;

            if (minutes > 30)
                hours++;

            string stringHour = hoursStr[hours - 1];
            if (hours == 1)
                builder.Append(stringHour + " heure");
            else
                builder.Append(stringHour + " heures");

            return builder;
        }

        #endregion

        public static string GiveHour(int hours, int minutes)
        {
            var builder = new StringBuilder();

            #region Gestion des Erreurs

            if (hours > 24 || hours < 0)
                throw new ArgumentOutOfRangeException(nameof(hours),
                    "Hours must be contain in a range between 0 and 24.");
            if (minutes < 0 || minutes > 59)
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes shall be between 0 and 59");

            #endregion

            #region Gestion des heures

            string[] hoursStr = new string[]
                {"Une", "Deux", "Trois", "Quatre", "Cinq", "Six", "Sept", "Huit", "Neuf", "Dix", "Onze"};


            if (hours == 0 || hours == 24)
                builder.Append("Minuit");
            else if (hours == 12)
                builder.Append("Midi");
            else
                builder = checkHour(hours, minutes, hoursStr, builder);

            #endregion

            #region Gestion des minutes

            #region DeclarationTableaux

            string[] minutesOnetoFourStr = new string[]
            {
                "quatre", "trois", "deux", "une"
            };

            Dictionary<int, string> fiveStr = new Dictionary<int, string>()
            {
                {1, "cinq"}, {2, "dix"}, {3, "quart"}, {4, "vingt"}, {5, "vingt-cinq"}, {7, "ving-cinq"},
                {8, "vingt"}, {10, "dix"}, {11, "cinq"}, {13, "le quart"}
            };

            #endregion

            #region Gestion des tranches de 30, 15 et 5 minutes

            if (minutes != 0 && minutes < 56)
            {
                if ((float) minutes / (float) 30 == (int) ((float) minutes / (float) 30))
                {
                    builder.Append(" et demi");
                }
                else if (minutes < 30)
                {
                    builder = MinuteThatAreFiveMult(3, " et ", " minutes", minutes, fiveStr, builder);
                }
                else if (minutes > 30)
                {
                    builder = MinuteThatAreFiveMult(13, " moins ", "", minutes, fiveStr, builder);
                }
            }

            #endregion

            #region Gestion du matin ou de l'après midi

            if (hours > 12 && hours < 24)
                builder.Append(" de l'après midi");
            else if (hours < 12 && hours > 0)
                builder.Append(" du matin");

            #endregion

            #region Gestion des minutes n'étant pas des tranches de 5

            if (!((float) minutes / (float) 5 == (int) ((float) minutes / (float) 5)))
            {
                String str = minutes.ToString();
                if (minutes < 10)
                {
                    int min = Int32.Parse(str);
                    builder = MinuteThatArentFiveMult(min, minutesOnetoFourStr, builder);
                }
                else if (minutes > 10)
                {
                    int min = Int32.Parse(str[1].ToString());
                    builder = MinuteThatArentFiveMult(min, minutesOnetoFourStr, builder);
                }
            }

            #endregion

            #endregion

            string result = builder.ToString();
            return result;
        }
    }
}