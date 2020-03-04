using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;
using System.Linq;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Fink, Kyle
    // Dated Created: 2/26/2020
    // Last Modified: 2/26/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderMenuScreen(finchRobot);
                        break;

                    case "d":

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region ALARM SYSTEM

        static void LightAlarmDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timetoMonitor = 0;

            bool quitDataRecorderMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) set sensors to monitor");
                Console.WriteLine("\tb) set range type");
                Console.WriteLine("\tc) set Maximum/Minimum Threshold Value");
                Console.WriteLine("\td) Set time to monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timetoMonitor = LightAlarmDisplaySetMaximumTimeToMonitor(rangeType, finchRobot);
                        break;
                    case "e":

                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }

        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;

            DisplayScreenHeader("Sensors To Monitor");

            Console.WriteLine("Sensors to monitor:");
            sensorsToMonitor = Console.ReadLine();

            DisplayContinuePrompt();

            return sensorsToMonitor;
        }

        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;

            DisplayScreenHeader("Range Type");

            Console.WriteLine("Range Type:");
            rangeType = Console.ReadLine();

            DisplayContinuePrompt();

            return rangeType;
        }

        static int LightAlarmDisplaySetMinMaxThresholdValue(string rangeType, Finch finchRobot) 
        {
            int minMaxThresholdValue;

            // validation
            bool validResponse;

            do
            {

                DisplayScreenHeader("Min/Max Threshold Value");

                Console.WriteLine($"Current Left light sensor: {finchRobot.getLeftLightSensor()}");
                Console.WriteLine($"Current right light sensor value: {finchRobot.getRightLightSensor()}");
                Console.WriteLine();

                Console.Write($"{rangeType} light sensor value: ");
                validResponse = int.TryParse(Console.ReadLine(), out minMaxThresholdValue);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an interger.");
                    DisplayContinuePrompt();
                }

            } while (!validResponse);

            //echo value back to user

            DisplayContinuePrompt();

            return minMaxThresholdValue;
        }

        static int LightAlarmDisplaySetMaximumTimeToMonitor(string rangeType, Finch finchRobot)
        {
            int timeToMonitor;

            // validation
            bool validResponse;

            do
            {

                DisplayScreenHeader("Time to Monitor");

                Console.WriteLine($"Time to Monitor:");
                Console.WriteLine("Time to Monitor [seconds]");
                Console.WriteLine();

                Console.Write($"{rangeType} light sensor value: ");
                validResponse = int.TryParse(Console.ReadLine(), out timeToMonitor);

            } while (!validResponse);

            //echo value back to user

            DisplayContinuePrompt();

            return timeToMonitor;
        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderMenuScreen(Finch myFinch) 
        {
            Console.CursorVisible = true;

            int numberOfPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            bool quitDataRecorderMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfPoints, dataPointFrequency, myFinch);
                        break;

                    case "d":
                        DataRecorderDisplayData(temperatures);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }

        private static void DataRecorderDisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Data");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayTable(double[] temperatures) 
        {
            //
            // table headers
            //
            Console.WriteLine(
                "DataPoint".PadLeft(12) +
                "Temp".PadLeft(10)
                );
            Console.WriteLine(
                "----------".PadLeft(12) +
                "----".PadLeft(10)
                );
            //
            // table data
            //
            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                (index + 1).ToString().PadLeft(12) +
                temperatures[index].ToString("n2").PadLeft(10)
                );
            }
        }

        static double[] DataRecorderDisplayGetData(int numberOfPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfPoints];
            int frequencyInSeconds;

            DisplayScreenHeader("Get Data");

            // echo number of data points

            Console.WriteLine("The Finch Robot is Ready to Record Tempuratures");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                Console.WriteLine($"Data #{index + 1}: {temperatures[index]} celsius");
                frequencyInSeconds = (int)(dataPointFrequency * 500);
                finchRobot.wait(frequencyInSeconds);
            }

            Console.WriteLine();
            Console.WriteLine("Current Data");
            DataRecorderDisplayTable(temperatures);

            Console.WriteLine();
            Console.WriteLine($"Average Tempurature: {temperatures.Average()}");

            DisplayContinuePrompt();

            return temperatures;
        }

        static double DataRecorderDisplayGetDataPointFrequency()
        {
            int dataPointFrequency;
            bool validResponse;
            string userResponse;

            do
            {
            DisplayScreenHeader("Data Point Frequency");

            Console.WriteLine("Data Point Frequency:");

            userResponse = Console.ReadLine();

            // validate response
            validResponse = int.TryParse(userResponse, out dataPointFrequency);

            if (!validResponse)
            {
                Console.WriteLine("Please enter the Data Point Frequency.");
                Console.WriteLine();
            }

                Console.WriteLine();
            Console.WriteLine($"Data Point Frequency: {dataPointFrequency}");
            } while (!validResponse);


            DisplayContinuePrompt();

            return dataPointFrequency;
        }

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            bool validResponse;
            string userResponse;

            do
            {
            DisplayScreenHeader("Number of Data Points");

            Console.WriteLine("Number of data points:");

            userResponse = Console.ReadLine();
            // validate response
            validResponse = int.TryParse(userResponse, out numberOfDataPoints);

                if (!validResponse)
                {
                    Console.WriteLine("Please enter the Number of data points.");
                    Console.WriteLine();
                }

            Console.WriteLine();
            Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");

            } while (!validResponse);




            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) ");
                Console.WriteLine("\tc) ");
                Console.WriteLine("\td) ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);
                        break;

                    case "b":

                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
