using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TpHorlogeParlante;

namespace TestHorlogeParlante
{
    [TestClass]
    public class UnitTest1
    {
        #region Factorisation test

        private void GiveMeTimeFacto(int hours, int minutes, string excpeted)
        {
            HorlogeParlante.GiveHour(hours, minutes);

            Assert.AreEqual(excpeted, HorlogeParlante.GiveHour(hours, minutes));
        }

        private void GiveMeError<T>(int hours, int minutes) where T : Exception
        {
            Action act = () => HorlogeParlante.GiveHour(hours, minutes);

            Assert.ThrowsException<T>(act);
        }

        #endregion

        #region testGiveHour (Nominal)

        #region tesGiveHour (Hours)

        #region Morning

        [TestMethod]
        public void GiveMeTime0100()
        {
            GiveMeTimeFacto(1, 0, "Une heure du matin");
        }

        [TestMethod]
        public void GiveMeTime0200()
        {
            GiveMeTimeFacto(2, 0, "Deux heures du matin");
        }

        [TestMethod]
        public void GiveMeTime0300()
        {
            GiveMeTimeFacto(3, 0, "Trois heures du matin");
        }

        [TestMethod]
        public void GiveMeTime0500()
        {
            GiveMeTimeFacto(5, 0, "Cinq heures du matin");
        }

        [TestMethod]
        public void GiveMeTime0800()
        {
            GiveMeTimeFacto(8, 0, "Huit heures du matin");
        }

        [TestMethod]
        public void GiveMeTime1100()
        {
            GiveMeTimeFacto(11, 0, "Onze heures du matin");
        }

        #endregion

        #region After Morning

        [TestMethod]
        public void GiveMeTime1300()
        {
            GiveMeTimeFacto(13, 0, "Une heure de l'après midi");
        }

        [TestMethod]
        public void GiveMeTime2300()
        {
            GiveMeTimeFacto(23, 0, "Onze heures de l'après midi");
        }

        #endregion

        #region Midnight and Midday

        [TestMethod]
        public void GiveMeTime1200()
        {
            GiveMeTimeFacto(12, 0, "Midi");
        }

        [TestMethod]
        public void GiveMeTime2400()
        {
            GiveMeTimeFacto(24, 0, "Minuit");
        }

        [TestMethod]
        public void GiveMeTime0000()
        {
            GiveMeTimeFacto(0, 0, "Minuit");
        }

        #endregion

        #endregion

        #region testGiveHour (Minutes)

        #region Five to Five

        #region Under Thirty

        [TestMethod]
        public void GiveMeTime0105()
        {
            GiveMeTimeFacto(1, 5, "Une heure et cinq minutes du matin");
        }

        [TestMethod]
        public void GiveMeTime0110()
        {
            GiveMeTimeFacto(1, 10, "Une heure et dix minutes du matin");
        }

        [TestMethod]
        public void GiveMeTime0120()
        {
            GiveMeTimeFacto(1, 20, "Une heure et vingt minutes du matin");
        }

        [TestMethod]
        public void GiveMeTime0125()
        {
            GiveMeTimeFacto(1, 25, "Une heure et vingt-cinq minutes du matin");
        }

        #endregion

        #region Upper Thirty

        [TestMethod]
        public void GiveMeTime0140()
        {
            GiveMeTimeFacto(1, 40, "Deux heures moins vingt du matin");
        }

        [TestMethod]
        public void GiveMeTime0150()
        {
            GiveMeTimeFacto(1, 50, "Deux heures moins dix du matin");
        }

        [TestMethod]
        public void GiveMeTime0155()
        {
            GiveMeTimeFacto(1, 55, "Deux heures moins cinq du matin");
        }

        #endregion

        #endregion

        #region Special Numbers

        [TestMethod]
        public void GiveMeTime0130()
        {
            GiveMeTimeFacto(1, 30, "Une heure et demi du matin");
        }

        [TestMethod]
        public void GiveMeTime0115()
        {
            GiveMeTimeFacto(1, 15, "Une heure et quart du matin");
        }

        [TestMethod]
        public void GiveMeTime0145()
        {
            GiveMeTimeFacto(1, 45, "Deux heures moins le quart du matin");
        }

        #endregion

        #region other numbers

        [TestMethod]
        public void GiveMeTime0116()
        {
            GiveMeTimeFacto(1, 16, "Une heure et vingt minutes du matin à quatre minutes près");
        }

        [TestMethod]
        public void GiveMeTime0117()
        {
            GiveMeTimeFacto(1, 17, "Une heure et vingt minutes du matin à trois minutes près");
        }

        [TestMethod]
        public void GiveMeTime0118()
        {
            GiveMeTimeFacto(1, 18, "Une heure et vingt minutes du matin à deux minutes près");
        }

        [TestMethod]
        public void GiveMeTime0119()
        {
            GiveMeTimeFacto(1, 19, "Une heure et vingt minutes du matin à une minute près");
        }

        #endregion

        #endregion

        #endregion

        #region testGiveHour (Extreme)

        [TestMethod]
        public void GiveMeTime0159()
        {
            GiveMeTimeFacto(1, 59, "Deux heures du matin à une minute près");
        }

        [TestMethod]
        public void GiveMeTimeWhereHoursAndMinutesEqualsZeroZero()
        {
            GiveMeTimeFacto(00, 00, "Minuit");
        }

        #endregion

        #region testGiveHour (Error)

        [TestMethod]
        public void GiveMeTimeHoursNegative()
        {
            GiveMeError<ArgumentOutOfRangeException>(-1, 0);
        }

        [TestMethod]
        public void GiveMeTimeMinutesNegative()
        {
            GiveMeError<ArgumentOutOfRangeException>(0, -1);
        }

        [TestMethod]
        public void GiveMeTimeHoursOverTwentyFour()
        {
            GiveMeError<ArgumentOutOfRangeException>(25, 0);
        }

        [TestMethod]
        public void GiveMeTimeMinutesOverFiftyNine()
        {
            GiveMeError<ArgumentOutOfRangeException>(0, 60);
        }

        #endregion
    }
}