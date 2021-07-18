/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using CP.Common.Logger;
using CP.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CP.Common.Commands
{
    public class CommandSystem
    {
        /*
         * TODO: Work on a Command system that can be instantiated multiple times while still being able to dynamically load CPCommands but only commands that correlate to 
         *       the correct Command System.
         */

        public Dictionary<string, Command> Commands { get; private set; } = new Dictionary<string, Command>();
        public ILogger Logger { get; set; } = new ConsoleLogger();
        public string CommandID = "";

        public CommandSystem(string commandID = "")
        {
            RegisterCommand();
            CommandID = commandID;
        }

        public void RegisterCommand()
        {
            Commands.Clear();
            Dictionary<string, string> commandAliases = new Dictionary<string, string>();
            List<Command> commands = ClassLoader.Load<Command>().ToList();
            commands = commands.OrderBy(t => t.Name).ToList();
            foreach (Command cl in commands)
            {
                if (!cl.CommandSystemID.Equals("") && !cl.CommandSystemID.Equals(CommandID))
                    return;
                string name = cl.Name;
                if (name == null)
                {
                    Logger.WriteLine("Command " + cl.GetType().Name + " has no name!", LogLevel.WARNING);
                    continue;
                }

                if (Commands.ContainsKey(name.ToLower()) && cl.GetType().GetCustomAttribute(typeof(OverrideCommandAttribute), false) == null)
                {
                    Logger.WriteLine(name + " already exists, please remove or rename!", LogLevel.WARNING);
                    continue;
                }

                if (cl.Aliases == null || cl.Aliases.Count == 0)
                {
                    Logger.WriteLine("Command " + name + " has no aliases! Using " + name.ToLower() + " as Alias");
                    cl.Aliases.Add(name.ToLower());
                }

                foreach(string alias in cl.Aliases.ToArray())
                {
                    if(commandAliases.ContainsKey(alias))
                    {
                        if(name.Equals(commandAliases[alias]))
                        {
                            Logger.WriteLine("The Command " + name + " has duplicate alias entries.", LogLevel.WARNING);
                            cl.Aliases.RemoveAll(t => t == alias);
                            cl.Aliases.Add(alias);
                        } else
                        {
                            Logger.WriteLine("The alias " + alias + " for Command " + name + " is already in use by Command " + commandAliases[alias] + ", removing!", LogLevel.WARNING);
                            cl.Aliases.RemoveAll(t => t == alias);
                        }
                        continue;
                    }

                    commandAliases.Add(alias, name);
                }

                cl.SetParentSystem(this);
                Commands.Add(name.ToLower(), cl);
            }
        }
        
        public bool CommandInterface(object obj, string[] args)
        {
            foreach (Command command in Commands.Values)
            {
                if (command.Aliases.Contains(args[0].ToLower()))
                {
                    if (!command.Execute(obj, args.ToList()))
                    {
                        Logger.WriteLine(command.ToString());
                    }

                    return true;
                }
            }

            Logger.WriteLine("Invalid Command!\n");
            return false;
        }
    }
}