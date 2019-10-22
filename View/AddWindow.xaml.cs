using System;
using System.Windows;
using NLog;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Configuration;

namespace View
{
/**
 * \brief Клас взаємодії для AddWindow.xaml
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас визначає обробку введення даних та натискання кнопок
 * у вікні додавання рейсів.
 */
    public partial class AddWindow : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ControlLib.Manipulation m = new ControlLib.Manipulation();

        /// Ініціалізує вікно додавання рейсів
        public AddWindow()
        {
            logger.Info("Add Window loaded");
            InitializeComponent();
        }

        /// Обробляє натиснення кнопки додавання
        private void AddFlightButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Add Flight method called");
            try
            {
                Regex direction_reg = new Regex(@"[A-Za-z]");
                Regex time_reg = new Regex(@"(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])-[0-9]{4}\s([0-1]\d|2[0-3])(:[0-5]\d)");
                if (direction_reg.IsMatch(directionTextBox.Text) && time_reg.IsMatch(departureTextBox.Text) && time_reg.IsMatch(arriveTextBox.Text))
                {
                    m.FlightAddition(new ControlLib.AirportModel
                    {
                        direction = directionTextBox.Text,
                        departureTime = DateTime.ParseExact(departureTextBox.Text, "MM-dd-yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None),
                        arriveTime = DateTime.ParseExact(arriveTextBox.Text, "MM-dd-yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None)
                    });
                }
                Trace.Assert(direction_reg.IsMatch(directionTextBox.Text), "Direction has invalid characters");
                Trace.Assert(time_reg.IsMatch(departureTextBox.Text), "Deprture time has incorrect format");
                Trace.Assert(time_reg.IsMatch(arriveTextBox.Text), "Arrive time has incorrect format");
                Close();
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Addition failed");
            }
        }
    }
}
