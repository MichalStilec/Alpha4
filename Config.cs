using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha4
{
    public class Config
    {
        // Methods for reading data from a configuration file
        /// <summary>
        /// Loads the peer-id from the config file
        /// </summary>
        /// <returns></returns>
        public static string LoadPeer()
        {
            return ConfigStructure("Peer ID", "stilec2");
        }
        public static string LoadPort()
        {
            return ConfigStructure("Port", "9876");
        }

        /// <summary>
        /// Reads the selected line from the config.cfg file
        /// </summary>
        /// <param name="data">Line name</param>
        /// <param name="defaultData">The base value, which is set if the program does not find another</param>
        /// <returns></returns>
        private static string ConfigStructure(string data, string defaultData)
        {
            string configFile = "config/config.cfg";

            if (File.Exists(configFile))
            {
                string[] lines = File.ReadAllLines(configFile);

                foreach (var line in lines)
                {
                    var parts = line.Split('=');

                    if (parts.Length == 2 && parts[0].Trim() == data)
                    {
                        return parts[1].Trim();
                    }
                }
            }

            // Return the default value if the correct value is not found
            return defaultData;
        }
    }
}