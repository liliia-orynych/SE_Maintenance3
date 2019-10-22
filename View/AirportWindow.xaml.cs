using System;
using System.Windows;
using System.Diagnostics;
using System.Data;
using Model;
using NLog;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
using View.Properties;

namespace View
{
    /**
     * \brief Клас взаємодії для MainWindow.xaml
     * \author Лілія Оринич
     * \version 1.0
     * \date Листопад 2018
     * \detail Клас визначає обробку натискання кнопок
     * у головному вікні програми.
     */
    public partial class AirportWindow : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ControlLib.Manipulation m = new ControlLib.Manipulation("DbConnection1");

        public IEnumerable<Control> Controls { get; private set; }

        /// Ініціалізує головне вікно програми
        public AirportWindow()
        {
            InitializeComponent();
        }

        /// Завантажує дані з бази у сітку даних на головному вікні
        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            logger.Info("Main Window loaded");
            var list = m.GetList();
            flightGrid.ItemsSource = list;
        }

        /**
         * \brief Обробляє натиснення кнопки додавання
         * \details При натисканні кнопки завантажується вікно додавання
         * рейсів. Введені дані зберігаються до бази даних.
         */
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                AddWindow addWindow = new AddWindow();
                addWindow.ShowDialog();
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Addition failed");
            }
        }

        /**
         * \brief Обробляє натискання кнопки додавання
         * \details Дані з бази повторно завантажуються та передаються
         * на сітку даних
         */
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Update method called");
            try
            {
                var list = m.GetList();
                flightGrid.ItemsSource = list;
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Update failed");
            }
        }


        /**
         * \brief Обробляє натиснення кнопки видалення
         * \details Виділена строка у сітці даних видаляється з бази даних
         */
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Delete method called");
            try
            {
                int temp = int.Parse(flightGrid.SelectedCells[0].ToString());
                m.FlightDeleting(temp);
                var list = m.GetList();
                flightGrid.ItemsSource = list;
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Delete failed");
            }
        }

        /**
         * \brief Обробляє натиснення кнопки пошуку за країною
         * \details Виводить рейси, що відлітають до Турції
         */
        private void GetByCountryButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Get by Country method called");
            try
            {
                var list = m.selectByCountry();
                flightGrid.ItemsSource = list;
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Getting by Country failed");
            }
        }

        /**
         * \brief Обробляє натиснення кнопки пошуку нічних рейсів
         * \details Виводить список рейсів що відлітають з 00:00 до 06:00
         */
        private void GetNightFlightsButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Get Night Flight method called");
            try
            {
                var list = m.sortByTime();
                flightGrid.ItemsSource = list;
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Getting Night Flights failed");
            }

        }

        /**
         * \brief Обробляє натиснення кнопки пошуку за напрямом
         * \details Виводить список рейсів за напрямом, що був введений
         * у текстове поле
         */
        private void GetByDirectionButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Get by Direction method called");
            try
            {
                var list = m.selectByDirection(directionTextBox.Text);
                flightGrid.ItemsSource = list;
                Regex reg = new Regex(@"[a-zA-Z]");
                Trace.Assert(reg.IsMatch(directionTextBox.Text), "Direction field has invalid characters");
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Getting by Direction failed");
            }
        }

        /**
         * \brief Обробляє натиснення кнопки сортування за часом
         * \details Виводить список рейсів, відсортований за часом
         */
        private void SortByDepartureButton_Click(object sender, RoutedEventArgs e)
        {
            logger.Info("Sort by Departure method called");
            try
            {
                var list = m.sortByTime();
                flightGrid.ItemsSource = list;
            }
            catch (Exception msg)
            {
                logger.Error(msg.ToString(), "Sorting by Departure failed");
            }
        }

        /**
         * \brief Обробляє натиснення кнопки відображення файлу логування
         * \details Відкриває файл логування
         */
        private void ShowLogButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"F:\Шарага\3 курс\5 семестр\Конструювання ПЗ\Orynych_318_04_2\View\bin\Debug\nlog.txt");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RussianMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");


        }

        private void UkrainianMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnglishMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


    }

}

